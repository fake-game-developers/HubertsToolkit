using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace HubertsToolkit;

[BepInAutoPlugin(id: "Huberts.Toolkit", name: "Lethal Company - Hubert's Toolkit")]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;

    private void Awake()
    {
        Log = Logger;
        Log.LogMessage($"{Id} has loaded successfully.");
        Harmony.CreateAndPatchAll(typeof(InfiniteSprint));
        Harmony.CreateAndPatchAll(typeof(InfiniteFlashlight));
    }
}
