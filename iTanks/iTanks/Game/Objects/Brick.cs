using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.AI;

namespace iTanks.Game.Objects
{
    public class Brick : Block
    {
        #region Constructors
        public Brick(int x, int y) : base(Type.BRICK, x, y)
        {
        }

        #endregion
        #region Methods
        /// <summary>
        /// Metoda odpowiadaj¹ca za reakcjê na zderzenie z obiektem przekazanym jako parametr.
        /// </summary>
        /// <param name="a">Obiekt, z którym dosz³o do zderzenia.</param>
        public override void Collision(Actor a)
        {
            if(a is Bullet)
            {
                Bullet bullet = (Bullet)a;
                bullet.Hited = type;
                ToRemove = true;
            }
        }
        #endregion
    }
}