using System.Collections.Generic;
namespace EasyStone.Engine.Particles
{
    interface ParticleGenerator
    {
        List<Particle> GenerateFirstParticles();
        List<Particle> UpdateGeneratorAndGetNewParticles(float delta);
    }
}
