using HarmonyLib;
using UnityEngine;

namespace Sodium.Patches.UnityEngine.Debug
{
    public static class LogWarning
    {
        [HarmonyPatch(typeof(global::UnityEngine.Debug), "LogWarning", new[] { typeof(object) })]
        [HarmonyPriority(Priority.First)]
        public static class LogObject
        {
            private static bool Prefix(object message)
            {
#if DEBUG
                System.Console.WriteLine(message);
#endif
                return false;
            }
        }

        [HarmonyPatch(typeof(global::UnityEngine.Debug), "LogWarning", new[] { typeof(object), typeof(Object) })]
        [HarmonyPriority(Priority.First)]
        public static class LogContext
        {
            private static bool Prefix(object message, Object context)
            {
#if DEBUG
                System.Console.WriteLine(message);
#endif
                return false;
            }
        }
    }

    public static class LogWarningFormat
    {
        [HarmonyPatch(typeof(global::UnityEngine.Debug), "LogWarningFormat", new[] { typeof(string), typeof(object[]) })]
        [HarmonyPriority(Priority.First)]
        public static class LogObject
        {
            private static bool Prefix(string format, object[] args)
            {
#if DEBUG
                System.Console.WriteLine(string.Format(format, args));
#endif
                return false;
            }
        }

        [HarmonyPatch(typeof(global::UnityEngine.Debug), "LogWarningFormat", new[] { typeof(Object), typeof(string), typeof(object[]) })]
        [HarmonyPriority(Priority.First)]
        public static class LogContext
        {
            private static bool Prefix(Object context, string format, object[] args)
            {
#if DEBUG
                System.Console.WriteLine(string.Format(format, args));
#endif
                return false;
            }
        }
    }
}
