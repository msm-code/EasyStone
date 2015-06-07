using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;
using EasyStone.Bullets;

namespace EasyStone.Items
{
    class GrenadeLauncher : Gun
    {
        private const float bulletSpeed = 17f;
        private const float maxRange = 20.0f;

        public GrenadeLauncher(Map world, DynamicObject parent)
            : base(world, parent, 0.5f) { }

        protected override void PerformShoot(Vector2 target)
        {
            float len = Math.Min(maxRange, target.Length);
            Vector2 bulletVelocity = Vector2.FromRotationAndLength(target.Rotation, bulletSpeed);

            world.Add(new Grenade(parent.Position, bulletVelocity, parent, world, len / bulletSpeed));
        }
    }
}
