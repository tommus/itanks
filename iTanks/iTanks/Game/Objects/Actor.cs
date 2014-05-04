using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameFramework;

namespace iTanks.Game.Objects
{
    public abstract class Actor
    {
        #region Fields
        protected int x;
        protected int y;
        protected int width;
        protected int height;
        protected int type;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje informacjê, czy obiekt nale¿y usun¹æ z listy
        /// nadzoruj¹cej w g³ównej grze.
        /// </summary>
        public Boolean ToRemove { get; set; }

        /// <summary>
        /// Parametr przechowuje informacje o wymiarach i po³o¿eniu obiektu.
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle(x, y, width, height); }
        }
        #endregion
        #region Constructors
        public Actor(int type, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.type = type;
            ToRemove = false;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda sprawdzaj¹ca, czy obiekt koliduje z obiektem przekazanym jako parametr.
        /// </summary>
        /// <param name="a">Obiekt, z którym badane jest zachodzenie kolizji.</param>
        public void CheckCollision(Actor a)
        {
            if(a != null)
            {
                Rectangle rect = a.Bounds;
                if(Bounds.Intersects(rect))
                {
                    Collision(a);
                    a.Collision(this);
                }
            }
        }
        #endregion
        #region Methods to implement in derrived classes
        /// <summary>
        /// Metoda odpowiedzialna za reakcjê na kolizje z obiektem.
        /// </summary>
        /// <param name="a">Obiekt, z którym badane jest zachodzenie kolizji.</param>
        public virtual void Collision(Actor a) {}

        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public virtual void Update(float DeltaTime) {}

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public virtual void Draw(Graphics graphics) { }
        #endregion
    }
}