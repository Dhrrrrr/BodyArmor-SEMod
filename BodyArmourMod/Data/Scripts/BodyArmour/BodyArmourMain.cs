using System;
using System.Text;
using System.Collections.Generic;
using Sandbox.Common.ObjectBuilders;
using SpaceEngineers.ObjectBuilders.ObjectBuilders;
using Sandbox;
using Sandbox.Definitions;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Interfaces.Terminal;
using SpaceEngineers.Game.ModAPI;
using VRage;
using VRage.Game;
using VRage.Game.Entity;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Input;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRage.Utils;
using VRageMath;
using BlendTypeEnum = VRageRender.MyBillboard.BlendTypeEnum;

namespace Dhr.HEAmmo
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate)]

    class BodyArmourMain : MySessionComponentBase
    {
        public static BodyArmourMain Instance;

        // Create body armour items
        MyItemType bodyArmour1 = new MyItemType("MyObjectBuilder_PhysicalObject", "BodyArmourLevel1");
        MyItemType bodyArmour2 = new MyItemType("MyObjectBuilder_PhysicalObject", "BodyArmourLevel2");
        MyItemType bodyArmour3 = new MyItemType("MyObjectBuilder_PhysicalObject", "BodyArmourLevel3");

        public override void LoadData()
        {
            Instance = this;

        }
        public override void BeforeStart()
        {
            // Get projectiles and add projectile hit lower function
            IMyProjectiles projectiles = MyAPIGateway.Projectiles;

            projectiles.AddOnHitInterceptor(1, ProjectileHit);
        }

        protected override void UnloadData()
        {
            Instance = null; 
        }
        
        void ProjectileHit(ref MyProjectileInfo projectile, ref MyProjectileHitInfo hit)
        {

            // Check if hit entity is a character
            if (hit.HitEntity is IMyCharacter)
            {

                // Check for body armour items
                IMyCharacter hitCharacter = (IMyCharacter)hit.HitEntity;

                VRage.Game.ModAPI.IMyInventory inventory = hitCharacter.GetInventory();


                if (inventory.ContainItems(1, bodyArmour3))
                {
                    hit.Damage *= 0.25f;

                }
                else if (inventory.ContainItems(1, bodyArmour2))
                {
                    hit.Damage *= 0.5f;

                }
                else if (inventory.ContainItems(1, bodyArmour1))
                {
                    hit.Damage *= 0.75f;

                }
            }
            else
            {
                return;
            }
        }
    }
}
