#region using

using BepInEx.Logging;
using RWCustom;
using UnityEngine;
using BepInEx;
using CreatureType = CreatureTemplate.Type;
using Fisobs.Core;
using Fisobs.Creatures;
using Fisobs.Sandbox;
using Fisobs.Items;
using Fisobs.Properties;
using System.Runtime.CompilerServices;
using MoreSlugcats;
using circle;

#endregion

namespace main
{

    public static class enums
    {

        //adds the Sandbox Unlocks maybe.
        public static class SandboxUnlock
        {

            public static readonly MultiplayerUnlocks.SandboxUnlockID un_circle = new("un_circle", true);               //add the unlock for [ circle_sandbox ]
            public static readonly MultiplayerUnlocks.SandboxUnlockID un_glow_sait = new("glow_sait", true);           //add the unlock for [ glow_sait ]

        }

        //adds the Abstract Objects maybe...
        public static class AbstractObjectType
        {

            public static readonly AbstractPhysicalObject.AbstractObjectType obj_circle = new("un_circle", true);       //add [the AbstractObjectType of [ circle_object ]

        }

    }

}