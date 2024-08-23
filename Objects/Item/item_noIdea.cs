using System;
using BepInEx;
using UnityEngine;

namespace Item
{

    public class item_noIdea : PlayerCarryableItem, IDrawable
    {

        private Vector2 rotation;
        private Vector2 lastRotation;
        private Vector2? setRotation;
        public Vector2? GetSetRotation() => setRotation;
        public void SetSetRotation(Vector2? value) => setRotation = value;
        public Vector2 GetLastRotation() => lastRotation;
        public void SetLastRotation(Vector2 value) => lastRotation = value;
        public Vector2 GetRotation() => rotation;
        public void SetRotation(Vector2 value) => rotation = value;
        public float LastDarkness { get; set; }
        public float Darkness { get; set; }
        
        /// <summary>
        /// ctor of my Circle.
        /// </summary>
        /// <param name="abstractPhysicalObject"> the object</param>
        /// <param name="lastRotation"> last rotation. </param>
        /// <param name="rotation"> rotation </param>
        /// <param name="setRotation"> set the rotation </param>
        public item_noIdea(AbstractPhysicalObject abstractPhysicalObject, Vector2 lastRotation = default, Vector2 rotation = default, Vector2? setRotation = null) : base(abstractPhysicalObject)
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

        }

        /// <summary>
        /// can be throwed by Player. It's empty
        /// </summary>
        public void ThrowByPlayer()
        {
        }


        /// <summary>
        /// place the object in a room
        /// </summary>
        /// <param name="placeRoom"> room for place the object </param>
        public override void PlaceInRoom(Room placeRoom)
        {

            base.PlaceInRoom(placeRoom);

            Vector2 center = placeRoom.MiddleOfTile(abstractPhysicalObject.pos);

            int i = 0;
            bodyChunks[i].HardSetPosition(new Vector2(1, 1) * 20f + center);

        }


        //IDrawable methods

        /// <summary>
        /// initiate the sprites of the object
        /// </summary>
        /// <param name="sLeaser"> sLeaser for point to a sprite i guess. </param>
        /// <param name="rCam"> Room Camera for the camera to the room.... </param>
        public void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {
            sLeaser.sprites = new FSprite[1];
            sLeaser.sprites[0] = new FSprite("Futile_White", true);
         
            AddToContainer(sLeaser, rCam, null);
        }

        /// <summary>
        /// draw the sprites here. i literally understood almost nothing here.
        /// </summary>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="timeStacker"></param>
        /// <param name="camPos"></param>
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
        }


        /// <summary>
        /// apply to Palette.
        /// </summary>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="palette"></param>
        public void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {
            color = UnityEngine.Color.Lerp(new Color(255, 255, 255), palette.blackColor, Darkness);
            sLeaser.sprites[0].color = color;
        }
        /// <summary>
        /// add to the container
        /// </summary>
        /// <param name="sLeaser"></param>
        /// <param name="rCam"></param>
        /// <param name="newContatiner"></param>
        public void AddToContainer(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {
            newContatiner ??= rCam.ReturnFContainer("Items");

            foreach (FSprite fsprite in sLeaser.sprites)
            {
                newContatiner.AddChild(fsprite);
            }
        }
    }
}
