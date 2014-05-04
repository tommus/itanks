using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;

namespace iTanks.Game
{
    public class Tutorial1Screen : Screen
    {
        #region Fields
        private Button LeftArrow;
        private Button RightArrow;
        private String[] text = { "Survive and destroy every enemy\n", "that appear on your way\n\n", "protect eagle\n\n", "get as many points as possible" };
        #endregion
        #region Constructors
        public Tutorial1Screen(global::GameFramework.Game game)
            : base(game)
        {
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.VerticalDrag;

            Graphics graphics = game.Graphics;

            int posX = 0;
            int posY = graphics.HalfHeight - (Assets.ArrowLeft.Height / 2);
            LeftArrow = new Button(Assets.ArrowLeft, Assets.ArrowLeft, posX, posY);
            posX = graphics.Width - Assets.ArrowRight.Width;
            RightArrow = new Button(Assets.ArrowRight, Assets.ArrowRight, posX, posY);
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
            while(TouchPanel.IsGestureAvailable)
            {
                GestureSample sample = TouchPanel.ReadGesture();
                if(sample.GestureType == GestureType.Tap)
                {
                    int posX = (int)sample.Position.X;
                    int posY = (int)sample.Position.Y;

                    if(LeftArrow.Intersects(posX, posY))
                    {
                        Back();
                    }
                    if (RightArrow.Intersects(posX, posY))
                    {
                        game.Screen = new Tutorial2Screen(game);
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

            int posX = graphics.HalfWidth - (Assets.GoalsText.Width / 2);

            graphics.DrawImage(Assets.TutorialBorder);

            graphics.DrawScaledImage(Assets.BlackBox, LeftArrow.Bounds.X, LeftArrow.Bounds.Y, LeftArrow.Bounds.Width, LeftArrow.Bounds.Height);
            LeftArrow.Draw(graphics);

            graphics.DrawScaledImage(Assets.BlackBox, RightArrow.Bounds.X, RightArrow.Bounds.Y, RightArrow.Bounds.Width, RightArrow.Bounds.Height);
            RightArrow.Draw(graphics);

            graphics.DrawScaledImage(Assets.BlackBox, posX - 10, 20, Assets.GoalsText.Width + 20, Assets.GoalsText.Height);
            graphics.DrawImage(Assets.GoalsText, posX, 20);

            for(int i = 0; i < text.Length; ++i)
            {
                int sumY = 0;
                for(int j = 0; j < i; ++j)
                {
                    sumY += (int)(Assets.BrickFont.MeasureString(text[j]).Y);
                }
                posX = graphics.HalfWidth - (int)(Assets.BrickFont.MeasureString(text[i]).X / 2);
                int posY = 120 + sumY;
                graphics.DrawString(Assets.BrickFont, text[i], posX, posY, Color.White);
            }
        }

        /// <summary>
        /// Metoda obs³uguj¹ca przycisk powrotu.
        /// </summary>
        public override void Back()
        {
            game.Screen = new MainMenuScreen(game);
            TouchPanel.EnabledGestures = GestureType.None;
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