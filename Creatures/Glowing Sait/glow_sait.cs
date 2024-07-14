using UnityEngine;
using BepInEx;
using System.Linq;
using System;
using RWCustom;
using System.Collections.Generic;
using main;
using circle;

namespace sait
{
    #region TT = Template.Type

    //SAIT template type
    public static class TT_sait
    {
        public static readonly CreatureTemplate.Type TT_glow_sait = new CreatureTemplate.Type("glow_sait", true);    //register the fucking creature into the static readonly field
    }

    #endregion
    #region T = Template

    //SAIT template
    public class T_sait : CreatureTemplate
    {
        public T_sait(CreatureTemplate.Type sait_type, CreatureTemplate ancestor, List<TileTypeResistance> tileResistances, List<TileConnectionResistance> connectionResistances, CreatureTemplate.Relationship defaultRelationship) : base(sait_type, ancestor, tileResistances, connectionResistances, defaultRelationship)
        {

            this.name = "Glowing Sait";    //the creature name -/- string
            this.AI = true;    //if it have AI -/- boolean
            this.canFly = false;    //if it can fly -/- boolean
            this.canSwim = false;    //if it can swim /-/ boolean
            this.grasps = 0;    //how many grasps it have -/- int

            base.shortcutColor = Color.green;    //a default shortcut color /-/ UnityEngine.Color
            base.smallCreature = true;    //if is smol creature -/- boolean
            base.type = type;    //base
        }
    }

    #endregion
    #region C = Creature

    //SAIT creature
    public class C_glow_sait : TubeWorm
    {
        /// mpty
        public C_glow_sait(AbstractCreature abstractCreature, World world) : base(abstractCreature, world)
        {
        }

        /// <summary>
        /// override the InitiateGraphicsModule for add the fucking graphics for the lil sait
        /// </summary>
        public override void InitiateGraphicsModule()
        {
            if (base.graphicsModule == null)    //if this graphics module field is null
            {
                base.graphicsModule = new TubeWormGraphics(this);    //ads the graphics
            }
        }
    }

    #endregion

    public class H_sait
    {

        /// <summary>
        /// [HOOK] apply the palette to the Sait :3
        /// </summary>
        public static void tubeworm_applyP(On.TubeWormGraphics.orig_ApplyPalette orig, TubeWormGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {
            orig(self, sLeaser, rCam, palette);                                         //call the orig

            sLeaser.sprites[0].color = new Color(0.6666667f, 0.94509804f, 0.3372549f);  //change color
            sLeaser.sprites[sLeaser.sprites.Length - 1].color = Color.black;            //changes the color of the rest of the index escept the first one
        }

        /// <summary>
        /// [HOOK] ctor.
        /// </summary>
        public static void tubeworm_ctor(On.TubeWormGraphics.orig_ctor orig, TubeWormGraphics self, PhysicalObject ow)
        {
            orig.Invoke(self, ow);    //it just calls the orig LMAO
        }

        /// <summary>
        /// [HOOK] initiate the sprites to The Sait
        /// </summary>
        public static void tubeworm_initS(On.TubeWormGraphics.orig_InitiateSprites orig, TubeWormGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {
            orig.Invoke(self, sLeaser, rCam);

            //things I don't know vs things I know:
            Array.Resize<FSprite>(ref sLeaser.sprites, sLeaser.sprites.Length + 1);    //????
            sLeaser.sprites[sLeaser.sprites.Length - 1] = new FSprite("FaceB0", true);    //????
            sLeaser.sprites[sLeaser.sprites.Length - 1].color = new Color(160, 2, 2);    //????
            sLeaser.sprites[sLeaser.sprites.Length - 1].scale = 1.3f;    //????
            sLeaser.sprites[0].shader = FShader.defaultShader;    //????
            rCam.ReturnFContainer("Midground").AddChild(sLeaser.sprites[sLeaser.sprites.Length - 1]);    //return and add to the container
        }

        /// <summary>
        /// [HOOK] draw the sprites. Its a updatable
        /// </summary>
        public static void tubeworm_drawS(On.TubeWormGraphics.orig_DrawSprites orig, TubeWormGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {
            orig.Invoke(self, sLeaser, rCam, timeStacker, camPos);    //calls the orig

            sLeaser.sprites[0].alpha = 1f;    //alpha of the sprite
            //i dont will explain the rest
            sLeaser.sprites[sLeaser.sprites.Length - 1].SetPosition((Vector2.Lerp(self.bodyParts[2].lastPos, self.bodyParts[2].pos, timeStacker) + Vector2.Lerp(self.bodyParts[1].lastPos, self.bodyParts[1].pos, timeStacker)) / 2f - camPos);
            sLeaser.sprites[sLeaser.sprites.Length - 1].MoveToFront();
        }

        /// <summary>
        /// [HOOK] add the thing to container.
        /// </summary>
        public static void tubeworm_container(On.TubeWormGraphics.orig_AddToContainer orig, TubeWormGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {
            orig.Invoke(self, sLeaser, rCam, newContatiner);    //call the orig....
        }
    }
}
