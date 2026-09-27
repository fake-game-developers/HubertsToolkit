using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HubertsToolkit
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class HubertsToolkitBase : BaseUnityPlugin // MODNAME : BaseUnityPlugin
    {
        public const string modGUID = "Huberts.Toolkit"; // mod name
        public const string modName = "Lethal Company - Hubert's Toolkit";
        public const string modVersion = "1.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);
        public static HubertsToolkitBase Instance; // instantiate base class
        internal ManualLogSource console; // console instance for use throughout


        void Awake()
        {
            if ((Object)(object)Instance == (Object)null) // if base class instance is null
            {
                Instance = this; // assign this to Instance
            }

            console = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            console.LogMessage(modGUID + " has loaded successfully."); // show mod loaded output in BepInEx console

            Config.SaveOnConfigSet = false;

            harmony.PatchAll(typeof(HubertsToolkitBase)); // mod base class 
            harmony.PatchAll(typeof(infiniteSprint)); // mod "infiniteSprint"
            harmony.PatchAll(typeof(infiniteFlashlight)); // mod "infiniteFlashlight"
        }
    }

    [HarmonyPatch(typeof(PlayerControllerB))] // PlayerControllerB handles player movement
    [HarmonyPatch("Update")] // update method handles movement for every frame
    class infiniteSprint // mod class name
    {
        [HarmonyPostfix] // run mod after the PlayerController Update void has executed
        static void Postfix(ref float ___sprintMeter) // sprintmeter handles the time left to sprint
        {
            ___sprintMeter = 1f; // set sprintMeter to full
        }
    }


    [HarmonyPatch(typeof(FlashlightItem))]
    internal class infiniteFlashlight
    {
        [HarmonyPatch("Update")] // target the update method
        [HarmonyPostfix]
        private static void Postfix(FlashlightItem __torch)
        {
            if (!((Object)(object)__torch == (Object)null)  // if flashlightitem is not null
                    && ((NetworkBehaviour)__torch).IsOwner // and player is owner of the torch/flash/whatever you wanna call it...
                    && ((GrabbableObject)__torch).isBeingUsed  // and the flashlight is being used
                    && ((GrabbableObject)__torch).itemProperties.requiresBattery  // and the torch requires battery
                    && ((GrabbableObject)__torch).insertedBattery.charge > 0f // and insertedBattery.charge property is greater than 0f
                    && !((GrabbableObject)__torch).itemProperties.itemIsTrigger) // and itemIsTrigger is set to false
            {
                //Battery insertedBattery = ((GrabbableObject)__torch).insertedBattery; // store the insertedBattery into an object
                ((GrabbableObject)__torch).insertedBattery.charge += Time.deltaTime / ((GrabbableObject)__torch).itemProperties.batteryUsage; // adjust the amount of charge so that battery usage does not decrease
            }
        }
    }
}