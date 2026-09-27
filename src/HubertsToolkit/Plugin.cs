using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace HubertsToolkit;

[BepInAutoPlugin(id: "Huberts.Toolkit", name: "Hubert's Toolkit")]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;

    internal static ConfigEntry<bool> InfiniteSprint { get; private set; } = null!;
    internal static ConfigEntry<bool> InfiniteFlashlight { get; private set; } = null!;

    private void Awake()
    {
        Log = Logger;

        InfiniteSprint = Config.Bind(
            "General",
            "InfiniteSprint",
            true,
            "Keep the sprint meter full.");

        InfiniteFlashlight = Config.Bind(
            "General",
            "InfiniteFlashlight",
            true,
            "Refill a flashlight battery while you are using it.");

        Log.LogMessage($"{Id} has loaded successfully.");
        Harmony.CreateAndPatchAll(typeof(InfiniteSprintPatch));
        Harmony.CreateAndPatchAll(typeof(InfiniteFlashlightPatch));
    }
}
