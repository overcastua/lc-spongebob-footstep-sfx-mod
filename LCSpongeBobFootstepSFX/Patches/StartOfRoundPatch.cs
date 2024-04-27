using HarmonyLib;
using LCSpongeBobFootstepSFX;
using System.Linq;

namespace LCSpongeBobFootstepSFX.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void OverrideAudio(StartOfRound __instance)
        {
            _ = OverrideFootstepAudio(__instance);
        }

        private static FootstepSurface[] OverrideFootstepAudio(StartOfRound startOfRoundInstance)
        {
            var newFootstepsSurfaceArray = new FootstepSurface[startOfRoundInstance.footstepSurfaces.Length];

            for (int i = 0; i < startOfRoundInstance.footstepSurfaces.Length; i++)
            {
                newFootstepsSurfaceArray[i] = startOfRoundInstance.footstepSurfaces[i];
                newFootstepsSurfaceArray[i].clips = LCSpongeBobFootstepSFXBase.FootstepSFX.ToArray();
            }

            return newFootstepsSurfaceArray;
        }

    }
}
