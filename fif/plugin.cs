#region using

using BepInEx.Logging;
using RWCustom;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Random = UnityEngine.Random;
using BepInEx;
using DevInterface;
using Fisobs.Core;
using Fisobs.Creatures;
using Fisobs.Sandbox;
using Fisobs.Items;
using Fisobs.Properties;
using MoreSlugcats;
using _enum;
using System.IO;

#endregion

namespace objs
{

    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]         //you know
    public class Plugin : BaseUnityPlugin
    {

        public const string PLUGIN_GUID = "slugg.fisobs.testing";               //the ID for the my mod in [ modinfo.json ]
        public const string PLUGIN_NAME = "fisobed";           //the name for my mod in [ modinfo.json ]
        public const string PLUGIN_VERSION = "1.2.2";               //the Version for my mod in [ modinfo.json ]

        public void OnEnable()
        {

            On.RainWorld.OnModsInit += log_test;
            On.MultiplayerUnlocks.SandboxItemUnlocked += @locked;
            On.RainWorld.Awake += awake;
            On.MultiplayerUnlocks.ctor += ctor_ctor;

        }

        #region ctor_ctor

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

        #endregion
        #region awake

        private void awake(On.RainWorld.orig_Awake orig, RainWorld self)    //for add unlocks for the list. Fisobs are so confusing...
        {
            
            orig(self);     //CALLS THIS ORIG BEFORE THE LIST

            var l_circle_san = _enum.enum_.SandboxUnlock.circle_sandbox;                //variable for store the enum [ circle ] Sandbox
            var l_circle_abs = _enum.enum_.AbstractObjectType.circle_object;            //variable for store the enum [ circle ] Abstract

            MultiplayerUnlocks.ItemUnlockList.Add(l_circle_san);                        //add to the list

        }

        #endregion
        #region @locked

        private bool @locked(On.MultiplayerUnlocks.orig_SandboxItemUnlocked orig, MultiplayerUnlocks self, MultiplayerUnlocks.SandboxUnlockID unlockID)
        {

            //the rest you have to understand

            var locked = _enum.enum_.SandboxUnlock.circle_sandbox;

            if(unlockID == locked)
            {

                return true;

            }

            return orig(self, unlockID);

        }

        #endregion
        #region log_test

        private void log_test(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {

            Logger.LogInfo("[[[[ FISOBED ]]]]     its actived? great ----------------------------");

            Debug.Log("[[[[ FISOBED ]]]]     its actived? great ----------------------------");

            orig(self);

        }

        #endregion

    }

}