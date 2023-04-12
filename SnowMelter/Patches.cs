using HarmonyLib;

namespace SnowMelter;

public class Patches
{
    [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.Awake))]
    public static class SnowMelterPatch
    {
        public static void Postfix(Fireplace __instance)
        {
            if (__instance.transform != null)
            {
                var enabled = __instance.transform.Find("_enabled");
                if (enabled)
                {
                    var melter = enabled.gameObject.AddComponent<Snowmelter>();
                    melter._OwningFireplace = __instance;
                }
            }
        }
    }
}