using System;
using System.Linq;

using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using ModMenu;
using UILib.Patches;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TweaksOfYore {
    [BepInDependency("com.github.Kaden5480.poy-ui-lib")]
    [BepInDependency(
        "com.github.Kaden5480.poy-mod-menu",
        BepInDependency.DependencyFlags.SoftDependency
    )]
    [BepInPlugin("com.github.Kaden5480.poy-tweaks-of-yore", "Tweaks of Yore", PluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin {
        private static Plugin instance = null;

        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        private void Awake() {
            instance = this;

            TweaksOfYore.Config.Init(this.Config);

            Patcher.Awake();

            SceneLoads.AddLoadListener((Scene scene) => {
                Cache.FindObjects(scene);
                Patcher.SceneLoad();
            });

            SceneLoads.AddUnloadListener(delegate {
                Patcher.SceneUnload();
                Cache.Clear();
            });

            if (AccessTools.AllAssemblies().FirstOrDefault(
                    a => a.GetName().Name == "ModMenu"
                ) != null
            ) {
                Register();
            }
        }

        /**
         * <summary>
         * Register with Mod Menu.
         * </summary>
         */
        private void Register() {
            ModInfo info = ModManager.Register(this);
            info.license = "GPL-3.0";

            info.Add(typeof(TweaksOfYore.Config));
        }

        /**
         * <summary>
         * Executes each frame.
         * </summary>
         */
        private void Update() {
            Patcher.Update();
        }

         /**
          * <summary>
          * Logs a debug message.
          * </summary>
          * <param name="message">The message to log</param>
          */
         internal static void LogDebug(string message) {
 #if DEBUG
             if (instance == null) {
                 Console.WriteLine($"[Debug] TweaksOfYore: {message}");
                 return;
             }

             instance.Logger.LogInfo(message);
 #else
             if (instance != null) {
                 instance.Logger.LogDebug(message);
             }
 #endif
         }

         /**
          * <summary>
          * Logs an informational message.
          * </summary>
          * <param name="message">The message to log</param>
          */
         internal static void LogInfo(string message) {
             if (instance == null) {
                 Console.WriteLine($"[Info] TweaksOfYore: {message}");
                 return;
             }
             instance.Logger.LogInfo(message);
         }

         /**
          * <summary>
          * Logs an error message.
          * </summary>
          * <param name="message">The message to log</param>
          */
         internal static void LogError(string message) {
             if (instance == null) {
                 Console.WriteLine($"[Error] TweaksOfYore: {message}");
                 return;
             }
             instance.Logger.LogError(message);
         }
    }
}
