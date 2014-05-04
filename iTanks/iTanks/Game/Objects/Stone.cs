using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.AI;

namespace iTanks.Game.Objects
{
    public class Stone : Block
    {
        #region Constructors
        public Stone(int x, int y) : base(Type.STONE, x, y)
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
            if(a is Bullet)
            {
                Bullet bullet = (Bullet)a;
                bullet.Hited = type;
                if (bullet.Owner is Player)
                {
                    Player owner = (Player)bullet.Owner;
                    if (owner.Cannon >= 2)
                        ToRemove = true;
                }
            }
        }
        #endregion
    }
}
