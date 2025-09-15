using BepInEx;
using Sodium.Console;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

namespace Sodium
{
    [System.ComponentModel.Description(PluginInfo.Description)]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin instance { get; private set; }

        private void Awake()
        {
            instance = this;
#if RELEASE
            Debug.unityLogger.logEnabled = false;
#endif

            Logger.Log(@"
  ___          _ _            
 / __| ___  __| (_)_  _ _ __  
 \__ \/ _ \/ _` | | || | '  \ 
 |___/\___/\__,_|_|\_,_|_|_|_|
     
 crimsoncauldron 
 tagdoesnothing
 astral
");

            PatchHandler.PatchAll();
            GorillaTagger.OnPlayerSpawned(OnGameInit);
        }

        private void OnGameInit()
        {
            #region Unity Application Settings
            Application.targetFrameRate = 144;
            #endregion

            #region Quality Settings
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
            }
            #endregion

            #region Camera
            Camera.main.farClipPlane = 50f;
            Camera.main.anamorphism = 0.0f;
            #endregion

            #region XR Disabler
            // Credits to The-Graze for this snippet
            XRManagerSettings xrManager = XRGeneralSettings.Instance.Manager;
            XRDisplaySubsystem xrDisplay = xrManager.activeLoader.GetLoadedSubsystem<XRDisplaySubsystem>();

            if (xrDisplay == null)
            {
                xrManager.DeinitializeLoader();
                QualitySettings.vSyncCount = -1;
            }
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