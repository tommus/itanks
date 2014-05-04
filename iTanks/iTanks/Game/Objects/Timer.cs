using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.Objects
{
    public class Timer : Bonus
    {
        #region Fields
        public static int FREEZE_TIME = 10000;
        #endregion
        #region Constructors
        public Timer(int x, int y) : base(x, y, Type.TIMER)
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda odpowiedzialna za reakcjê na kolizje z obiektem.
        /// </summary>
        /// <param name="a">Obiekt, z którym badane jest zachodzenie kolizji.</param>
        public override void Collision(Actor a)
        {
            base.Collision(a);

            if (a is Player)
            {
                Level.Instance.FreezeBrigade();
            }
        }
        #endregion
    }
}