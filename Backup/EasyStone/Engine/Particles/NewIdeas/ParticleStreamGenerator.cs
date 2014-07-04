using System.Collections.Generic;
namespace EasyStone.Engine.Particles
{
    interface ParticleStreamGenerator
    {
        List<Particle> Generate(int count, float totalTime);
    }
}
