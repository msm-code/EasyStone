using Msm.Geometry;
using EasyStone.Enemy;
using System;
namespace EasyStone.Waves
{
    class BossFight : Wave
    {
        int bossNr;
        DynamicObject theBoss;
        int bossDeathTicks;

        public BossFight(Map world, int waveNumber, int bossNr)
            :base(world, waveNumber)
        {
            this.bossNr = bossNr;
        }

        public override void Spawn()
        {
            Vector2 position = base.GetRandomPosition();

            DynamicObject boss;
            if (bossNr == 1)
            {
               boss = new FirstBoss(position, world);
            }
            else
                boss = new SecondBoss(position, world);
            this.theBoss = boss;

            base.AddEnemy(boss);

            theBoss.Killed += (sender, e) => { 
                bossDeathTicks = Environment.TickCount;
                GlobalInterface.AddInfoBox("Boss is dead :D");
            };
        }

        public override void Update(float delta)
        {
        }

        public override bool Ended
        {
            get { return !myUnits.Contains(theBoss) && Environment.TickCount - bossDeathTicks > 5000; }
        }
    }
}
