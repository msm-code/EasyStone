using System;
using System.Collections.Generic;
using Msm.Geometry;
namespace EasyStone
{
    abstract class Wave
    {
        protected Map world;
        protected int waveNumber;
        protected Random rgen;
        protected List<DynamicObject> myUnits;

        public Wave(Map world, int waveNumber)
        {
            this.world = world;
            this.waveNumber = waveNumber;
            this.rgen = new Random();
            this.myUnits = new List<DynamicObject>();
        }

        public abstract void Spawn();
        public abstract void Update(float delta);
        public abstract bool Ended { get; }

        protected void AddEnemy(DynamicObject enemy)
        {
            world.Add(enemy);
            myUnits.Add(enemy);

            enemy.Killed += (sender, e) =>
            {
                myUnits.Remove(enemy);
            };
        }

        protected Vector2 GetRandomPosition()
        {
            float posx = (float)(rgen.NextDouble() - 0.5) * 8;
            float posy = (float)(rgen.NextDouble() - 0.5) * 8;
            return new Vector2(posx - 10, posy);
        }
    }
}
