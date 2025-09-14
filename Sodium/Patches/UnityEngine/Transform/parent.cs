using HarmonyLib;
namespace Sodium.Patches.UnityEngine.Transform
{
    [HarmonyPatch(typeof(global::UnityEngine.Transform), "parent", MethodType.Setter)]
    [HarmonyPriority(Priority.First)]
    public static class TransformParentPatch
    {
        static bool Prefix(global::UnityEngine.Transform __instance, global::UnityEngine.Transform value)
        {
            __instance.parentInternal = value;
            return false;
        }
    }
}
