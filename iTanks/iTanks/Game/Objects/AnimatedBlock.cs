using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework;

namespace iTanks.Game.Objects
{
    public class AnimatedBlock : Actor
    {
        #region Fields
        protected Animation animation;
        #endregion
        #region Constructors
        public AnimatedBlock(int type, int x, int y) : base(type, x, y)
        {
            animation = Assets.Animations[type];

            width = animation.Image.Width;
            height = animation.Image.Height;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public override void Update(float DeltaTime)
        {
            animation.Update(DeltaTime);
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(animation.Image, x, y, width, height);
        }
        #endregion
    }
}