using GameNetcodeStuff;
using HarmonyLib;

namespace HubertsToolkit;

[HarmonyPatch(typeof(PlayerControllerB), "Update")]
internal static class InfiniteSprintPatch
{
    [HarmonyPostfix]
    private static void Postfix(ref float ___sprintMeter)
    {
        if (!Plugin.InfiniteSprint.Value)
        {
            return;
        }

        ___sprintMeter = 1f;
    }
}
