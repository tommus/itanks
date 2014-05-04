using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using iTanks.Game.GUI;

namespace iTanks.Game
{
    public class HighscoresScreen : Screen
    {
        #region Fields
        private Button LeftArrow;
        private String[] text;

        private Boolean showMenu;
        private Screen menu;
        private int slideY;
        private int maxSlide;
        #endregion
        #region Constructors
        public HighscoresScreen(global::GameFramework.Game game)
            : base(game)
        {
            text = new String[10];
            text = Highscores.GetScores();

            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.Hold | GestureType.VerticalDrag;

            Graphics graphics = game.Graphics;

            int posX = 0;
            int posY = graphics.HalfHeight - (Assets.ArrowLeft.Height / 2);
            LeftArrow = new Button(Assets.ArrowLeft, Assets.ArrowLeft, posX, posY);

            slideY = 120;
            maxSlide = 130;

            showMenu = false;
            menu = new HighscoresMenuScreen(game);
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
            text = Highscores.GetScores();

            if (showMenu)
            {
                menu.Update(DeltaTime);
                showMenu = ((HighscoresMenuScreen)menu).AtExit();
            }
            else
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
                    }
                    if (sample.GestureType == GestureType.Hold)
                    {
                        TouchPanel.EnabledGestures = GestureType.Tap;
                        showMenu = true;
                    }
                    if (sample.GestureType == GestureType.VerticalDrag)
                    {
                        int newY = slideY + (int)sample.Delta.Y;

                        if (newY >= (120 - maxSlide) && newY <= 120)
                        {
                            slideY = newY;
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
            
            int posX = graphics.HalfWidth - (Assets.HighscoresText.Width / 2);

            for (int i = 0; i < 5; ++i)
            {
                int sumY = 0;
                for (int j = 0; j < i; ++j)
                {
                    sumY += (int)(Assets.BrickFont.MeasureString(text[j]).Y) + 20;
                }
                int possX = graphics.HalfWidth - (int)(Assets.BrickFont.MeasureString(text[i]).X) - 120;
                int posY = slideY + sumY;
                graphics.DrawString(Assets.BrickFont, "" + (i+1), 160, posY, Color.Red);
                graphics.DrawString(Assets.BrickFont, text[i], possX, posY, Color.White);
            }

            for (int i = 5; i < text.Length; ++i)
            {
                int sumY = 0;
                for (int j = 5; j < i; ++j)
                {
                    sumY += (int)(Assets.BrickFont.MeasureString(text[j]).Y) + 20;
                }
                int possX = graphics.HalfWidth - (int)(Assets.BrickFont.MeasureString(text[i]).X) + 200;
                int posY = slideY + sumY;

                if (i != text.Length - 1)
                {
                    graphics.DrawString(Assets.BrickFont, "" + (i + 1), 480, posY, Color.Red);
                }
                else
                {
                    graphics.DrawString(Assets.BrickFont, "" + (i + 1), 467, posY, Color.Red);
                }
                graphics.DrawString(Assets.BrickFont, text[i], possX+10, posY, Color.White);
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

            graphics.DrawScaledImage(Assets.BlackBox, posX - 10, 20, Assets.HighscoresText.Width + 20, Assets.HighscoresText.Height);
            graphics.DrawImage(Assets.HighscoresText, posX, 20);

            if (showMenu)
                menu.Draw(DeltaTime);
        }

        /// <summary>
        /// Metoda obs³uguj¹ca przycisk powrotu.
        /// </summary>
        public override void Back()
        {
            if (showMenu)
            {
                menu.Back();
            }
            else
            {
                game.Screen = new MainMenuScreen(game);
                TouchPanel.EnabledGestures = GestureType.None;
            }
        }

        /// <summary>
        /// Metoda zwalniaj¹ca przydzielone zasoby.
        /// </summary>
        public override void Dispose()
        {
            menu.Dispose();
        }
        #endregion
    }
}