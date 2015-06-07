using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;
using EasyStone.Engine;

namespace EasyStone
{
    class FastEnemy : EnemyBase
    {
        public FastEnemy(Vector2 position, Map world)
            : base(position, world) { this.totalLifetime = 0; }

        protected override void BeforeUpdate(float delta)
        {
            base.BeforeUpdate(delta);

            if (velocity != Vector2.Zero)
            { velocity.Rotation += Angle.FromRadians(Math.Sin(totalLifetime * 7)); }
        }

        protected override float Speed
        {
            get { return 16; }
        }

        protected override Color4 Color
        {
            get { return new Color4(50, 0, 200); }
        }

        protected override ShapeType ShapeType
        { get { return ShapeType.Ship;  } }
    }
}
