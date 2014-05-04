using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace iTanks.Game
{
    public class LevelSelectionScreen : Screen
    {
        #region Fields
        private Button LeftArrow;
        private Button[] buttons;
        private Player player;
        #endregion
        #region Constructors
        public LevelSelectionScreen(global::GameFramework.Game game)
            : base(game)
        {
            player = Player.Instance;
            player.PreparePlayer(true);

            TouchPanel.EnabledGestures = GestureType.Tap;

            buttons = new Button[50];
            int buttonSide = 40;

            int buttonNumber = 0;
            int posX = 0;
            int posY = 0;
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    posX = 110 + i * (buttonSide + 20);
                    posY = 130 + j * (buttonSide + 20);
                    buttons[buttonNumber++] = new Button(Assets.BrickFont, posX, posY, buttonSide, buttonSide);
                }
            }

            Graphics graphics = game.Graphics;

            posX = 0;
            posY = graphics.HalfHeight - (Assets.ArrowLeft.Height / 2);
            LeftArrow = new Button(Assets.ArrowLeft, Assets.ArrowLeft, posX, posY);
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
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample sample = TouchPanel.ReadGesture();
                if (sample.GestureType == GestureType.Tap)
                {
                    int posX = (int)sample.Position.X;
                    int posY = (int)sample.Position.Y;

                    if(LeftArrow.Intersects(posX, posY))
                    {
                        Back();
                    }

                    for(int i = 0; i < buttons.Length; ++i)
                    {
                        if(buttons[i].Intersects(posX, posY))
                        {
                            TouchPanel.EnabledGestures = GestureType.None;
                            game.Screen = new LevelLoadingScreen(game, i+1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public override void Draw(float DeltaTime)
        {
            Graphics graphics = game.Graphics;

            graphics.DrawImage(Assets.TutorialBorder);

            int posX = graphics.HalfWidth - (Assets.LevelText.Width / 2);

            graphics.DrawScaledImage(Assets.BlackBox, posX - 10, 20, Assets.LevelText.Width + 20, Assets.LevelText.Height);
            graphics.DrawImage(Assets.LevelText, posX, 20);

            for(int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].Draw(graphics, "" + (i + 1), Color.White);
            }

            graphics.DrawScaledImage(Assets.BlackBox, LeftArrow.Bounds.X, LeftArrow.Bounds.Y, LeftArrow.Bounds.Width, LeftArrow.Bounds.Height);
            LeftArrow.Draw(graphics);
        }

        /// <summary>
        /// Metoda obs³uguj¹ca przycisk powrotu.
        /// </summary>
        public override void Back()
        {
            TouchPanel.EnabledGestures = GestureType.None;
            game.Screen = new MainMenuScreen(game);
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