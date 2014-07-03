using Msm.Geometry;
using System;
namespace EasyStone
{
    class SuperEnemy : EnemyBase
    {
        public SuperEnemy(Vector2 position, Map world)
            : base(position, world)
        {
            this.lives = 360;
            this.totalLifetime = 0;
        }

        protected override void BeforeUpdate(float delta)
        {
            this.totalLifetime += delta;

            base.BeforeUpdate(delta);
        }

        protected override float Speed
        { get { return 5.0f; } }

        protected override float ObjectSize
        { get { return 4.0f; } }

        protected override Color4 Color
        {
            get { return new Color4((byte)(255 * (lives / 360.0f)), 0, (byte)(50 * (Math.Sin(totalLifetime * 3) + 1))); }
        }
    }
}
