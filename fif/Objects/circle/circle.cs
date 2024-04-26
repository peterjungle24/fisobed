#region using

using BepInEx;
using _enum;
using RWCustom;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Random = UnityEngine.Random;
using DevInterface;
using Fisobs.Core;
using Fisobs.Creatures;
using Fisobs.Sandbox;
using Fisobs.Items;
using Fisobs.Properties;
using MoreSlugcats;
using System.IO;

#endregion

namespace circle
{

    public class Circle : PlayerCarryableItem, IDrawable
    {

        private Vector2 rotation;
        private Vector2 lastRotation;
        private Vector2? setRotation;

        public static string circle_icon = Path.Combine("icon", "item_circle_icon");

        public Circle(AbstractPhysicalObject abstractPhysicalObject, Vector2 lastRotation = default, Vector2 rotation = default, Vector2? setRotation = null) : base(abstractPhysicalObject)
        {

            bodyChunks = new BodyChunk[1];
            bodyChunks[0] = new BodyChunk(this, 0, new Vector2(0f, 0f), 8f, 0.2f);
            bodyChunkConnections = new BodyChunkConnection[0];
            airFriction = 0.999f;
            gravity = 0.1f;
            bounce = 0.4f;
            surfaceFriction = 0.4f;
            collisionLayer = 2;
            waterFriction = 0.98f;
            buoyancy = 0.4f;
            firstChunk.loudness = 7f;

            SetLastRotation(lastRotation);
            SetRotation(rotation);
            SetSetRotation(setRotation);

        }

        public void ThrowByPlayer()
        {
        }

        public Vector2? GetSetRotation() => setRotation;    //set rottate

        public void SetSetRotation(Vector2? value) => setRotation = value;

        public Vector2 GetLastRotation() => lastRotation;   //last rotate

        public void SetLastRotation(Vector2 value) => lastRotation = value;

        public Vector2 GetRotation() => rotation;   //rotate

        public void SetRotation(Vector2 value) => rotation = value;


        public float LastDarkness { get; set; }     //dark :/
        public float Darkness { get; set; }         //last dark :3

        public override void PlaceInRoom(Room placeRoom)
        {

            base.PlaceInRoom(placeRoom);

            Vector2 center = placeRoom.MiddleOfTile(abstractPhysicalObject.pos);

            int i = 0;
            bodyChunks[i].HardSetPosition(new Vector2(1, 1) * 20f + center);

        }

        public void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {

            sLeaser.sprites = new FSprite[1];
            sLeaser.sprites[0] = new FSprite(circle_icon, true);
            AddToContainer(sLeaser, rCam, null);    //why, bro

        }

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

            for (int i = 0; i < 4; i++)
            {

                sLeaser.sprites[i].x = vector.x - camPos.x;
                sLeaser.sprites[i].y = vector.y - camPos.y;
                sLeaser.sprites[i].rotation = Custom.VecToDeg(v);
                sLeaser.sprites[0].element = Futile.atlasManager.GetElementWithName(circle_icon);

            }

            if (blink > 0 && Random.value < 0.5f)
            {

                sLeaser.sprites[1].color = blinkColor;

            }
            else
            {

                sLeaser.sprites[1].color = color;

            }

            if (slatedForDeletetion || room != rCam.room)
            {

                sLeaser.CleanSpritesAndRemove();

            }

        }

        public void ApplyPalette(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, RoomPalette palette)
        {

            color = Color.Lerp(new Color(255, 255, 255), palette.blackColor, Darkness);

            sLeaser.sprites[0].color = color;

        }

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