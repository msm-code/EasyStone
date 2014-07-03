using EasyStone.Engine.Particles;
using System.Drawing;
using Msm.Geometry;
using EasyStone.Engine;

namespace EasyStone
{
    class PredefinedEffects
    {
        public static GraphicsEffect FirstBossKilled(Vector2 position)
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();
            descriptor.Position = position;
            descriptor.StartColor = new Color4(0, 0, 0, 50);
            descriptor.EndColor = new Color4(0, 0, 0, 0);
            descriptor.MinVelocity = 15;
            descriptor.VelocityVariation = 2;
            descriptor.MinLifetime = 3;
            descriptor.LifetimeVariation = 0;
            ParticleSystem first = ParticleSystemFactory.CreateExplosion(descriptor, 1000);

            descriptor.Position = position;
            descriptor.StartColor = Color4.Red;
            descriptor.EndColor = new Color4(0, 0, 0, 0);
            descriptor.MinVelocity = 0;
            descriptor.VelocityVariation = 15;
            descriptor.MinLifetime = 2;
            descriptor.LifetimeVariation = 1;
            ParticleSystem second = ParticleSystemFactory.CreateExplosion(descriptor, 2000);

            return new CombinedEffect(first, second);

            /*world.AddEffect(new ExplosionEffect(Position, 1000, Color4.Black, new Color4(0, 0, 0, 0), 15, 2, 3, 0));
            world.AddEffect(new ExplosionEffect(Position, 2000, Color4.Red, new Color4(0, 0, 0, 0), 0, 15, 2, 1));*/
        }

        public static GraphicsEffect SecondBossKilled(Vector2 position)
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();
            descriptor.Position = position;

            descriptor.StartColor = new Color4(255, 255, 0, 200);
            descriptor.EndColor = new Color4(0, 0, 0, 0);
            descriptor.MinVelocity = 25;
            descriptor.VelocityVariation = 0;
            descriptor.MinLifetime = 3;
            descriptor.LifetimeVariation = 0;
            ParticleSystem first = ParticleSystemFactory.CreateExplosion(descriptor, 1000);

            descriptor.StartColor = new Color4(255, 100, 0, 220);
            descriptor.EndColor = new Color4(100, 0, 0, 0);
            descriptor.MinVelocity = 15;
            descriptor.VelocityVariation = 15;
            descriptor.MinLifetime = 2;
            descriptor.LifetimeVariation = 1;
            ParticleSystem second = ParticleSystemFactory.CreateExplosion(descriptor, 1000);

            descriptor.StartColor = new Color4(0, 0, 0, 220);
            descriptor.EndColor = new Color4(0, 255, 0, 0);
            descriptor.MinVelocity = 0;
            descriptor.VelocityVariation = 25;
            descriptor.MinLifetime = 0;
            descriptor.LifetimeVariation = 3;
            ParticleSystem third = ParticleSystemFactory.CreateExplosion(descriptor, 1000);

            descriptor.StartColor = new Color4(50, 50, 255, 220);
            descriptor.EndColor = new Color4(0, 0, 255, 0);
            descriptor.MinVelocity = 5;
            descriptor.VelocityVariation = 5;
            descriptor.MinLifetime = 0;
            descriptor.LifetimeVariation = 3;
            ParticleSystem fourth = ParticleSystemFactory.CreateExplosion(descriptor, 1000);


            return new CombinedEffect(first, second, third, fourth);

