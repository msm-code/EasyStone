using Msm.Geometry;

namespace EasyStone.Bullets
{
    class SimpleBullet : BulletBase
    {
        public SimpleBullet(Vector2 position, Vector2 velocity, DynamicObject parent, Map world)
            : base(position, velocity, parent, world) { }
    }
}
