using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.Objects;
using GameFramework;

namespace iTanks.Game
{
    public class Spawn
    {
        #region Fields
        private Animation animation;
        private int x;
        private int y;
        private float animationTime;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechouje informacjê o koniecznoœci usuniêcia obiektu typu 'Spawn' z listy
        /// przechouj¹cej instancje obiektu w grze.
        /// </summary>
        public Boolean ToRemove { get; set; }
        #endregion
        #region Constructors
        public Spawn(int x, int y)
        {
            ToRemove = false;
            animation = Assets.Spawn;
            this.x = x;
            this.y = y;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public void Update(float DeltaTime)
        {
            animationTime += DeltaTime;

            if (animationTime >= 1200)
                ToRemove = true;
            else
                animation.Update(DeltaTime);
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy 'Graphics' pozwalaj¹cy tworzyæ nowe obrazy, czcionki, rysowaæ na ekranie.</param>
        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(animation.Image, x, y);
        }
        #endregion
    }
}