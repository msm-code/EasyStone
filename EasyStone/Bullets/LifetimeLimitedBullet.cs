using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;

namespace EasyStone.Bullets
{
    class LifetimeLimitedBullet : BulletBase
    {
        float lifeLeft;

        public LifetimeLimitedBullet(Vector2 position, Vector2 velocity, DynamicObject parent, Map world, float maxLife)
            : base(position, velocity, parent, world)
        {
            this.lifeLeft = maxLife;
        }

        protected override void BeforeUpdate(float delta)
        {
            base.BeforeUpdate(delta);

            this.lifeLeft -= delta;

            if (lifeLeft < 0)
                this.Kill();
        }
    }
}
