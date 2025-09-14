using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sodium.Patches.UnityEngine.GameObject
{
    [HarmonyPatch(typeof(global::UnityEngine.GameObject), "Find")]
    [HarmonyPriority(Priority.First)]
    public static class Find
    {
        private static Dictionary<string, global::UnityEngine.GameObject> gameObjectCache = new Dictionary<string, global::UnityEngine.GameObject>();
        private static Queue<string> cacheOrder = new Queue<string>();

        private static bool Prefix(string name, ref global::UnityEngine.GameObject __result)
        {
            if (gameObjectCache.TryGetValue(name, out global::UnityEngine.GameObject result))
            {
                if (result != null)
                {
                    __result = result;
                    return false;
                }
                else
                    gameObjectCache.Remove(name);
            }

            return true;
        }

        private static void Postfix(string name, global::UnityEngine.GameObject __result)
        {
            if (__result != null && !gameObjectCache.ContainsKey(name))
            {
                cacheOrder.Enqueue(name);
                gameObjectCache[name] = __result;
            }

            if (gameObjectCache.Count >= 512)
            {
                string oldestKey = cacheOrder.Dequeue();
                gameObjectCache.Remove(oldestKey);
            }
        }
    }
}
