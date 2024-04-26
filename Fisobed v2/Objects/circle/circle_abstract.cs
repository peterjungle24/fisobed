#region using

using BepInEx.Logging;
using _enum;
using RWCustom;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Random = UnityEngine.Random;
using BepInEx;
using DevInterface;
using Fisobs.Core;
using Fisobs.Creatures;
using Fisobs.Sandbox;
using Fisobs.Items;
using Fisobs.Properties;
using MoreSlugcats;
using SandboxUnlock = Fisobs.Sandbox.SandboxUnlock;

#endregion

namespace circle
{

    public class circle_abstract : AbstractPhysicalObject
    {

        public circle_abstract(World world, WorldCoordinate pos, EntityID ID) : base(world, _enum.enum_.AbstractObjectType.circle_object, null, pos, ID)
        {
        }

        public override void Realize()
        {

            base.Realize();     //??
            realizedObject = new Circle(this);    //create the circle

        }

        public class circle_abstract_type
        {

            public static AbstractPhysicalObject.AbstractObjectType circle_absType = new(nameof(circle_absType), true);

        }

    }

}

