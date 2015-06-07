using Msm.Geometry;
using System;
using System.Collections.Generic;
using EasyStone.Enemy;
namespace EasyStone.Waves
{
    class NormalWave : Wave
    {
        float coolDownTime;

        public NormalWave(Map world, int waveNumber)
            :base(world, waveNumber)
        {
        }

        public override void Spawn()
        {
            for (int i = 0; i < waveNumber; i++)
            {
                Vector2 position = base.GetRandomPosition();

                int enemyLvl = rgen.Next(25) + (int)(waveNumber * rgen.NextDouble());
                DynamicObject enemy;
                if (enemyLvl < 20)
                { enemy = new SimpleEnemy(position, world); }
                else if (enemyLvl < 25)
                { enemy = new JumperEnemy(position, world); }
                else if (enemyLvl < 30)
                { enemy = new FastEnemy(position, world); }
                else if (enemyLvl < 35)
                { enemy = new GunnerEnemy(position, world); }
                else if (enemyLvl < 40)
                { enemy = new StrongEnemy(position, world); }
                else if (enemyLvl < 45)
                { enemy = new ExplosionEnemy(position, world); }
                else if (enemyLvl < 50)
                { enemy = new MotherEnemy(position, world); }
                else
                { enemy = new SuperEnemy(position, world); }

                AddEnemy(enemy);
            }
        }

        public override void Update(float delta)
        {
            if (myUnits.Count == 0)
            { coolDownTime += delta; }
        }

        public override bool Ended
        { get { return myUnits.Count == 0 && coolDownTime > 1.0f; } }
    }
}
