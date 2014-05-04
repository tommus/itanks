using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;

namespace iTanks.Game.Objects
{
    public class Bonus : Actor
    {
        #region Internal Classes
        public class Type
        {
            public const int GRANADE = 0;
            public const int SHIELD = 1;
            public const int SHOVEL = 2;
            public const int SHIP = 3;
            public const int STAR = 4;
            public const int TANK = 5;
            public const int TIMER = 6;
        }
        #endregion
        #region Fields
        public static int AVAILABLE_TIME = 10000;

        private Sound bonusSound;
        protected Animation animation;
        #endregion
        #region Constructors
        public Bonus(int x, int y, int type) : base(type, x, y)
        {
            bonusSound = Assets.BonusSound;
            animation = Assets.Bonuses[type];

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

        /// <summary>
        /// Metoda odpowiadaj¹ca za reakcjê na zderzenie z obiektem przekazanym jako parametr.
        /// </summary>
        /// <param name="a">Obiekt, z którym dosz³o do zderzenia.</param>
        public override void Collision(Actor a)
        {
            if (a is Player)
            {
                ToRemove = true;
                bonusSound.Play(1.0f);
            }
        }
        #endregion
    }
}