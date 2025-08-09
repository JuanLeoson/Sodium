using BepInEx;
using Console;
using UnityEngine;

namespace Sodium
{
    [BepInPlugin("com.tagandastral.Sodium", "Sodium", Sodium.PluginData.Internal_Plugin_Data.ThisVersion)]
    internal class Plugin : BaseUnityPlugin
    {
        private static string loadedvers = "-LOADING-";
        public readonly string ClientSodiumVersion = loadedvers;
        private void Awake()
        {
            UnityEngine.Debug.Log(@"
  ___          _ _            
 / __| ___  __| (_)_  _ _ __  
 \__ \/ _ \/ _` | | || | '  \ 
 |___/\___/\__,_|_|\_,_|_|_|_|
                              
     Made By: tag, astral     
");
            GorillaTagger.OnPlayerSpawned(OnGameInit);
        }

        private void OnGameInit()
        {
            #region Unity Application Settings Shit
            Application.targetFrameRate = 144;
            #endregion
            #region Quality Settings Shit
            QualitySettings.SetQualityLevel(1);
            QualitySettings.antiAliasing = 0;
            QualitySettings.shadows = 0;
            QualitySettings.particleRaycastBudget = 0;
            QualitySettings.pixelLightCount = 0;
            QualitySettings.anisotropicFiltering = 0;
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.globalTextureMipmapLimit = 0;
            QualitySettings.lodBias = 0.0f;
            QualitySettings.pixelLightCount = 0;
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.enableLODCrossFade = false;
            QualitySettings.maximumLODLevel = 0;
            foreach (Camera camera in Camera.allCameras)
            {
                camera.allowMSAA = false;
                camera.focusDistance = 0;
                camera.farClipPlane = 50.0f;
                camera.focusDistance = 1f;
                camera.allowHDR = false;
                //camera.nearClipPlane = 0.07f; breaks mirror
            }

            #endregion
            #region Camera Shit
            Camera.main.farClipPlane = 50f;
            Camera.main.anamorphism = 0.0f;
            #endregion
        }

        void Start() =>
    GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);

        void OnPlayerSpawned()
        {
            string ConsoleGUID = $"goldentrophy_Console_{Console.Console.ConsoleVersion}";
            GameObject ConsoleObject = GameObject.Find(ConsoleGUID);

            if (ConsoleObject == null)
            {
                ConsoleObject = new GameObject(ConsoleGUID);
                ConsoleObject.AddComponent<CoroutineManager>();
                ConsoleObject.AddComponent<Console.Console>();
            }

            if (ServerData.ServerDataEnabled)
                ConsoleObject.AddComponent<ServerData>();
        }
    }
};


//i crashed my car into north korea
