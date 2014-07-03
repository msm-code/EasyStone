using Msm.Geometry;
using EasyStone.Bullets;
using EasyStone.Items;
using System;
using EasyStone.Engine.Particles;
namespace EasyStone.Enemy
{
    class FirstBoss : EnemyBase
    {
        public FirstBoss(Vector2 position, Map world)
            :base(position, world)
        {
            this.lives = 100;
        }

        protected override float Speed
        {
            get { return 3.0f; }
        }

        protected override float ObjectSize
        { get { return 3.0f; } }

        protected override void OnKilled()
        {
            world.AddEffect(PredefinedEffects.FirstBossKilled(Position));
            /*world.AddEffect(new ExplosionEffect(Position, 1000, Color4.Black, new Color4(0, 0, 0, 0), 15, 2, 3, 0));
            world.AddEffect(new ExplosionEffect(Position, 2000, Color4.Red, new Color4(0, 0, 0, 0), 0, 15, 2, 1));*/

            base.OnKilled();
        }

        protected override void BeforeUpdate(float delta)
        {
            base.BeforeUpdate(delta);

            Vector2 bulletVelocity = velocity;
            if ((int)(totalLifetime * 7) < (int)((totalLifetime + delta) * 7))
            {
                bulletVelocity.Length = 25;
                world.Add(new SimpleBullet(Position, bulletVelocity, this, world));
            }
            if ((int)(totalLifetime) < (int)(totalLifetime + delta))
            {
                bulletVelocity.Length = 15;
                world.Add(new Grenade(Position, bulletVelocity, this, world, 1));
            }
            if ((int)(totalLifetime / 5) < (int)((totalLifetime + delta) / 5))
            {
                //new Aura(world, this, 16).Shoot(Vector2.Zero);
            }
        }

        protected override Msm.Geometry.Color4 Color
        {
            get
            {
                float scale = Math.Min(lives / 15.0f, 1.0f);
                return new Color4((byte)(250 * scale), (byte)(100 * scale), (byte)(100 * (lives / 100.0f) * scale));
            }
        }
    }
}
