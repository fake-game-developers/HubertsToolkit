using HarmonyLib;
using UnityEngine;

namespace HubertsToolkit;

[HarmonyPatch(typeof(FlashlightItem), "Update")]
internal static class InfiniteFlashlightPatch
{
    [HarmonyPostfix]
    private static void Postfix(FlashlightItem __instance)
    {
        if (!Plugin.InfiniteFlashlight.Value)
        {
            return;
        }

        if (__instance == null || !__instance.IsOwner || !__instance.isBeingUsed)
        {
            return;
        }

        var item = __instance.itemProperties;
        if (item == null || !item.requiresBattery || item.itemIsTrigger || item.batteryUsage <= 0f)
        {
            return;
        }

        var battery = __instance.insertedBattery;
        if (battery == null || battery.charge <= 0f)
        {
            return;
        }

        battery.charge += Time.deltaTime / item.batteryUsage;
    }
}
