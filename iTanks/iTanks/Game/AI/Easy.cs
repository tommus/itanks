using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.AI
{
    public class Easy : Enemy
    {
        #region Constructors
        public Easy() : base((int)Level.SpawnSpot.ONE.X, (int)Level.SpawnSpot.ONE.Y, Enemy.Type.EASY)
        {
            Speed = 0.5f;
            BulletSpeed = 1.0f;
            up = Assets.Enemy[Type.EASY];
            down = Assets.Enemy[Type.EASY + 1];
            left = Assets.Enemy[Type.EASY + 2];
            right = Assets.Enemy[Type.EASY + 3];
        }
        #endregion
    }
}
