using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using iTanks.Game.Objects;

namespace iTanks.Game
{
    public class Explosion
    {
        #region Fields
        private Animation animation;
        private int x;
        private int y;
        private float animationTime;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje informacjê, czy obiekt nale¿y usun¹æ z listy przechowywanej
        /// w instancji nadzoruj¹cej grê.
        /// </summary>
        public Boolean ToRemove { get; set; }
        #endregion
        #region Constructors
        public Explosion(int x, int y)
        {
            ToRemove = false;
            animation = Assets.Explosion;
            this.x = x - 5;
            this.y = y - 5;
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

            if (animationTime >= 300)
                ToRemove = true;
            else
                animation.Update(DeltaTime);
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy Graphics umo¿liwiaj¹cej tworzenie obrazów, czcionek, rysowanie na ekranie.</param>
        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(animation.Image, x, y);
        }
        #endregion
    }
}