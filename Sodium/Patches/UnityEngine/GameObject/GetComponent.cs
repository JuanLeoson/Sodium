using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Sodium.Patches.UnityEngine.GameObject
{
    [HarmonyPatch(typeof(global::UnityEngine.GameObject), "GetComponent", new[] { typeof(Type) })]
    [HarmonyPriority(Priority.First)]
    public static class GetComponent
    {
        private static Dictionary<(object, Type), Component> componentPool = new Dictionary<(object, Type), Component>();
        private static LinkedList<(object, Type)> insertionOrder = new LinkedList<(object, Type)>();
        private static object _lock = new object();

        private static bool Prefix(global::UnityEngine.GameObject __instance, Type type, ref Component __result)
        {
            var key = (__instance, type);

            lock (_lock)
            {
                if (componentPool.TryGetValue(key, out Component component))
                {
                    if (component == null)
                        componentPool.Remove(key);
                    else
                    {
                        __result = component;
                        return false;
                    }
                }
            }

            return true;
        }

        private static void Postfix(global::UnityEngine.GameObject __instance, Type type, ref Component __result)
        {
            if (__result == null) return;

            var key = (__instance, type);

            lock (_lock)
            {
                if (!componentPool.ContainsKey(key))
                {
                    if (componentPool.Count >= 512)
                    {
                        var oldestKey = insertionOrder.First.Value;
                        insertionOrder.RemoveFirst();
                        componentPool.Remove(oldestKey);
                    }

                    componentPool[key] = __result;
                    insertionOrder.AddLast(key);
                }
            }
        }
    }
}
