using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyStone
{
    static class SessionStatistics
    {
        private static int killCount;

        public static void EnemyKilled(DynamicObject enemy)
        {
            killCount++;

            if (killCount == 1)
            { GlobalInterface.AddInfoBox("First blood: enemy killed"); }

            if (killCount == 10)
            { GlobalInterface.AddInfoBox("Newbie: 10 enemies"); }

            if (killCount == 50)
            { GlobalInterface.AddInfoBox("Getting started: 50 enemies"); }

            if (killCount == 100)
            { GlobalInterface.AddInfoBox("Gamer: 100 enemies"); }

            if (killCount == 200)
            { GlobalInterface.AddInfoBox("Skilled: 200 enemies"); }

            if (killCount == 300)
            { GlobalInterface.AddInfoBox("Pro: 300 enemies"); }

            if (killCount == 400)
            { GlobalInterface.AddInfoBox("Master: 400 enemies"); }

            if (killCount == 500)
            { GlobalInterface.AddInfoBox("Impossible: 500 enemies"); }
        }

        public static int KillCount
        { get { return killCount; } }
    }
}
