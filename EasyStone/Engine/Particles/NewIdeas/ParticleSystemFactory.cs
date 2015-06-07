using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    class ParticleSystemFactory
    {
        public static ParticleSystem CreateExplosion(ParticleSystemDescriptor descriptor, int maxCount)
        {
            ParticleRenderer renderer = new PointRenderer();
            ParticleGenerator generator = 
                new ExplosionStrategy(new RawParticleStreamGenerator(descriptor), maxCount);

            return new ParticleSystem(renderer, generator);
        }

        public static ParticleSystem CreateSerpentExplosion(ParticleSystemDescriptor descriptor, int maxCount)
        {
            ParticleRenderer renderer = new SerpentRenderer();
            ParticleGenerator generator =
                new ExplosionStrategy(new RawParticleStreamGenerator(descriptor), maxCount);

            return new ParticleSystem(renderer, generator);
        }

        public static ParticleSystem CreateSmoothExplosion(ParticleSystemDescriptor descriptor, int maxCount)
        {
            ParticleRenderer renderer = new SmoothRenderer();

            ParticleGenerator generator =
               new ExplosionStrategy(new RegularModyifer(new RawParticleStreamGenerator(descriptor), descriptor), maxCount);

            return new ParticleSystem(renderer, generator);
        }

        public static ParticleSystem CreateFountain(ParticleSystemDescriptor desciptor, 
            Angle direction, float directionVariation, int particlesPerSecond)
        {
            //ParticleStreamGenerator generator = new RawParticleStreamGenerator(desciptor);
            //generator = new FountainModyifer(generator, direction, directionVariation);
            //generator.Generate(100);

            ParticleRenderer renderer = new PointRenderer();
            //ParticleStreamGenerator generator = new DirectedGenerator(desciptor, direction, directionVariation, particlesPerSecond);
            ParticleGenerator generator =
                new FountainStrategy(new FountainModyifer(new RawParticleStreamGenerator(desciptor), direction, directionVariation), particlesPerSecond);

            return new ParticleSystem(renderer, generator);
            //return null;
        }

        public static ParticleSystem CreateSerpentFountain(ParticleSystemDescriptor desciptor, 
            Angle direction, float directionVariation, int particlesPerSecond)
        {
            //ParticleStreamGenerator generator = new RawParticleStreamGenerator(desciptor);
            //generator = new FountainModyifer(generator, direction, directionVariation);
            //generator.Generate(100);

            ParticleRenderer renderer = new SerpentRenderer();
            //ParticleStreamGenerator generator = new DirectedGenerator(desciptor, direction, directionVariation, particlesPerSecond);
            ParticleGenerator generator =
                new FountainStrategy(new FountainModyifer(new RawParticleStreamGenerator(desciptor), direction, directionVariation), particlesPerSecond);

            return new ParticleSystem(renderer, generator);
            //return null;
        }

        public static ParticleSystem CreateMovingFountain(ParticleSystemDescriptor desciptor,
            Angle direction, float directionVariation, int particlesPerSecond, Vector2 velocity)
        {
            ParticleRenderer renderer = new PointRenderer();
            //ParticleStreamGenerator generator = new DynamicGenerator(desciptor, direction, directionVariation, particlesPerSecond, velocity);
            ParticleGenerator generator =
                new FountainStrategy(new DynamicModyifer(new FountainModyifer(new RawParticleStreamGenerator(desciptor), direction, directionVariation), velocity), particlesPerSecond);

            return new ParticleSystem(renderer, generator);
        }

        public static ParticleSystem CreateLimitedFountain(ParticleSystemDescriptor desciptor,
            Angle direction, float directionVariation, int particlesPerSecond, int generateUpToParticles)
        {
            ParticleRenderer renderer = new PointRenderer();
            //ParticleStreamGenerator generator = new DirectedGenerator(desciptor, direction, directionVariation, particlesPerSecond);
            //generator = new LimitedGenerator(generator, generateUpToParticles);
            ParticleGenerator generator =
                new FountainStrategy(
                    new LimitedModyifer(
                        new FountainModyifer(
                            new RawParticleStreamGenerator(desciptor),
                            direction,
                            directionVariation),
                        generateUpToParticles),
                    particlesPerSecond);

            return new ParticleSystem(renderer, generator);
        }

        public static ParticleSystem CreateTexturedMovingFountain(ParticleSystemDescriptor descriptor,
            Angle direction, float directionVariation, int particlesPerSecond, Vector2 velocity, int texture)
        {
            ParticleRenderer renderer = new TexturedRenderer(texture, 1.5f, 5f);
            //ParticleStreamGenerator generator = new DirectedGenerator(desciptor, direction, directionVariation, particlesPerSecond);
            //generator = new LimitedGenerator(generator, generateUpToParticles);
            ParticleGenerator generator =
                new FountainStrategy(
                    new DynamicModyifer(
                        new FountainModyifer(
                            new RawParticleStreamGenerator(descriptor),
                            direction,
                            directionVariation),
                        velocity),
                    particlesPerSecond);

            return new ParticleSystem(renderer, generator);
        }

        public static ParticleSystem CreateTexturedExplosion(ParticleSystemDescriptor descriptor, int maxParticles, int texture, float initialSize, float finalSize)
        {
            ParticleRenderer renderer = new TexturedRenderer(texture, initialSize, finalSize);
            ParticleGenerator generator = new ExplosionStrategy(
                new RawParticleStreamGenerator(descriptor), maxParticles);

            return new ParticleSystem(renderer, generator);
        }
    }
}
