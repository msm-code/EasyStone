using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;
using EasyStone.Bullets;
using EasyStone.Engine.Particles;

namespace EasyStone.Items
{
    class Aura : Gun
    {
        private const float bulletSpeed = 20f;
        private int power;

        public Aura(Map world, DynamicObject parent, int power)
            : base(world, parent, 2.5f) { this.power = power; }

        protected override void PerformShoot(Vector2 target)
        {
            for (int i = 0; i < power; i++)
            {
                Vector2 velocity = Vector2.FromRotationAndLength(Angle.FromDegrees(i * 360 / (float)power), bulletSpeed);
                world.Add(new SimpleBullet(parent.Position, velocity, parent, world));
                world.Add(new SimpleBullet(parent.Position - velocity * 0.1f, velocity, parent, world));
            }

            world.AddEffect(PredefinedEffects.AuraEffect(parent.Position));
        }
    }
}
