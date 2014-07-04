using System;
using Msm.Geometry;
using System.Collections.Generic;
namespace EasyStone
{
    class SurvivalWave : Wave
    {
        bool spawned;
        float totalTime;

        public SurvivalWave(Map world, int waveNumber)
            :base(world, waveNumber)
        {
            this.spawned = false;
            this.totalTime = 0;
        }

        public override void Spawn()
        {
            spawned = true;
            GlobalInterface.AddInfoBox("Special event: Survival!");
        }

        public override void Update(float delta)
        {
            if (!spawned)
                return;

            if (totalTime > 9.0f)
            {
                for (int i = myUnits.Count - 1; i >= 0; i--)
                {
                    if (i % 10 == (int)((totalTime - 9.0f) * 10) % 10)
                        myUnits[i].Kill();
                }
            }
            else
            {
                if ((int)(totalTime * waveNumber * 3) < (int)((totalTime + delta) * waveNumber * 3))
                {
                    SpawnNewUnit();
                }
            }

            totalTime += delta;
        }

        private void SpawnNewUnit()
        {
            Vector2 position = base.GetRandomPosition();

            DynamicObject enemy = new SimpleEnemy(position, world);
            AddEnemy(enemy);
        }

        public override bool Ended
        {
            get { return totalTime > 10.0f; }
        }
    }
}
