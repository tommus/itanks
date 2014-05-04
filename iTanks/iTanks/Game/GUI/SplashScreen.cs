using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework;

namespace iTanks.Game
{
    public class SplashScreen : Screen
    {
        #region Fields
        private float transparency;
        private float accumulatedTime;
        #endregion
        #region Constructors
        public SplashScreen(global::GameFramework.Game game)
            : base(game)
        {
            Graphics graphics = game.Graphics;
            Assets.Splash = graphics.NewImage("GUI/TDClogo");

            transparency = .0f;
            accumulatedTime = .0f;
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
            accumulatedTime += DeltaTime;
            float temp = accumulatedTime / 100;

            if(temp <= 10)
            {
                transparency += .1f;
            }

            if(temp >= 30 && temp < 40)
            {
                transparency -= .1f;
            }

            if (temp >= 40)
            {
                game.Screen = new GameLoadingScreen(game);
                Dispose();
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public override void Draw(float DeltaTime)
        {
            Graphics graphics = game.Graphics;
            int posX = (graphics.Width/2) - (Assets.Splash.Width/2);
            int posY = (graphics.Height/2) - (Assets.Splash.Height/2);
            graphics.DrawImage(Assets.Splash, posX, posY, Color.White * transparency);
        }

        /// <summary>
        /// Metoda obs³uguj¹ca przycisk powrotu.
        /// </summary>
        public override void Back()
        {
            ((Microsoft.Xna.Framework.Game)game).Exit();
        }

        /// <summary>
        /// Metoda zwalniaj¹ca przydzielone zasoby.
        /// </summary>
        public override void Dispose()
        {
            Assets.Splash.Dispose();
        }
        #endregion
    }
}