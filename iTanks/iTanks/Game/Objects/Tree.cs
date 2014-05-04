using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game.Objects
{
    public class Tree : Block
    {
        #region Constructors
        public Tree(int x, int y) : base(Type.TREE, x, y)
        {
        }
        #endregion
    }
}