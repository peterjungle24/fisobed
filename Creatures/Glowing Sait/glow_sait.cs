using UnityEngine;
using BepInEx;
using System.Linq;
using System;
using RWCustom;
using System.Collections.Generic;
using main;
using circle;

namespace sait
{

    #region TT = Template.Type

    //SAIT template type
    public static class TT_sait
    {

        public static CreatureTemplate.Type TT_glow_sait = new CreatureTemplate.Type("glow_sait", true);

    }

    #endregion
    #region T = Template

    //SAIT template
    public class T_sait : CreatureTemplate
    {

        public T_sait(CreatureTemplate.Type sait_type, CreatureTemplate ancestor, List<TileTypeResistance> tileResistances, List<TileConnectionResistance> connectionResistances, CreatureTemplate.Relationship defaultRelationship) : base(sait_type, ancestor, tileResistances, connectionResistances, defaultRelationship)
        {

            this.name = "Glowing Sait";
            this.AI = true;
            this.canFly = false;
            this.canSwim = false;
            this.grasps = 0;

            base.shortcutColor = Color.green;
            base.smallCreature = true;
            base.type = type;

        }

    }

    #endregion
    #region C = Creature

    //SAIT creature
    public class C_glow_sait : TubeWorm
    {

        public C_glow_sait(AbstractCreature abstractCreature, World world) : base(abstractCreature, world)
        {
        }

        public override void InitiateGraphicsModule()
        {

            if (base.graphicsModule == null)
            {

                base.graphicsModule = new TubeWormGraphics(this);

            }

        }

    }

    #endregion
    #region UN = Unlock



    #endregion

}