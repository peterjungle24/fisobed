using System;
using UnityEngine;
using BepInEx;
using System.Linq;
using RWCustom;
using System.Collections.Generic;
using main;
using circle;

namespace sait
{
    //deal with the SAIT sprite.
    public class SPR_glow_sait : TubeWormGraphics
    {
        public SPR_glow_sait(PhysicalObject ow) : base(ow)
        {
        }

        /// <summary>
        /// Initiate the sprites.
        /// </summary>
        public override void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {
            sLeaser.sprites = new FSprite[5];    //create 5 FSprites
            
            sLeaser.sprites[0] = TriangleMesh.MakeLongMesh(4, false, false);    //I DONT KNOW HOW TO DEAL WITH TRIANGLE MASH I JUST COPIED THIS LINE
            sLeaser.sprites[0].shader = rCam.room.game.rainWorld.Shaders["TubeWorm"];    //use the TubeWorm shader bruh
            sLeaser.sprites[1] = TriangleMesh.MakeLongMesh(4, false, false);    //....
            sLeaser.sprites[2] = TriangleMesh.MakeLongMesh(this.ropeSegments.Length - 1, false, true);    //....
            for (int i = 3; i < 5; i++)    //acess 5 sprite leaser indexes at same time
            {
                sLeaser.sprites[i] = new FSprite("Circle20", true);    //create a circle FSprite
                sLeaser.sprites[i].scaleX = 0.2f;    //scale to [0.2f]
                sLeaser.sprites[i].anchorY = 0f;    //anchor it to [of]
            }
            this.AddToContainer(sLeaser, rCam, null);    //add to the container
            base.InitiateSprites(sLeaser, rCam);    //and initialize the sprites
        }

        /// <summary>
        /// Draw the sprites (I CAN'T READ THIS ENTIRE METHOD HELP)
        /// </summary>
        public override void DrawSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {
            base.DrawSprites(sLeaser, rCam, timeStacker, camPos);    //different orig time
        }

        /// <summary>
        /// Add the sprite to Container for show in-game
        /// </summary>
        public override void AddToContainer(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {
            sLeaser.RemoveAllSpritesFromContainer();    //remove all the sprites from the container
            newContatiner = rCam.ReturnFContainer("Midground");    //new container
        }

        /// <summary>
        /// Apply to the palette
        /// </summary>
        public override void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {
            base.ApplyPalette(sLeaser, rCam, palette);    //orig weeeeeeeeeeeeeeeeeeeeeee
        }
    }
}
