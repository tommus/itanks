using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Input.Touch;
using iTanks.Game.AI;

namespace iTanks.Game
{
    public class MainMenuScreen : Screen
    {
        #region Fields
        private Button NewGameButton;
        private Button TutorialButton;
        private Button HighscoresButton;
        private Button EditorButton;
        private Button ExitButton;
        #endregion
        #region Constructors
        public MainMenuScreen(global::GameFramework.Game game)
            : base(game)
        {
            Graphics graphics = game.Graphics;
            int sumY = Assets.ButtonBackground.Height * 5 + 40;
            int posX = graphics.HalfWidth - (Assets.ButtonBackground.Width / 2);

            for (int i = 0; i < 5; ++i)
            {
                int posY = graphics.HalfHeight - (sumY / 2) + (i * (Assets.ButtonBackground.Height + 10));
                switch (i)
                {
                    case 0:
                        NewGameButton = new Button(Assets.ButtonBackground, Assets.NewGameText, posX, posY);
                        break;
                    case 1:
                        TutorialButton = new Button(Assets.ButtonBackground, Assets.TutorialText, posX, posY);
                        break;
                    case 2:
                        HighscoresButton = new Button(Assets.ButtonBackground, Assets.HighscoresText, posX, posY);
                        break;
                    case 3:
                        EditorButton = new Button(Assets.ButtonBackground, Assets.EditorText, posX, posY);
                        break;
                    case 4:
                        ExitButton = new Button(Assets.ButtonBackground, Assets.ExitText, posX, posY);
                        break;
                }
            }
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
            foreach(TouchLocation location in TouchPanel.GetState())
            {
                if(location.State == TouchLocationState.Released)
                {
                    int x = (int)location.Position.X;
                    int y = (int)location.Position.Y;

                    if (NewGameButton.Intersects(x, y))
                    {
                        game.Screen = new LevelSelectionScreen(game);
                    }
                    if (TutorialButton.Intersects(x, y))
                    {
                        game.Screen = new Tutorial1Screen(game);
                    }
                    if (HighscoresButton.Intersects(x, y))
                    {
                        game.Screen = new HighscoresScreen(game);
                    }
                    if (EditorButton.Intersects(x, y))
                    {
                        game.Screen = new EditorScreen(game);
                    }
                    if (ExitButton.Intersects(x, y))
                    {
                        Back();
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
        
            graphics.DrawImage(Assets.BrickBackground);
            NewGameButton.Draw(graphics);
            TutorialButton.Draw(graphics);
            HighscoresButton.Draw(graphics);
            EditorButton.Draw(graphics);
            ExitButton.Draw(graphics);
        }

        /// <summary>
        /// Metoda obs³uguj¹ca przycisk powrotu.
        /// </summary>
        public override void Back()
        {
            ((Microsoft.Xna.Framework.Game)game).Exit();
            Dispose();
        }

        /// <summary>
        /// Metoda zwalniaj¹ca przydzielone zasoby.
        /// </summary>
        public override void Dispose()
        {
            Assets.DisposeAll();
        }
        #endregion
    }
}
