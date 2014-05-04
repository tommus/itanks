using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.Objects
{
    public class Star : Bonus
    {
        #region Constructors
        public Star(int x, int y) : base(x, y, Type.STAR)
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

                if(player.Cannon <= 2)
                    player.SetTank(Player.TankType.HEAVY);  
            }
        }
        #endregion
    }
}