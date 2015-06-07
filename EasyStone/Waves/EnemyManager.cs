using System.Collections.Generic;
using Msm.Geometry;
using System;
using EasyStone.Enemy;
namespace EasyStone.Waves
{
    class EnemyManager2
    {
        Map world;
        Wave currentWave;
        Random rgen;

        public EnemyManager2(Map world)
        {
            this.world = world;
            this.WaveNumber = 0;
            this.rgen = new Random();
        }

        public bool NextWaveReady
        { get { if (currentWave == null) return true; else  return currentWave.Ended; } }


        public void Update(float delta)
        {
            if (currentWave == null)
                return;
            currentWave.Update(delta);
        }

        public void SpawnNextWave()
        {
            WaveNumber++;
            //if (rgen.Next() % 10 == 0)
            //{
            //    this.currentWave = new SurvivalWave(world, WaveNumber);
            //}
            //else
            if (WaveNumber % 15 == 0)
            {
                int bossNr = WaveNumber / 15;
                this.currentWave = new BossFight(world, WaveNumber, bossNr);
            }
            else
            {
                this.currentWave = new NormalWave(world, WaveNumber);
            }

            currentWave.Spawn();
        }

        public bool Victory 
        { get { return WaveNumber > 30; } }

        public int WaveNumber
        { get; private set; }
    }
}
