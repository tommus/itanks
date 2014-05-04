using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.Objects;

namespace iTanks.Game.AI
{
    public class Hard : Enemy
    {
        #region Constructors
        public Hard() : base((int)Level.SpawnSpot.ONE.X, (int)Level.SpawnSpot.ONE.Y, Enemy.Type.HARD)
        {
            lives = 2;
            Speed = 0.8f;
            BulletSpeed = 1.2f;
            up = Assets.Enemy[Type.HARD];
            down = Assets.Enemy[Type.HARD + 1];
            left = Assets.Enemy[Type.HARD + 2];
            right = Assets.Enemy[Type.HARD + 3];
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda odpowiedzialna za reakcjê obiektu klasy na zdarzenie z innym obiektem.
        /// </summary>
        /// <param name="a">Obiekt, z którym dosz³o do kolizji.</param>
        public override void Collision(Actor a)
        {
            base.Collision(a);

            if (a is Bullet)
            {
                Bullet bullet = (Bullet)a;
                if (bullet.Owner is Player && !bullet.ToRemove)
                    Level.Instance.NewBonus();
            }
        }
        #endregion
    }
}