            /*world.AddEffect(new ExplosionEffect(Position, 1000, new Color4(255, 255, 0, 200), new Color4(0, 0, 0, 0), 25, 0, 3, 0));
            world.AddEffect(new ExplosionEffect(Position, 1000, new Color4(255, 100, 0, 220), new Color4(100, 0, 0, 0), 15, 5, 2, 1));
            world.AddEffect(new ExplosionEffect(Position, 1000, new Color4(50, 50, 255, 220), new Color4(0, 0, 255, 0), 5, 10, 0, 3));
            world.AddEffect(new ExplosionEffect(Position, 3000, new Color4(0, 0, 0, 220), new Color4(0, 255, 0, 0), 0, 25, 0, 3));*/
        }

        public static GraphicsEffect GrenadeExplosion(Vector2 position, Vector2 velocity)
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();
            descriptor.Position = position;
            descriptor.MinVelocity = (velocity.Length / 2) * 1.5f;
            descriptor.VelocityVariation = velocity.Length / 2;
            descriptor.MinLifetime = 0.5f;
            descriptor.LifetimeVariation = 0.3f;
            descriptor.StartColor = new Color4(255, 128, 0, 250);
            descriptor.EndColor = new Color4(200, 200, 0, 0);

            ParticleSystem smooth1 = ParticleSystemFactory.CreateSmoothExplosion(descriptor, 100);

            descriptor.MinLifetime = 0.2f;
            descriptor.MinVelocity = (velocity.Length / 2) * 1.2f;
            descriptor.StartColor = new Color4(255, 255, 255, 250);
            descriptor.EndColor = new Color4(255, 255, 0, 0);
            ParticleSystem smooth2 = ParticleSystemFactory.CreateSmoothExplosion(descriptor, 50);

            descriptor.VelocityVariation += descriptor.MinVelocity;
            descriptor.MinVelocity = 0;
            descriptor.StartColor = new Color4(255, 0, 0);
            ParticleSystem point1 = ParticleSystemFactory.CreateExplosion(descriptor, 200);

            return new CombinedEffect(smooth1, smooth2, point1);
        }

        public static GraphicsEffect UnitKilled(Vector2 position, Color4 unitColor, float unitSize, float unitSpeed)
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();

            descriptor.Position = position;
            descriptor.StartColor = unitColor;
            descriptor.EndColor = new Color4(0, 0, 0, 0);
            descriptor.MinVelocity = 1.0f;
            descriptor.VelocityVariation = unitSpeed;
            descriptor.MinLifetime = 0;
            descriptor.LifetimeVariation = 0.7f;

            return ParticleSystemFactory.CreateTexturedExplosion(descriptor, (int)(100 * unitSize), TextureRepository.Instance.GetTexture("particle-star"), 1.2f, 1.2f);
        }

        public static GraphicsEffect AuraEffect(Vector2 position)
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();
            descriptor.Position = position;
            descriptor.StartColor = new Color4(255, 200, 0, 255);
            descriptor.EndColor = new Color4(255, 255, 150, 0);
            descriptor.MinVelocity = 0;
            descriptor.VelocityVariation = 20;
            descriptor.MinLifetime = 2;
            descriptor.LifetimeVariation = 1;

            return ParticleSystemFactory.CreateExplosion(descriptor, 1000);
        }

        public static GraphicsEffect GrenadeSmoke(Vector2 startPosition, Vector2 velocity)
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();
            descriptor.Position = startPosition;
            descriptor.StartColor = new Color4(200, 200, 200, 200);
            descriptor.EndColor = new Color4(100, 100, 100, 0);
            descriptor.MinVelocity = velocity.Length;
            descriptor.VelocityVariation = 0;
            descriptor.MinLifetime = 0.5f;
            descriptor.LifetimeVariation = 0;

            ParticleSystem smokeOnTheWater = ParticleSystemFactory.CreateTexturedMovingFountain(descriptor, -velocity.Rotation, 40, 50, velocity, TextureRepository.Instance.GetTexture("smoke"));

            return smokeOnTheWater; // and fire on the sky!
        }

        public static GraphicsEffect ShotgunFire(Vector2 startPosition, Vector2 velocity)
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();
            descriptor.Position = startPosition;
            descriptor.StartColor = new Color4(255, 128, 0, 128);
            descriptor.EndColor = new Color4(255, 0, 0, 0);
            descriptor.VelocityVariation = 2;
            descriptor.MinLifetime = 0.5f;
            descriptor.LifetimeVariation = 0;

            descriptor.MinVelocity = velocity.Length;
            ParticleSystem first = ParticleSystemFactory.CreateLimitedFountain(descriptor, velocity.Rotation, 45, 700, 50);

            ParticleSystem second = ParticleSystemFactory.CreateLimitedFountain(descriptor, velocity.Rotation, 25, 500, 50);

            return new CombinedEffect(first, second);
        }

        public static GraphicsEffect LightingGun(Vector2 start)
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();
            descriptor.Position = start;
            descriptor.StartColor = new Color4(200, 200, 0);
            descriptor.EndColor = new Color4(0,0, 0, 0);
            descriptor.MinVelocity = 35;
            descriptor.VelocityVariation = 15;
            descriptor.MinLifetime = 1.5f;
            descriptor.LifetimeVariation = 0;

            ParticleSystem lighting = ParticleSystemFactory.CreateSerpentFountain(descriptor, Angle.FromDegrees(310), 6, 20);

            return lighting;
        }
    }
}
