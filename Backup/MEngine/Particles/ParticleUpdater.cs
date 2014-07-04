using System.Collections.Generic;

namespace MEngine.Particles
{
    class ParticleUpdater : IParticleUpdater
    {
        private List<IParticleUpdater> beforeUpdate;
        private List<IParticleUpdater> afterUpdate;

        public ParticleUpdater()
        {
            beforeUpdate = new List<IParticleUpdater>();
            afterUpdate = new List<IParticleUpdater>();
        }

        public void AttachActionBeforeUpdate(IParticleUpdater updater)
        {
            this.beforeUpdate.Add(updater);
        }

        public void AttachActionAfterUpdate(IParticleUpdater updater)
        {
            this.afterUpdate.Add(updater);
        }

        public void Update(List<Particle> particles, float delta)
        {
            foreach (IParticleUpdater updater in beforeUpdate)
                updater.Update(particles, delta);

            RealUpdateParticles(particles, delta);

            foreach (IParticleUpdater updater in afterUpdate)
                updater.Update(particles, delta);
        }

        private void RealUpdateParticles(List<Particle> particles, float delta)
        {
            int count = particles.Count;
            for (int i = 0; i < count; i++)
            {
                particles.ForEach((x) => 
                {
                    x.Position += x.Velocity; 
                    x.Lifetime += delta; 
                });
            }
        }
    }
}
