using Msm.Geometry;
using EasyStone.Bullets;
namespace EasyStone.Items
{
    class Pistol : Gun
    {
        private const float bulletSpeed = 26f;

        public Pistol(Map world, DynamicObject parent)
            : base(world, parent, 0.6f) { }

        protected override void PerformShoot(Vector2 target)
        {
            Vector2 bulletVelocity = Vector2.FromRotationAndLength(target.Rotation, bulletSpeed);

            world.Add(new SimpleBullet(parent.Position, bulletVelocity, parent, world));
        }
    }
}
