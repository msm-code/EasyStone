using Msm.Geometry;
using EasyStone.Bullets;

namespace EasyStone
{
    class ExplosionEnemy : EnemyBase
    {
        public ExplosionEnemy(Vector2 position, Map world)
            : base(position, world) { this.lives = 5; }

        protected override float Speed
        {
            get { return 5; }
        }

        protected override float ObjectSize
        { get { return 1.2f; } }

        protected override void OnKilled()
        {
            Vector2 velocity = new Vector2(0, -10);
            for (int i = 0; i < 16; i++)
            {
                
                world.Add(new SimpleBullet(this.Position, velocity, this, world));
                velocity.Rotation += Angle.FromDegrees(360 / 16);
            }

            base.OnKilled();
        }

        protected override Color4 Color
        {
            get { return new Color4((byte)(127 + 25 * lives / 5.0f), (byte)(lives * 51), 0); }
        }
    }
}
