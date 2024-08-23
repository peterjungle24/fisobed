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
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        public override void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {

            sLeaser.sprites = new FSprite[5];
            sLeaser.sprites[0] = TriangleMesh.MakeLongMesh(4, false, false);
            sLeaser.sprites[0].shader = rCam.room.game.rainWorld.Shaders["TubeWorm"];
            sLeaser.sprites[1] = TriangleMesh.MakeLongMesh(4, false, false);
            sLeaser.sprites[2] = TriangleMesh.MakeLongMesh(this.ropeSegments.Length - 1, false, true);
            for (int i = 3; i < 5; i++)
            {
                sLeaser.sprites[i] = new FSprite("Circle20", true);
                sLeaser.sprites[i].scaleX = 0.2f;
                sLeaser.sprites[i].anchorY = 0f;
            }
            this.AddToContainer(sLeaser, rCam, null);
            base.InitiateSprites(sLeaser, rCam);

        }

        /// <summary>
        /// Draw the sprites (I CAN'T READ THIS ENTIRE METHOD HELP)
        /// </summary>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="timeStacker"></param>
        /// <param name="camPos"></param>
        public override void DrawSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {

            base.DrawSprites(sLeaser, rCam, timeStacker, camPos);

        }

        /// <summary>
        /// Add the sprite to Container for show in-game
        /// </summary>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="f_container"></param>
        public override void AddToContainer(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {

            sLeaser.RemoveAllSpritesFromContainer();
            newContatiner = rCam.ReturnFContainer("Midground");

        }

        /// <summary>
        /// Apply to the palette
        /// </summary>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="palette"></param>
        public override void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {

            base.ApplyPalette(sLeaser, rCam, palette);

        }

    }

}