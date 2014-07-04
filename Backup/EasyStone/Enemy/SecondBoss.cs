using System;
using Msm.Geometry;
using EasyStone.Bullets;
using EasyStone.Items;
using EasyStone.Engine.Particles;
namespace EasyStone.Enemy
{
    class SecondBoss : EnemyBase
    {
        private const int MaxHp = 500;

        public SecondBoss(Vector2 position, Map world)
            : base(position, world) { this.lives = MaxHp; }

        protected override float Speed
        {
            get { return 3; }
        }

        protected override float ObjectSize
        { get { return 4.0f; } }

        protected override void OnKilled()
        {
            world.AddEffect(PredefinedEffects.SecondBossKilled(Position));
            /*world.AddEffect(new ExplosionEffect(Position, 1000, new Color4(255, 255, 0, 200), new Color4(0, 0, 0, 0), 25, 0, 3, 0));
            world.AddEffect(new ExplosionEffect(Position, 1000, new Color4(255, 100, 0, 220), new Color4(100, 0, 0, 0), 15, 5, 2, 1));
            world.AddEffect(new ExplosionEffect(Position, 1000, new Color4(50, 50, 255, 220), new Color4(0, 0, 255, 0), 5, 10, 0, 3));
            world.AddEffect(new ExplosionEffect(Position, 3000, new Color4(0, 0, 0, 220), new Color4(0, 255, 0, 0), 0, 25, 0, 3));*/

            base.OnKilled();
        }

        protected override void BeforeUpdate(float delta)
        {
            base.BeforeUpdate(delta);

            float bulletSpeed = 20;
            Angle bulletDirection = Angle.FromDegrees(totalLifetime * 50);
            Vector2 bulletVelocity = Vector2.FromRotationAndLength(bulletDirection, bulletSpeed);

            if ((int)(totalLifetime * 15) < (int)((totalLifetime + delta) * 15))
            {
                world.Add(new SimpleBullet(Position, bulletVelocity, this, world));
                world.Add(new SimpleBullet(Position, bulletVelocity.Rotated(Angle.FromDegrees(180)), this, world));
            }
        }

        protected override Color4 Color
        {
            get
            {
                float scale = Math.Min(lives / 50.0f, 1.0f);
                return new Color4((byte)(150 * scale), (byte)(50 * (lives / (float)MaxHp) * scale), (byte)(50 * scale));
            }
        }
    }
}
