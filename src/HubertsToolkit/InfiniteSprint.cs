using GameNetcodeStuff;
using HarmonyLib;

namespace HubertsToolkit;

[HarmonyPatch(typeof(PlayerControllerB), "Update")]
internal static class InfiniteSprint
{
    [HarmonyPostfix]
    private static void Postfix(ref float ___sprintMeter)
    {
        ___sprintMeter = 1f;
    }
}
