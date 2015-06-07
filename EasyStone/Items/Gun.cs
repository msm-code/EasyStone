using Msm.Geometry;
using System;

namespace EasyStone.Items
{
    abstract class Gun
    {
        protected Map world;
        protected DynamicObject parent;

        protected Random rgen;

        private float cooldownTime;
        private float maxCooldown;

        private Vector2 target;
        private bool isShooting;

        public Gun(Map world, DynamicObject parent, float maxCooldown)
        {
            this.world = world;
            this.parent = parent;
            this.maxCooldown = maxCooldown;
            this.rgen = new Random();
        }

        public void TimeElsaped(float time)
        {
            this.cooldownTime -= time;

            if (CanShoot())
                Shoot(target);
        }

        public virtual void BeginShooting(Vector2 target)
        {
            this.target = target;
            isShooting = true;
        }

        public Vector2 Target
        { get { return target; } set { this.target = value; } }

        public virtual void EndShooting()
        {
            isShooting = false;
        }

        protected virtual bool CanShoot()
        {
            return (isShooting && cooldownTime <= 0);
        }

        private void Shoot(Vector2 target)
        {
            PerformShoot(target - parent.Position);
            cooldownTime = maxCooldown;
        }

        public bool IsShooting
        { get { return isShooting; } }

        protected abstract void PerformShoot(Vector2 target);
    }
}
