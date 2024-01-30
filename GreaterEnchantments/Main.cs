using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;

namespace GreaterEnchantments
{
    [BepInPlugin("Aidanamite.GreaterEnchantments", "GreaterEnchantments", "1.0.0")]
    public class Main : BaseUnityPlugin
    {
        internal static Assembly modAssembly = Assembly.GetExecutingAssembly();
        internal static string modName = $"{modAssembly.GetName().Name}";
        internal static string modDir = $"{Environment.CurrentDirectory}\\BepInEx\\{modName}";

        void Awake()
        {
            new Harmony($"com.Aidanamite.{modName}").PatchAll(modAssembly);
            Logger.LogInfo($"{modName} has loaded");
        }
    }

    [HarmonyPatch(typeof(Constants))]
    public class Patch_Constants
    {
        [HarmonyPatch("GetFloat")]
        [HarmonyPostfix]
        static void GetFloat(ref float __result, string key)
        {
            if (key == "kMaxDefense")
                __result = 90;
        }

        [HarmonyPatch("GetInt")]
        [HarmonyPostfix]
        static void GetInt(ref int __result, string key)
        {
            if (key.Contains("EnchantmentLevel"))
                __result = int.MaxValue;
        }
    }
}