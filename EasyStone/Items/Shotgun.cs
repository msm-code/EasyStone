using Msm.Geometry;
using EasyStone.Bullets;
using System;
namespace EasyStone.Items
{
    class Shotgun : Gun
    {
        private const float bulletSpeed = 20f;

        public Shotgun(Map world, DynamicObject parent)
            : base(world, parent, 0.5f)
        { rgen = new Random(); }

        protected override void PerformShoot(Vector2 target)
        {
            Vector2 velocityBase = Vector2.FromRotationAndLength(target.Rotation, bulletSpeed);

            for (int i = -3; i <= 3; i++)
            {
                Vector2 bulletVelocity = velocityBase;
                bulletVelocity.Rotation += Angle.FromDegrees(i * 5 + (rgen.NextDouble() - 0.5) * 4);
                float lifetime = (float)rgen.NextDouble() * 0.3f + 0.5f;
                world.Add(new LifetimeLimitedBullet(parent.Position, bulletVelocity, parent, world, lifetime));
            }

            world.AddEffect(PredefinedEffects.ShotgunFire(parent.Position, velocityBase));
        }
    }
}
