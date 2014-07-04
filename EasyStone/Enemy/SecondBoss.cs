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
        Random r = new Random();

        public SecondBoss(Vector2 position, Map world)
            : base(position, world) { this.lives = MaxHp; }

        protected override float Speed
        {
            get { return 2; }
        }

        protected override float ObjectSize
        { get { return 4.0f; } }

        protected override void OnKilled()
        {
            world.AddEffect(PredefinedEffects.SecondBossKilled(Position));

            base.OnKilled();
        }

        protected override void BeforeUpdate(float delta)
        {
            base.BeforeUpdate(delta);

            float bulletSpeed = 16;
            Angle bulletDirection = Angle.FromDegrees(totalLifetime * 25);
            Vector2 bulletVelocity = Vector2.FromRotationAndLength(bulletDirection, bulletSpeed);

            if ((int)(totalLifetime * 15) < (int)((totalLifetime + delta) * 15))
            {
                world.Add(new SimpleBullet(Position, bulletVelocity, this, world));
                world.Add(new SimpleBullet(Position, bulletVelocity.Rotated(Angle.FromDegrees(180)), this, world));
            }

            if ((int)(totalLifetime * 20) < (int)((totalLifetime + delta) * 20)) {
                var randomRotation = Angle.FromDegrees(r.NextDouble() * 360);
                var randomSpeed = bulletSpeed * 0.7f;
                var randomVelocity = Vector2.FromRotationAndLength(randomRotation, randomSpeed);
                world.Add(new SimpleBullet(Position, randomVelocity, this, world));
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
