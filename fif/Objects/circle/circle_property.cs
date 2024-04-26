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

    public class circle_property : ItemProperties
    {

        public override void Throwable(Player player, ref bool throwable) => throwable = true;  //make the item trowable (?)

    }

}