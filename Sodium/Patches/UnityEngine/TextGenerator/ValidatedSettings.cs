using HarmonyLib;
using UnityEngine;

namespace Sodium.Patches.UnityEngine.TextGenerator
{
    [HarmonyPatch(typeof(global::UnityEngine.TextGenerator), "ValidatedSettings")]
    public static class TextGeneratorPatch
    {
        static bool Prefix(ref TextGenerationSettings settings, ref TextGenerationSettings __result)
        {
            TextGenerationSettings textGenerationSettings;
            if (settings.font != null && settings.font.dynamic)
                textGenerationSettings = settings;
            else
            {
                if (settings.fontSize != 0 || settings.fontStyle > FontStyle.Normal)
                {
                    settings.fontSize = 0;
                    settings.fontStyle = FontStyle.Normal;
                }

                if (settings.resizeTextForBestFit)
                    settings.resizeTextForBestFit = settings.font != null;

                textGenerationSettings = settings;
            }
            __result = textGenerationSettings;
            return false;
        }
    }
}