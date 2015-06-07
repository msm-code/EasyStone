using System.Collections.Generic;

namespace EasyStone.Engine.Particles
{
    class ParticleSystem : GraphicsEffect
    {
        private List<Particle> particles;
        private ParticleRenderer renderer;
        private ParticleGenerator generator;
        private bool detached;

        public ParticleSystem(ParticleRenderer renderer,
            ParticleGenerator generator)
        {
            this.particles = new List<Particle>();
            this.renderer = renderer;
            this.generator = generator;
            this.detached = false;

            this.particles.AddRange(generator.GenerateFirstParticles());
        }

        public bool Update(float delta)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(delta);
            }

            particles.RemoveAll((x) => { return !x.Alive; });

            if (!detached)
                this.particles.AddRange(generator.UpdateGeneratorAndGetNewParticles(delta));

            return particles.Count == 0;
        }

        public void Redraw()
        {
            renderer.Render(particles);
        }

        public void Detach()
        {
            this.detached = true;
        }

        public void Restore()
        {
            this.detached = false;
        }
    }
}
