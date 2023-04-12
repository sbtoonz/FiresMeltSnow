using System;
using System.Collections.Generic;
using System.Text;
using SnowMelter;
using UnityEngine;


public class Snowmelter : MonoBehaviour
{
    public List<Material> OriginalMaterials = new List<Material>();
    public Fireplace? _OwningFireplace;

    private void OnEnable()
    {
        if (_OwningFireplace != null) if(!_OwningFireplace.IsBurning())return;
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, SnowMelterMod.MeltRadius!.Value);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponentsInChildren<Renderer>() != null)
            {
                var m = hitCollider.gameObject.GetComponentsInChildren<Renderer>();
                foreach (var r in m)
                {
                    if (r.material.shader.name.StartsWith("Custom/Veg") || r.material.shader.name.StartsWith("Custom/Static") && r.material.GetInt("_AddSnow") == 1)
                    {
                        r.material.SetInt("_AddSnow", 0);
                        var sb = new StringBuilder(r.material.name);
                        sb.Append(sb.GetHashCode() / UnityEngine.Random.value);
                        r.material.name = sb.ToString();
                        OriginalMaterials.Add (r.material);
                    }
                    if (r.material.shader.name.StartsWith("Custom/Piece"))
                    {
                        r.material.shader = Shader.Find("Custom/StaticRock");
                        r.material.SetInt("_AddSnow", 0);
                        var sb = new StringBuilder(r.material.name);
                        sb.Append(sb.GetHashCode() / UnityEngine.Random.value);
                        sb.Append("_pieceMat");
                        r.material.name = sb.ToString();
                        OriginalMaterials.Add( r.material);
                    }
                }
            }
            
        }
        
    }

    private void OnDisable()
    {
        foreach (var VARIABLE in OriginalMaterials)
        {
            if (VARIABLE.GetInt("_AddSnow") == 0)
            {
                VARIABLE.SetInt("_AddSnow", 1); 
            }

            if (VARIABLE.name.Contains("_pieceMat"))
            {
                VARIABLE.shader = Shader.Find("Custom/Piece");
                string t = "_pieceMat";
                var i = VARIABLE.name.IndexOf(t);
                var sb = new StringBuilder(VARIABLE.name);
                sb.Remove(i, 9);
                VARIABLE.name = sb.ToString();
            }
        }
        OriginalMaterials.Clear();
    }
}