using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.AI;

namespace iTanks.Game.Objects
{
    public class Water : AnimatedBlock
    {
        #region Constructors
        public Water(int x, int y) : base(Block.Type.WATER, x, y)
        {
        }
        #endregion
    }
}