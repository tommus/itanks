using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;

namespace iTanks.Game.Objects
{
    public class Block : Actor
    {
        #region Internal Classes
        /// <summary>
        /// Klasa przechowuje informacjê o typie bloku.
        /// </summary>
        public class Type
        {
            public static int BRICK = 0;
            public static int STONE = 1;
            public static int ROAD = 2;
            public static int TREE = 3;
            public static int WATER = 4;
            public static int EAGLE = 5;
            public static int PLAYER = 6;
            public static int BULLET = 7; // textures 7 - 10

            // below not used for textures
            public static int BONUS = 8;
            public static int ENEMY = 9;
        }
        #endregion
        #region Fields
        protected Image texture;
        #endregion
        #region Constructors
        public Block(int type, int x, int y) : base(type, x, y)
        {
            texture = Assets.Blocks[type];

            width = texture.Width;
            height = texture.Height;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(texture, x, y, width, height);
        }
        #endregion
    }
}