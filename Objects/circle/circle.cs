#region using

using BepInEx;
using RWCustom;
using UnityEngine;
using Random = UnityEngine.Random;
using MoreSlugcats;
using System.IO;
using main;
using System;
using BepInEx.Logging;

#endregion

namespace circle
{

    public class Circle : PlayerCarryableItem, IDrawable
    {

        private Vector2 rotation;
        private Vector2 lastRotation;
        private Vector2? setRotation;

        #region ctor of my circle. will have a [ ctor_ ]

        //ctor of my Circle.
        public Circle(AbstractPhysicalObject abstractPhysicalObject, Vector2 lastRotation = default, Vector2 rotation = default, Vector2? setRotation = null) : base(abstractPhysicalObject)
        {

            bodyChunks = new BodyChunk[1];
            bodyChunks[0] = new BodyChunk(this, 0, new Vector2(0f, 0f), 8f, 0.2f);
            bodyChunkConnections = new BodyChunkConnection[0];
            airFriction = 0.999f;
            gravity = 1f;
            bounce = 1f;
            surfaceFriction = 0.4f;
            collisionLayer = 2;
            waterFriction = 0.98f;
            buoyancy = 0.4f;
            firstChunk.loudness = 7f;

            SetLastRotation(lastRotation);
            SetRotation(rotation);
            SetSetRotation(setRotation);

            Debug.Log("---->  public Circle actived, maybe? idk, ask for it.");

        }

        #endregion
        #region can be throwed by Player. Empty

        //can be throwed by Player. Empty
        public void ThrowByPlayer()
        {
        }

        #endregion
        
        #region get; set;
        
        //get and set the rotation...? idk more
        public Vector2? GetSetRotation() => setRotation;

        //set the rotation.
        public void SetSetRotation(Vector2? value) => setRotation = value;

        //get the last rotation.
        public Vector2 GetLastRotation() => lastRotation;

        //set the last rotation
        public void SetLastRotation(Vector2 value) => lastRotation = value;

        //get the rotation..
        public Vector2 GetRotation() => rotation;

        //set the rotation
        public void SetRotation(Vector2 value) => rotation = value;

        //dark.
        public float LastDarkness { get; set; }

        //dark.
        public float Darkness { get; set; }
        #endregion

        #region place in a rom

        //place the object in a room
        public override void PlaceInRoom(Room placeRoom)
        {

            base.PlaceInRoom(placeRoom);

            Vector2 center = placeRoom.MiddleOfTile(abstractPhysicalObject.pos);

            int i = 0;
            bodyChunks[i].HardSetPosition(new Vector2(1, 1) * 20f + center);

        }

        #endregion
        #region Initiate the sprites.

        //initiate the sprites
        public void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {

            sLeaser.sprites = new FSprite[1];
            sLeaser.sprites[0] = new FSprite(main.Plugin.ION, true);
            AddToContainer(sLeaser, rCam, null);    //why, bro

        }

        #endregion
        #region draw the sprites.

        //draw the sprites here. i literally understood almost nothing here.
        public void DrawSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {

            Vector2 vector = Vector2.Lerp(firstChunk.lastPos, firstChunk.pos, timeStacker);
            Vector2 v = Vector3.Slerp(GetLastRotation(), GetRotation(), timeStacker);

            LastDarkness = Darkness;

            Darkness = rCam.room.Darkness(vector) * (1f - rCam.room.LightSourceExposure(vector));

            if (Darkness != LastDarkness)
            {

                ApplyPalette(sLeaser, rCam, rCam.currentPalette);

            }

            for (int i = 0; i < Math.Min(4, sLeaser.sprites.Length); i++)
            {

                sLeaser.sprites[i].x = vector.x - camPos.x;
                sLeaser.sprites[i].y = vector.y - camPos.y;
                sLeaser.sprites[i].rotation = Custom.VecToDeg(v);
                sLeaser.sprites[0].element = Futile.atlasManager.GetElementWithName(main.Plugin.ION);

            }

            if (blink > 0 && Random.value < 0.5f)
            {

                sLeaser.sprites[0].color = blinkColor;

            }
            else
            {

                sLeaser.sprites[0].color = color;                       //sus

            }

            if (slatedForDeletetion || room != rCam.room)
            {

                sLeaser.CleanSpritesAndRemove();

            }

        }

        #endregion
        #region apply palettes

        //apply the palettes.
        public void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {

            color = UnityEngine.Color.Lerp(new Color(255, 255, 255), palette.blackColor, Darkness);

            sLeaser.sprites[0].color = color;

        }

        #endregion
        #region add to te container.

        //add to the container
        public void AddToContainer(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {

            newContatiner ??= rCam.ReturnFContainer("Items");

            foreach (FSprite fsprite in sLeaser.sprites)
            {

                newContatiner.AddChild(fsprite);

            }

        }

        #endregion

    }

}