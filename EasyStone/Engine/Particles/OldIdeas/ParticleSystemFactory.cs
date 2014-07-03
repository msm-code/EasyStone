using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    class ParticleSystemFactory
    {
        public static ParticleSystem CreateExplosion(ParticleSystemDescriptor descriptor, int maxCount)
        {
            ParticleRenderer renderer = new PointRenderer();
            ParticleGenerator generator = new FullRandomGenerator(descriptor, maxCount);

            return new ParticleSystem(renderer, generator);
        }

        /*public static ParticleSystem CreateExplosion(ParticleSystemDescriptor descriptor, int maxCount)
        {
            ParticleRenderer renderer = new PointRenderer();
            ParticleGenerationStrategy generator = 
                new ExplosionStrategy(new RawParticleStreamGenerator(descriptor), maxCount);

            return new ParticleSystem(renderer, generator);
        }*/

        public static ParticleSystem CreateSerpentExplosion(ParticleSystemDescriptor descriptor, int maxCount)
        {
            ParticleRenderer renderer = new SerpentRenderer();
            //ParticleGenerationStrategy generator =
            //    new ExplosionStrategy(new RawParticleStreamGenerator(descriptor), maxCount);
            ParticleGenerator generator = new FullRandomGenerator(descriptor, maxCount);

            return new ParticleSystem(renderer, generator);
        }

        public static ParticleSystem CreateSmoothExplosion(ParticleSystemDescriptor descriptor, int maxCount)
        {
            ParticleRenderer renderer = new SmoothRenderer();

            //ParticleGenerationStrategy generator =
            //    new ExplosionStrategy(new RawParticleStreamGenerator(descriptor), maxCount);
            ParticleGenerator generator = new RegularGenerator(descriptor, maxCount, true);

            return new ParticleSystem(renderer, generator);
        }

        public static ParticleSystem CreateFountain(ParticleSystemDescriptor desciptor, 
            Angle direction, float directionVariation, int particlesPerSecond)
        {
            //ParticleStreamGenerator generator = new RawParticleStreamGenerator(desciptor);
            //generator = new FountainModyifer(generator, direction, directionVariation);
            //generator.Generate(100);

            ParticleRenderer renderer = new PointRenderer();
            ParticleGenerator generator = new DirectedGenerator(desciptor, direction, directionVariation, particlesPerSecond);
            //ParticleGenerationStrategy generator =
            //    new FountainStrategy(new FountainModyifer(new RawParticleStreamGenerator(desciptor), direction, directionVariation), particlesPerSecond);

            return new ParticleSystem(renderer, generator);
            //return null;
        }

        public static ParticleSystem CreateMovingFountain(ParticleSystemDescriptor desciptor,
            Angle direction, float directionVariation, int particlesPerSecond, Vector2 velocity)
        {
            ParticleRenderer renderer = new PointRenderer();
            ParticleGenerator generator = new DynamicGenerator(desciptor, direction, directionVariation, particlesPerSecond, velocity);
            //ParticleGenerationStrategy generator =
            //    new FountainStrategy(new DynamicModyifer(new FountainModyifer(new RawParticleStreamGenerator(desciptor), direction, directionVariation), velocity), particlesPerSecond);

            return new ParticleSystem(renderer, generator);
            //return null;
        }

        public static ParticleSystem CreateLimitedFountain(ParticleSystemDescriptor desciptor,
            Angle direction, float directionVariation, int particlesPerSecond, int generateUpToParticles)
        {
            ParticleRenderer renderer = new PointRenderer();
            ParticleGenerator generator = new DirectedGenerator(desciptor, direction, directionVariation, particlesPerSecond);
            generator = new LimitedGenerator(generator, generateUpToParticles);
            /*ParticleGenerationStrategy generator =
                new FountainStrategy(
                    new LimitedModyifer(
                        new FountainModyifer(
                            new RawParticleStreamGenerator(desciptor),
                            direction,
                            directionVariation),
                        generateUpToParticles),
                    particlesPerSecond);*/



            return new ParticleSystem(renderer, generator);
            //return null;
        }
    }
}
