#region using

using BepInEx.Logging;
using Vector2 = UnityEngine.Vector2;
using _enum;
using RWCustom;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Random = UnityEngine.Random;
using BepInEx;
using DevInterface;
using CreatureType = CreatureTemplate.Type;
using static PathCost.Legality;
using Fisobs.Core;
using Fisobs.Creatures;
using Fisobs.Sandbox;
using Fisobs.Items;
using Fisobs.Properties;
using System.Runtime.CompilerServices;
using MoreSlugcats;
using HUD;
using circle;
using System.Drawing;

#endregion

namespace _enum
{

    public static class enum_
    {

        //adds the Sandbox Unlocks maybe.
        public static class SandboxUnlock
        {

            public static readonly MultiplayerUnlocks.SandboxUnlockID circle_sandbox = new("circle_IDK", true);    //add [ circle_sandbox ]

        }


        //adds the Abstract Objects maybe...
        public static class AbstractObjectType
        {

            public static readonly AbstractPhysicalObject.AbstractObjectType circle_object = new("circle_IDK", true);   //add [ circle_object ]

        }

    }

}