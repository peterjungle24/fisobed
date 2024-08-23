#region using

using BepInEx.Logging;
using main;
using RWCustom;
using UnityEngine;
using BepInEx;
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

    public class abs_circle : AbstractPhysicalObject
    {

        #region ctor

        public abs_circle(World world, WorldCoordinate pos, EntityID ID) : base(world, main.enums.AbstractObjectType.obj_circle, null, pos, ID)
        {
        }

        #endregion
        #region realize.

        //realize.
        public override void Realize()
        {

            base.Realize();     //??
            realizedObject = new Circle(this);    //create the circle

        }

        #endregion
        #region abstract type Circle

        //abstract type
        public class absT_Circle
        {

            public static AbstractPhysicalObject.AbstractObjectType absT_circle = new("absT_circle", true);

        }

        #endregion

    }

}

