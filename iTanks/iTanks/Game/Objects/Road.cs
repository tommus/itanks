using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.Objects
{
    public class Road : Block
    {
        #region Constructors
        public Road(int x, int y) : base(Type.ROAD, x, y)
        {
        }
        #endregion
    }
}