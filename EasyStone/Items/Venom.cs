using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;
using EasyStone.Bullets;

namespace EasyStone.Items
{
    class Venom : Gun
    {
        private const float bulletSpeed = 21f;

        public Venom(Map world, DynamicObject parent)
            : base(world, parent, 0.05f) { }

        protected override void PerformShoot(Vector2 target)
        {
            Vector2 bulletVelocity = Vector2.FromRotationAndLength(target.Rotation, bulletSpeed);
            bulletVelocity.Rotation += Angle.FromDegrees((rgen.NextDouble() - 0.5f) * 3);

            Angle offsetRotation = (parent.Position - target).Rotation;
            Vector2 offset1 = Vector2.FromRotationAndLength(offsetRotation + Angle.FromDegrees(90), 0.2f);
            Vector2 offset2 = Vector2.FromRotationAndLength(offsetRotation - Angle.FromDegrees(90), 0.2f);
            world.Add(new SimpleBullet(parent.Position + offset1, bulletVelocity, parent, world));
            world.Add(new SimpleBullet(parent.Position + offset2, bulletVelocity, parent, world));
        }
    }
}
