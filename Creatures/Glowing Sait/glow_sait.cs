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

        //If you use the same string name, you can use it like ID in World File [ world_PD.txt ]
        public static readonly CreatureTemplate.Type TT_glow_sait = new CreatureTemplate.Type("glow_sait", true);

    }

    #endregion
    #region T = Template

    //SAIT template
    public class T_sait : CreatureTemplate
    {

        public T_sait(CreatureTemplate.Type sait_type, CreatureTemplate ancestor, List<TileTypeResistance> tileResistances, List<TileConnectionResistance> connectionResistances, CreatureTemplate.Relationship defaultRelationship) : base(sait_type, ancestor, tileResistances, connectionResistances, defaultRelationship)
        {

            this.name = "Glowing Sait";
            this.AI = true;
            this.canFly = true;
            this.canSwim = false;
            this.grasps = 1;

            base.shortcutColor = Color.green;
            base.smallCreature = true;
            base.type = type;

        }

    }

    #endregion
    #region C = Creature

    //SAIT creature
    public class C_glow_sait : TubeWorm
    {

        public C_glow_sait(AbstractCreature abstractCreature, World world) : base(abstractCreature, world)
        {
        }

        public override void InitiateGraphicsModule()
        {

            if (base.graphicsModule == null)
            {

                base.graphicsModule = new TubeWormGraphics(this);

            }

        }

    }

    #endregion

    public class H_sait
    {

        /// <summary>
        /// apply the palette to the Sait :3
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="palette"></param>
        public static void tubeworm_applyP(On.TubeWormGraphics.orig_ApplyPalette orig, TubeWormGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {

            orig(self, sLeaser, rCam, palette);                                         //call the orig

            if (self.worm.Template.type == TT_sait.TT_glow_sait)
            {

                sLeaser.sprites[0].color = new Color(0.6666667f, 0.94509804f, 0.3372549f);  //change color?
                sLeaser.sprites[sLeaser.sprites.Length - 1].color = Color.black;            //i think it changess the color here too

            }

        }

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        /// <param name="ow"></param>
        public static void tubeworm_ctor(On.TubeWormGraphics.orig_ctor orig, TubeWormGraphics self, PhysicalObject ow)
        {

            orig.Invoke(self, ow);

        }

        /// <summary>
        /// initiate the sprites to The Sait
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        public static void tubeworm_initS(On.TubeWormGraphics.orig_InitiateSprites orig, TubeWormGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {

            orig.Invoke(self, sLeaser, rCam);

            if (self.worm.Template.type == TT_sait.TT_glow_sait)
            {

                Array.Resize<FSprite>(ref sLeaser.sprites, sLeaser.sprites.Length + 1);
                sLeaser.sprites[sLeaser.sprites.Length - 1] = new FSprite("FaceB0", true);
                sLeaser.sprites[sLeaser.sprites.Length - 1].color = new Color(160, 2, 2);
                sLeaser.sprites[sLeaser.sprites.Length - 1].scale = 1.3f;
                sLeaser.sprites[0].shader = FShader.defaultShader;
                rCam.ReturnFContainer("Midground").AddChild(sLeaser.sprites[sLeaser.sprites.Length - 1]);

            }

        }

        /// <summary>
        /// draw the sprites. Its a updatable
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="timeStacker"></param>
        /// <param name="camPos"></param>
        public static void tubeworm_drawS(On.TubeWormGraphics.orig_DrawSprites orig, TubeWormGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {

            orig.Invoke(self, sLeaser, rCam, timeStacker, camPos);

            if (self.worm.Template.type == TT_sait.TT_glow_sait)
            {

                sLeaser.sprites[0].alpha = 1f;
                sLeaser.sprites[sLeaser.sprites.Length - 1].SetPosition((Vector2.Lerp(self.bodyParts[2].lastPos, self.bodyParts[2].pos, timeStacker) + Vector2.Lerp(self.bodyParts[1].lastPos, self.bodyParts[1].pos, timeStacker)) / 2f - camPos);
                sLeaser.sprites[sLeaser.sprites.Length - 1].MoveToFront();

            }

        }

        /// <summary>
        /// add the thing to container.
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="newContatiner"></param>
        public static void tubeworm_container(On.TubeWormGraphics.orig_AddToContainer orig, TubeWormGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {

            orig.Invoke(self, sLeaser, rCam, newContatiner);

        }

    }

}