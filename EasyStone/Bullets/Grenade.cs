using Msm.Geometry;
using System;
using EasyStone.Engine.Particles;
namespace EasyStone.Bullets
{
    class Grenade : LifetimeLimitedBullet
    {
        GraphicsEffect smoke;

        public Grenade(Vector2 position, Vector2 velocity, DynamicObject parent, Map world, float maxLife)
            :base(position, velocity, parent, world, maxLife)
        {
            this.smoke = PredefinedEffects.GrenadeSmoke(position, velocity);
            world.AddEffect(smoke);
        }

        protected override void OnKilled()
        {
            Random r = new Random();

            Vector2 velocity = new Vector2(0, this.velocity.Length);
            for (int i = 0; i < 32; i++)
            {
                world.Add(new LifetimeLimitedBullet(Position, velocity,
                    parent, world, 0.3f + (float)r.NextDouble() * 0.2f));
                velocity.Rotation += Angle.FromDegrees(360 / 32);
            }

            world.AddEffect(PredefinedEffects.GrenadeExplosion(Position, velocity));

            smoke.Detach();

            base.OnKilled();
        }

        protected override float ObjectSize
        { get { return 0.9f; } }
    }
}
