using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.Objects
{
    public class Granade : Bonus
    {
        #region Constructors
        public Granade(int x, int y) : base(x, y, Type.GRANADE)
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
                Level.Instance.DetonateBrigade();
                Level.Instance.Spawn();
                Level.Instance.Spawn();
                Level.Instance.Spawn();
            }
        }
        #endregion
    }
}