using System.Linq;
using Msm.Geometry;

namespace EasyStone
{
    class SimpleEnemy : EnemyBase
    {
        public SimpleEnemy(Vector2 position, Map world)
            : base(position, world) { }

        protected override float Speed
        {
            get { return 8; }
        }

        protected override Color4 Color
        {
            get { return new Color4(250, 0, 0); }
        }
    }
}
