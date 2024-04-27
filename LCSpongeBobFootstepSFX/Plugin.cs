
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using LCSpongeBobFootstepSFX.Patches;

namespace LCSpongeBobFootstepSFX
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class LCSpongeBobFootstepSFXBase : BaseUnityPlugin
    {
        private const string modGUID = "Overcastua.SpongebobStepSound";
        private const string modName = "Spongebob Footsteps Sound";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static LCSpongeBobFootstepSFXBase Instance;

        internal ManualLogSource mls;

        internal static List<AudioClip> FootstepSFX;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("------------Mod Loaded------------");

            harmony.PatchAll(typeof(StartOfRoundPatch));

            mls = Logger;

            FootstepSFX = new List<AudioClip>();

            string folderLocation = Instance.Info.Location;
            folderLocation = folderLocation.TrimEnd("LCSpongeBobFootstepSFX.dll".ToCharArray());

            Dictionary<string, string> bundlePaths = new Dictionary<string, string>()
    {
        { "sb_step_1", folderLocation + "sb_step_1" },
        { "sb_step_2", folderLocation + "sb_step_2" }
    };

            foreach (var bundleEntry in bundlePaths)
            {
                var bundleName = bundleEntry.Key;
                var bundlePath = bundleEntry.Value;

                var bundle = AssetBundle.LoadFromFile(bundlePath);
                if (bundle != null)
                {
                    mls.LogInfo($"------------Successfully loaded {bundleName} asset bundle------------");
                    FootstepSFX.AddRange(bundle.LoadAllAssets<AudioClip>());
                }
                else
                {
                    mls.LogError($"Failed to load {bundleName} asset bundle");
                }
            }
        }
    }
}