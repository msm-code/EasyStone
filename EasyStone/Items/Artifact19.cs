using Msm.Geometry;
using EasyStone.Bullets;
namespace EasyStone.Items
{
    class Artifact19 : Gun
    {
        const float bulletSpeed = 25.0f;

        public Artifact19(Map world, DynamicObject parent)
            : base(world, parent, 0.1f) { }

        protected override void PerformShoot(Vector2 target)
        {
            Vector2 velocityBase = Vector2.FromRotationAndLength(target.Rotation, bulletSpeed);

            for (int i = -3; i <= 3; i++)
            {
                Vector2 bulletVelocity = velocityBase;
                bulletVelocity.Rotation += Angle.FromDegrees(i * 2 + (rgen.NextDouble() - 0.5) * 4);
                float lifetime = 5.0f;
                world.Add(new Grenade(parent.Position, bulletVelocity, parent, world, lifetime));
            }
        }
    }
}
