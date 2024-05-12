using System;
using UnityEngine;
using BepInEx;
using System.Linq;
using RWCustom;
using System.Collections.Generic;
using _enum;
using circle;

namespace sait
{

    //deal with the SAIT sprite.
    public class SPR_glow_sait : TubeWormGraphics, IDrawable
    {

        public SPR_glow_sait(PhysicalObject ow) : base(ow)
        {
        }

        public override void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {

            sLeaser.sprites = new FSprite[1];                                   //create a FSprite, for create a sprite.
            sLeaser.sprites[0] = new FSprite(Fisobed_v2.Plugin.ION, true);      //creates a new FSprite with the sprite path.... and register it
            AddToContainer(sLeaser, rCam, null);                                //add to the container for see it

        }

        public override void DrawSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {
        }

        public override void AddToContainer(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer f_container)
        {
        }

        public override void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {
        }

    }

}