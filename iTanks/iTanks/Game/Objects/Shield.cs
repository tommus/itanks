using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.Objects
{
    public class Shield : Bonus
    {
        #region Fields
        public static int SHIELD_TIME = 30000;
        #endregion
        #region Constructors
        public Shield(int x, int y) : base(x, y, Type.SHIELD)
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

            if(a is Player)
            {
                Player player = (Player)a;
                player.SetShield(SHIELD_TIME);
            }
        }
        #endregion
    }
}