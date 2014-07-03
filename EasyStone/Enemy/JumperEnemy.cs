using System;
using Msm.Geometry;
using EasyStone.Engine;
namespace EasyStone
{
    class JumperEnemy : EnemyBase
    {
        private float rushLeft;

        public JumperEnemy(Vector2 position, Map world)
            : base(position, world) { }

        protected override void BeforeUpdate(float delta)
        {
            if (rushLeft <= 0)
            { base.BeforeUpdate(delta); rushLeft = 1.0f; }
            else
                rushLeft -= delta;

            if (this.velocity != Vector2.Zero)
            { this.velocity.Length = Speed; }
        }

        protected override float Speed
        {
            get { return 25.0f * (float)Math.Sin(rushLeft / 1.0f) + 0.5f; }
        }

        protected override Color4 Color
        {
            get { return new Color4(100, 50, 200); }
        }

        protected override float ObjectSize
        { get { return 1.1f; } }

        protected override ShapeType ShapeType
        { get { return ShapeType.Triangle; } }
    }
}
