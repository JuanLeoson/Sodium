using HarmonyLib;
using System;

namespace Sodium.Patches.UnityEngine.Debug
{
    public static class LogException
    {
        [HarmonyPatch(typeof(global::UnityEngine.Debug), "LogException", new[] { typeof(Exception) })]
        [HarmonyPriority(Priority.First)]
        public static class LogObject
        {
            private static bool Prefix(Exception exception)
            {
#if DEBUG
                System.Console.WriteLine(exception);
#endif
                return false;
            }
        }

        [HarmonyPatch(typeof(global::UnityEngine.Debug), "LogException", new[] { typeof(Exception), typeof(global::UnityEngine.Object) })]
        [HarmonyPriority(Priority.First)]
        public static class LogContext
        {
            private static bool Prefix(Exception exception, global::UnityEngine.Object context)
            {
#if DEBUG
                System.Console.WriteLine(exception);
#endif
                return false;
            }
        }
    }
}
