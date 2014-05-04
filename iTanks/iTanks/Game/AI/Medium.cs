using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.AI
{
    public class Medium : Enemy
    {
        #region Constructors
        public Medium() : base((int)Level.SpawnSpot.ONE.X, (int)Level.SpawnSpot.ONE.Y, Enemy.Type.MEDIUM)
        {
            Speed = 1.0f;
            BulletSpeed = 2.0f;
            up = Assets.Enemy[Type.MEDIUM];
            down = Assets.Enemy[Type.MEDIUM + 1];
            left = Assets.Enemy[Type.MEDIUM + 2];
            right = Assets.Enemy[Type.MEDIUM + 3];
        }
        #endregion
    }
}
