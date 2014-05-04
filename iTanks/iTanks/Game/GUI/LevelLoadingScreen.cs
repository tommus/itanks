using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Input.Touch;

namespace iTanks.Game
{
    public class LevelLoadingScreen : Screen
    {
        #region Fields
        private int percentage;
        private float accumulatedTime;
        private Level level;
        private Player player;
        #endregion
        #region Constructors
        public LevelLoadingScreen(global::GameFramework.Game game, int level)
            : base(game)
        {
            player = Player.Instance;
            this.level = Level.Instance;
            this.level.PrepareLevel(level);

            percentage = 0;
            accumulatedTime = .0f;
        }

        public LevelLoadingScreen(global::GameFramework.Game game, Brigade brigade, Map map)
            : base(game)
        {
            player = Player.Instance;
            this.level = Level.Instance;
            this.level.PrepareLevel(map, brigade);

            percentage = 0;
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
            percentage = (int)(accumulatedTime / 25);

            if (percentage > 100)
            {
                TouchPanel.EnabledGestures = GestureType.Hold;
                game.Screen = new MainGameScreen(game);
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public override void Draw(float DeltaTime)
        {
            Graphics graphics = game.Graphics;

            int tempY = Assets.LoadingText.Height + Assets.LoadingBorder.Height + 10;

            int posX = graphics.HalfWidth - (Assets.LoadingText.Width / 2);
            int posY = graphics.HalfHeight - (tempY / 2);
            graphics.DrawImage(Assets.LoadingText, posX, posY);

            posX = graphics.HalfWidth - (Assets.LoadingBorder.Width / 2);
            posY += Assets.LoadingText.Height + 10;
            graphics.DrawImage(Assets.LoadingBorder, posX, posY);

            posX = graphics.HalfWidth - (Assets.LoadingBar.Width / 2) - 3;
            posY += 12;
            graphics.DrawImage(Assets.LoadingBar, posX, posY, Assets.LoadingBar.Width * percentage / 100, Assets.LoadingBar.Height);
        }

        /// <summary>
        /// Metoda obs³uguj¹ca przycisk powrotu.
        /// </summary>
        public override void Back()
        {
            TouchPanel.EnabledGestures = GestureType.None | GestureType.Tap;
            game.Screen = new LevelSelectionScreen(game);
        }

        /// <summary>
        /// Metoda zwalniaj¹ca przydzielone zasoby.
        /// </summary>
        public override void Dispose()
        {
        }
        #endregion
    }
}