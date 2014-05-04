using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace iTanks.Game
{
    public class Tutorial2Screen : Screen
    {
        #region Fields
        private Button LeftArrow;
        private Button RightArrow;
        private String[] text = { "at the begining of the game\n", "player has three lives\n\n",
                                    "to move your tank press or hold\n", "one of the arrows on the left side\n\n",
                                    "to make a shot press a button on\n", "the right side\n\n",
                                    "pick bonuses to enhance your tank" };
        private int slideY;
        private int maxSlide;
        #endregion
        #region Constructors
        public Tutorial2Screen(global::GameFramework.Game game)
            : base(game)
        {
            slideY = 120;
            maxSlide = 191;

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
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample sample = TouchPanel.ReadGesture();
                if (sample.GestureType == GestureType.Tap)
                {
                    int posX = (int)sample.Position.X;
                    int posY = (int)sample.Position.Y;

                    if (LeftArrow.Intersects(posX, posY))
                    {
                        Back();
                    }
                    if (RightArrow.Intersects(posX, posY))
                    {
                        game.Screen = new Tutorial3Screen(game);
                    }
                }
                if(sample.GestureType == GestureType.VerticalDrag)
                {
                    int newY = slideY + (int)sample.Delta.Y;

                    if (newY >= (120 - maxSlide) && newY <= 120)
                    {
                        slideY = newY;
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

            int posX = graphics.HalfWidth - (Assets.PlayerText.Width / 2);

            for (int i = 0; i < text.Length; ++i)
            {
                int sumY = 0;
                for (int j = 0; j < i; ++j)
                {
                    sumY += (int)(Assets.BrickFont.MeasureString(text[j]).Y);
                }
                int possX = graphics.HalfWidth - (int)(Assets.BrickFont.MeasureString(text[i]).X / 2);
                int posY = slideY + sumY;
                graphics.DrawString(Assets.BrickFont, text[i], possX, posY, Color.White);
            }

            graphics.DrawImage(Assets.TutorialBorder);

            if (slideY >= 119)
            {
                String txt = "slide up";
                int possX = graphics.HalfWidth - (int)(Assets.BrickFont.MeasureString(txt).X / 2);
                int posY = graphics.Height - 80;
                graphics.DrawString(Assets.BrickFont, txt, possX + 40, posY, 0.6f, Color.White);
            }

            graphics.DrawScaledImage(Assets.BlackBox, LeftArrow.Bounds.X, LeftArrow.Bounds.Y, LeftArrow.Bounds.Width, LeftArrow.Bounds.Height);
            LeftArrow.Draw(graphics);

            graphics.DrawScaledImage(Assets.BlackBox, RightArrow.Bounds.X, RightArrow.Bounds.Y, RightArrow.Bounds.Width, RightArrow.Bounds.Height);
            RightArrow.Draw(graphics);

            graphics.DrawScaledImage(Assets.BlackBox, posX - 10, 20, Assets.PlayerText.Width + 20, Assets.PlayerText.Height);
            graphics.DrawImage(Assets.PlayerText, posX, 20);
        }

        /// <summary>
        /// Metoda obs³uguj¹ca przycisk powrotu.
        /// </summary>
        public override void Back()
        {
            game.Screen = new Tutorial1Screen(game);
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