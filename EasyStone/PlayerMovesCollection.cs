using System;
namespace EasyStone
{
    public enum MoveTypes
    {
        Pistol,
        MachineGun,
        Shotgun,
        GrenadeLauncher,
        Aura,
        Venom,
        Artifact19 = 100
    }

    class MoveUnlockedEventArgs : EventArgs
    {
        public string MoveName {get;set;}

        public MoveUnlockedEventArgs(string moveName)
        {
            this.MoveName = moveName;
        }
    }

    class PlayerMovesCollection
    {
        private int unlockLevel;

        public void UnlockNext()
        {
            unlockLevel++;

            string[] values = Enum.GetNames(typeof(MoveTypes));

            foreach (string item in values)
            {
                int value = (int)Enum.Parse(typeof(MoveTypes), item);

                if (value > unlockLevel)
                    return;
                if (value == unlockLevel)
                {
                    if (UnlockedMove != null)
                        UnlockedMove(this, new MoveUnlockedEventArgs(item));
                }
            }
        }

        public bool IsUnlocked(MoveTypes type)
        {
            return (int)type <= unlockLevel;
        }

        public event EventHandler<MoveUnlockedEventArgs> UnlockedMove;
    }
}
