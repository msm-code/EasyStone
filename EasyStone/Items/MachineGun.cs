using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;
using EasyStone.Bullets;

namespace EasyStone.Items
{
    class MachineGun : Gun
    {
        private const float bulletSpeed = 19f;

        public MachineGun(Map world, DynamicObject parent)
            : base(world, parent, 0.2f) { }

        protected override void PerformShoot(Vector2 target)
        {
            Vector2 bulletVelocity = Vector2.FromRotationAndLength(target.Rotation, bulletSpeed);
            bulletVelocity.Rotation += Angle.FromDegrees((rgen.NextDouble() - 0.5f) * 3);

            world.Add(new SimpleBullet(parent.Position, bulletVelocity, parent, world));
        }
    }
}
