using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace SnowMelter;

public static class Transpiler_Class
{
    /*
    IL_00b1: ldarg.0      // this
    IL_00b2: ldarg.2      // wx
    IL_00b3: ldarg.3      // wy
    IL_00b4: ldc.i4.0
    IL_00b5: call         instance float32 WorldGenerator::GetSnowMountainHeight(float32, float32, bool)
    ---->  call         instance float32 WorldGenerator::GetMenuHeight(float32, float32) * 200f
    IL_00ba: ldc.r4       200
    IL_00bf: mul
    IL_00c0: ret
     */

  
}