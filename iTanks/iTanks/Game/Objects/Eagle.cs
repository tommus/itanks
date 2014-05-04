using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.Objects
{
    public class Eagle : Block
    {
        #region Constructors
        public Eagle(int x, int y) : base(Type.EAGLE, x, y)
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
            if (a is Bullet)
                Level.Instance.EagleAlive = false;
        }
        #endregion
    }
}