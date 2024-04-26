using BepInEx;
using BepInEx.Logging;
using UnityEngine;

namespace Fisobed_v2
{

    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {

        public const string PLUGIN_GUID = "slugg.fisobs.mod";
        public const string PLUGIN_NAME = "fisobed 2";
        public const string PLUGIN_VERSION = "1.0.0";

        public static new ManualLogSource Logger { get; private set; }

        public void OnEnable()
        {

            Logger = base.Logger;
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;
            On.MultiplayerUnlocks.ctor += ctor_ctor;
            On.RainWorld.Awake += awake;
            On.MultiplayerUnlocks.SandboxItemUnlocked += ahh;

        }


        private bool ahh(On.MultiplayerUnlocks.orig_SandboxItemUnlocked orig, MultiplayerUnlocks self, MultiplayerUnlocks.SandboxUnlockID unlockID)
        {

            var locked = _enum.enum_.SandboxUnlock.circle_sandbox;

            if (unlockID == locked)
            {

                return true;

            }

            return orig(self, unlockID);

        }


        private void ctor_ctor(On.MultiplayerUnlocks.orig_ctor orig, MultiplayerUnlocks self, PlayerProgression progression, System.Collections.Generic.List<string> allLevels)
        {

            var absObj_value = AbstractPhysicalObject.AbstractObjectType.values;

            if (absObj_value.entries.Contains(_enum.enum_.AbstractObjectType.circle_object.value))
            {

                Logger.LogInfo("absObj_value " + absObj_value);

            }

            //log these too
            int valueTypeCount = absObj_value.Count;
            int circle_UnlockIDIndex = (int)MultiplayerUnlocks.SymbolDataForSandboxUnlock(_enum.enum_.SandboxUnlock.circle_sandbox).itemType;

            circle_UnlockIDIndex = 49;

            Logger.LogInfo("valueTypeCount: " + valueTypeCount);
            Logger.LogInfo("circle_UnlockIDIndex" + circle_UnlockIDIndex);

            orig(self, progression, allLevels);

        }


        private void awake(On.RainWorld.orig_Awake orig, RainWorld self)
        {

            orig(self);     //CALLS THIS ORIG BEFORE THE LIST

            var l_circle_san = _enum.enum_.SandboxUnlock.circle_sandbox;                //variable for store the enum [ circle ] Sandbox
            var l_circle_abs = _enum.enum_.AbstractObjectType.circle_object;            //variable for store the enum [ circle ] Abstract

            MultiplayerUnlocks.ItemUnlockList.Add(l_circle_san);                        //add to the list


        }


        private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {

            orig(self);
            Logger.LogInfo("its actived? great ----------------------------");
            Logger.LogError("its actived? great ----------------------------");
            Logger.LogWarning("its actived? great ----------------------------");
            Debug.Log("Its actived? Great -----------------------------");

        }

    }

}