using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using System.IO;
using System;
using _enum;
using circle;
using help;

namespace Fisobed_v2
{

    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {

        public const string PLUGIN_GUID = "slugg.fisobs.mod";
        public const string PLUGIN_NAME = "fisobed 2.0";
        public const string PLUGIN_VERSION = "1.0.1";

        public static new ManualLogSource Logger { get; private set; }
        public static string ION;

        public void OnEnable()
        {

            Logger = base.Logger;                                                   //logger
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;                        //logs on the initialize.
            On.MultiplayerUnlocks.ctor += ctor_ctor;                                //? [ circle ]
            On.RainWorld.Awake += awake;                                            //? [ circle ]
            On.MultiplayerUnlocks.SandboxItemUnlocked += ahh;                       //? [ circle ]
            On.RainWorld.OnModsInit += load_image;                                  //load the image before this thing can be used
            On.ItemSymbol.SpriteNameForItem += sprite_name_for_item_RRRRRRRR;       //maybe add the icon, but
            On.SandboxGameSession.SpawnItems += spawn_circle;                       //autoexplain name
            On.Player.Grabability += grab_the_crap;

        }

        #region RainWorld.OnModsInit

        //logs on the initialize.
        private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {

            orig(self);

            Logger.LogInfo("its actived? great ----------------------------");
            Logger.LogError("its actived? great ----------------------------");
            Logger.LogWarning("its actived? great ----------------------------");
            Debug.Log("Its actived? Great -----------------------------");

        }

        #endregion
        #region load_imamge

        //load the image before this thing can be used
        private void load_image(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {

            orig(self);                                                                                 //call the orig before of all
            ION = Path.Combine("icons", "item_circle_icon");                                     //a string variable for specify the path. WAS A HELL FOR THIS IMAGE WORK

            try                                                                                             //if it works
            {

                Futile.atlasManager.LoadImage(ION);                                                             //ATLAS FUCKING this in image the load [ read in other way ]

                if (Futile.atlasManager.DoesContainElementWithName(ION))                                        // if was registered
                {

                    Debug.Log("This PNG will be actived.");                                                     // log
                    Logger.LogInfo("This PNG will be actived");

                }

            }
            catch (Exception cu)                                                                            //if it doesnt work
            {

                string assetPath = AssetManager.ResolveFilePath(ION + ".png");                                 //string about the image for the Exception

                if (!File.Exists(assetPath))                                                                    //if the file doesnt existe
                {

                    Logger.LogError("this png could not be found at path " + assetPath);                            //loggers poggers

                }

                Logger.LogError(cu);                                                                            //i placed this thing for just prevent problems :3 (useless or no, i will keep)

            }

        }

        #endregion
        #region sprite_name_for_item_RRRRRRRR

        //maybe this thing can add the icon, idk
        private string sprite_name_for_item_RRRRRRRR(On.ItemSymbol.orig_SpriteNameForItem orig, AbstractPhysicalObject.AbstractObjectType itemType, int intData)
        {

            string icon_name = ION;

            if (itemType == _enum.enum_.AbstractObjectType.circle_object)       //if item type its equal of [ circle_abstract ]
            {

                Logger.LogInfo("AirFryer DETECTED: ");                          //log
                return help.files.GetProperElementName(icon_name);                                               //return Sprite Name or Icon or whatever

            }

            return orig(itemType, intData);                                     //orig

        }

        #endregion
        #region ahh

        //? [ circle ]
        private bool ahh(On.MultiplayerUnlocks.orig_SandboxItemUnlocked orig, MultiplayerUnlocks self, MultiplayerUnlocks.SandboxUnlockID unlockID)
        {

            var locked = _enum.enum_.SandboxUnlock.circle_sandbox;              //variable that makes refernece of the SandboxUnlock

            if (unlockID == locked)                                             //if this [ unlockedID ] its equal to [ locked ]
            {

                return true;                                                    //return True

            }

            return orig(self, unlockID);                                        //orig

        }

        #endregion
        #region ctor_ctor

        //? [ circle ]
        private void ctor_ctor(On.MultiplayerUnlocks.orig_ctor orig, MultiplayerUnlocks self, PlayerProgression progression, System.Collections.Generic.List<string> allLevels)
        {

            var absObj_value = AbstractPhysicalObject.AbstractObjectType.values;                                //Abstract values

            if (absObj_value.entries.Contains(_enum.enum_.AbstractObjectType.circle_object.value))              //if the absValue contains the value of [ circle_object]  
            {

                Logger.LogInfo("absObj_value " + absObj_value);                                                 //log

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

        //? [ circle ]
        private void awake(On.RainWorld.orig_Awake orig, RainWorld self)
        {

            orig(self);     //CALLS THIS ORIG BEFORE THE LIST

            var l_circle_san = _enum.enum_.SandboxUnlock.circle_sandbox;                //variable for store the enum [ circle ] Sandbox
            var l_circle_abs = _enum.enum_.AbstractObjectType.circle_object;            //variable for store the enum [ circle ] Abstract

            MultiplayerUnlocks.ItemUnlockList.Add(l_circle_san);                        //add to the list


        }

        #endregion
        #region circle_spawn

        //handle the spawner
        private void spawn_circle(On.SandboxGameSession.orig_SpawnItems orig, SandboxGameSession self, IconSymbol.IconSymbolData data, WorldCoordinate pos, EntityID entityID)
        {

            var absol = _enum.enum_.AbstractObjectType.circle_object;           //variable for the abstract object

            if (data.itemType == absol)
            {

                var abs_object = new circle_abstract(self.game.world, pos, entityID);

                self.game.world.GetAbstractRoom(0).AddEntity(abs_object);

            }

            orig(self, data, pos, entityID);

        }

        #endregion
        #region grab_the_crap

        //allow you to pick this most crap ever
        private Player.ObjectGrabability grab_the_crap(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {

            if (obj is circle.Circle)
            {

                return Player.ObjectGrabability.OneHand;

            }

            return orig(self, obj);

        }

        #endregion

    }

}