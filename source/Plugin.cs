using BepInEx;
using BepInEx.Logging;
using sait;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace main
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "slugg.fisobs.mod";
        public const string PLUGIN_NAME = "fisobed 2.0";
        public const string PLUGIN_VERSION = "1.0.1";

        //my fields.

        public static new ManualLogSource Logger { get; private set; }
        public static string ION;
        public static string creature = "sait";

        /// <summary>
        /// when the mod its enabled, will load hooks.
        /// </summary>
        public void OnEnable()
        {
            //logs on "mods.txt"
            Logger = base.Logger;

            #region Creatures

            // functional
            On.StaticWorld.InitCustomTemplates += init_custom_template;             // init the custom palette for spawn i guess
            On.StaticWorld.InitStaticWorld += init_static_world;                    // init the static world for add to the list 
            On.MultiplayerUnlocks.UnlockedCritters += unlock_sait;                  // creates a unlock for Sait
            On.RainWorld.Awake += awake_sait;                                       // add to the CreatureUnlock list
            On.CreatureSymbol.SpriteNameOfCreature += sprite_sait;                  // add the TubeWorm sandbox icon to the unlock.

            // graphics
            On.TubeWormGraphics.ApplyPalette += H_sait.tubeworm_applyP;             // apply the palette to the sprite.
            On.TubeWormGraphics.InitiateSprites += H_sait.tubeworm_initS;           // initiate the sprites for initiate the sprites and work
            On.TubeWormGraphics.DrawSprites += H_sait.tubeworm_drawS;               // Draw sprites.
            On.TubeWormGraphics.ctor += H_sait.tubeworm_ctor;                       // ctor
            On.TubeWormGraphics.AddToContainer += H_sait.tubeworm_container;        // add the sprite to the container

            #endregion
        }

        #region load_imamge

        //load the image before this thing can be used
        private void load_image(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);                                                                                 //call the orig before of all
            ION = Path.Combine("icons", "item_circle_icon");                                            //a string variable for specify the path. WAS A HELL FOR THIS IMAGE WORK

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
        #region glowing sait

        //creation

        #region awake_sait

        /// <summary>
        /// unlock awake process for Sait
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        private void awake_sait(On.RainWorld.orig_Awake orig, RainWorld self)
        {
            try
            {
                Logger.LogInfo("Sait is awaken");

                orig(self);

                //UNLOCK IDS NEED TO BE SAME CREATURETEMPLATE.TYPE IN STRINGS 
                var saited = enums.SandboxUnlock.un_glow_sait;                  //variable for store the enum [ glow_sait ] Unlock
                MultiplayerUnlocks.CreatureUnlockList.Add(saited);              //add to the list
            }
            catch (Exception ex)
            {
                Logger.LogError(@"<awake_sait> exception from Catch: " + ex);
            }
        }

        #endregion
        #region sprite_sait

        /// <summary>
        /// add the Icon sprite to the Icon sandbox unlock of Sait wqowowoowowoow
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="iconData"></param>
        /// <returns></returns>
        private string sprite_sait(On.CreatureSymbol.orig_SpriteNameOfCreature orig, IconSymbol.IconSymbolData iconData)
        {
            var saited = TT_sait.TT_glow_sait;      //assign the CreatureTemplate.Type for the variable.

            //add the Sait for the critType on IconData
            if (iconData.critType == saited)
            {
                return "Kill_Tubeworm";     //i think it will add the Unlock icon.
            }

            return orig(iconData);
        }

        #endregion
        #region init_custom_template

        //a template
        private void init_custom_template(On.StaticWorld.orig_InitCustomTemplates orig)
        {
            try
            {
                var sait_type = TT_sait.TT_glow_sait;   //the CreatureTemplate.Type of sait

                orig();                                                                                                         //orig

                var sait_ancestor = StaticWorld.GetCreatureTemplate(CreatureTemplate.Type.TubeWorm);                            //get the creature template
                var sait_relationaship = new CreatureTemplate.Relationship(CreatureTemplate.Relationship.Type.Ignores, 0f);     //get the Relationaship from the creature
                var tile_res = new List<TileTypeResistance>();                                                                  //get the tile resistance
                var tile_con = new List<TileConnectionResistance>();                                                            //get the tile connection resistance
                var sait = new sait.T_sait(sait_type, sait_ancestor, tile_res, tile_con, sait_relationaship);                   //THE SAIT

                StaticWorld.creatureTemplates[sait_type.Index] = sait;
            }
            catch(Exception me)
            {
                Logger.LogError("<init_custom_template> TryCatch was sucefully FAILED");
                Logger.LogError(me);
                Logger.LogError(me.StackTrace);
            }
        }

        #endregion
        #region init static world

        private void init_static_world(On.StaticWorld.orig_InitStaticWorld orig)
        {
            var tt_sait = sait.TT_sait.TT_glow_sait;
            orig();
        }

        #endregion
        #region unlock_sait

        /// <summary>
        /// Add the unlock for Sait
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private List<CreatureTemplate.Type> unlock_sait(On.MultiplayerUnlocks.orig_UnlockedCritters orig, MultiplayerUnlocks.LevelUnlockID ID)
        {
            var saited = TT_sait.TT_glow_sait;  //add the Sait template here

            Logger.LogInfo("saited before: " + saited.Index);

            //Get list returned by orig containing unlocked CreatureTemplate.Types for a particular unlock group identified by ID
            List<CreatureTemplate.Type> list = orig(ID);

            //Check for the ground that contains the TubeWorm
            if (ID == MultiplayerUnlocks.LevelUnlockID.Default)
            {
                //Add my custom TubeWorm template to the list
                list.Add(saited);
                Logger.LogInfo("SAIT UNLOCK >>>> Saited");
            }
            else
            {
                Logger.LogError("<unlock_sait()>[ID == MultiplayerUnlocks.LevelUnlockID.Default] failed.");
            }

            Logger.LogInfo("saited after: " + saited.Index);
            return list;
        }

        #endregion

        #endregion
    }
}