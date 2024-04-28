#region using

using System.IO;
using BepInEx.Logging;
using Vector2 = UnityEngine.Vector2;
using objType = AbstractPhysicalObject.AbstractObjectType;
using objPhy = AbstractPhysicalObject;
using System.ComponentModel;
using _enum;
using System;
using System.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
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
using static Player;
using static MoreSlugcats.SingularityBomb;
using System.Reflection;
using MoreSlugcats;
using HUD;
using Fisobed_v2;

using circle;

#endregion

namespace idks
{

    public class circle_fisob : Fisob
    {

        public circle_fisob() : base(_enum.enum_.AbstractObjectType.circle_object)
        {

            Icon = new SimpleIcon(Fisobed_v2.Plugin.ION, new Color(255, 255, 255));    //color

            RegisterUnlock(_enum.enum_.SandboxUnlock.circle_sandbox);     //register the Sandbox unlock

        }

        public override AbstractPhysicalObject Parse(World world, EntitySaveData entitySaveData, Fisobs.Sandbox.SandboxUnlock unlock)
        {

            var abs = new circle_abstract(world, entitySaveData.Pos, entitySaveData.ID);    //var for the Abstract
            return abs;     //return itself

        }

        private static readonly circle_property circle_properties = new();   //new [ circle_property ]

        public class Unlocks
        {

            public static MultiplayerUnlocks.SandboxUnlockID circle_unlock = new("circle_IDK", true);

        }

    }

}
