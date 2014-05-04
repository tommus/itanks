using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Input.Touch;
using iTanks.Game.Objects;
using Microsoft.Xna.Framework;

namespace iTanks.Game
{
    public class EditorScreen : Screen
    {
        #region Internal Structures
        /// <summary>
        /// Struktura pomocnicza przechowuj¹ca pary znak - grafika.
        /// </summary>
        public struct Part
        {
            public char Sign;
            public Image Image;
        }
        #endregion
        #region Fields
        private Button LeftArrow;
        private Button RightArrow;
        private Part[,] map;
        private Button[] picks;
        private String picksString = "ZMPOQNUTSRDGFHELKJIABC";
        private Part selection;
        private Brigade brigade;
        private Player player;
        #endregion
        #region Constructors
        public EditorScreen(global::GameFramework.Game game)
            : base(game)
        {
            player = Player.Instance;
            player.PreparePlayer(true);

            Graphics graphics = game.Graphics;

            int posX = 0;
            int posY = graphics.HalfHeight - (Assets.ArrowLeft.Height / 2);
            LeftArrow = new Button(Assets.ArrowLeft, Assets.ArrowLeft, posX, posY);
            posX = graphics.Width - Assets.ArrowRight.Width;
            RightArrow = new Button(Assets.ArrowRight, Assets.ArrowRight, posX, posY);

            map = new Part[13, 13];
            picks = new Button[22];

            picks[0] = new Button(null, null, 604, 17);
            picks[0].SetSize(34, 34);
            picks[1] = new Button(graphics.NewImage("Editor/SolidBrick"), null, 639, 17);

            picks[2] = new Button(graphics.NewImage("Editor/SBrick"), null, 604, 51);
            picks[3] = new Button(graphics.NewImage("Editor/WBrick"), null, 639, 51);

            picks[4] = new Button(graphics.NewImage("Editor/EBrick"), null, 604, 85);
            picks[5] = new Button(graphics.NewImage("Editor/NBrick"), null, 639, 85);

            picks[6] = new Button(graphics.NewImage("Editor/SWBrick"), null, 604, 119);
            picks[7] = new Button(graphics.NewImage("Editor/SEBrick"), null, 639, 119);

            picks[8] = new Button(graphics.NewImage("Editor/NWBrick"), null, 604, 153);
            picks[9] = new Button(graphics.NewImage("Editor/NEBrick"), null, 639, 153);

            picks[10] = new Button(graphics.NewImage("Editor/SolidStone"), null, 604, 187);
            picks[11] = new Button(graphics.NewImage("Editor/SStone"), null, 639, 187);

            picks[12] = new Button(graphics.NewImage("Editor/WStone"), null, 604, 221);
            picks[13] = new Button(graphics.NewImage("Editor/EStone"), null, 639, 221);

            picks[14] = new Button(graphics.NewImage("Editor/NStone"), null, 604, 255);
            picks[15] = new Button(graphics.NewImage("Editor/SWStone"), null, 639, 255);

            picks[16] = new Button(graphics.NewImage("Editor/SEStone"), null, 604, 289);
            picks[17] = new Button(graphics.NewImage("Editor/NWStone"), null, 639, 289);

            picks[18] = new Button(graphics.NewImage("Editor/NEStone"), null, 604, 323);
            picks[19] = new Button(graphics.NewImage("Editor/Road"), null, 639, 323);

            picks[20] = new Button(graphics.NewImage("Editor/Tree"), null, 604, 357);
            picks[21] = new Button(graphics.NewImage("Editor/Water"), null, 639, 357);

            brigade = new Brigade();
        }
        #endregion

        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public override void Update(float DeltaTime)
        {
            foreach (TouchLocation location in TouchPanel.GetState())
            {
                if (location.State == TouchLocationState.Released)
                {
                    int x = (int)location.Position.X;
                    int y = (int)location.Position.Y;

                    if(LeftArrow.Intersects(x, y))
                    {
                        Back();
                    }
                    if(RightArrow.Intersects(x, y))
                    {
                        Map preparedMap = new Map(map);
                        game.Screen = new LevelLoadingScreen(game, brigade, preparedMap);
                    }

                    for (int i = 0; i < picks.Length; ++i)
                        if (picks[i].Intersects(x, y))
                        {
                            selection.Sign = picksString[i];
                            if (picks[i].Image == null)
                                selection.Image = Assets.BlackBox;
                            else
                                selection.Image = picks[i].Image;
                        }

                    if(x >= 124 && x <=569 && y >= 17 && y <= 462)
                    {
                        int col = (x - 124) / 34;
                        int row = (y - 17) / 34;
                        if ((row == 12 && col == 6) || (row == 0 && col == 0) || (row == 0 && col == 6) || (row == 0 && col == 12) || (row == 12 && col == 4))
                            break;
                        map[col, row].Sign = selection.Sign;
                        map[col, row].Image = selection.Image;
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

            LeftArrow.Draw(graphics);
            RightArrow.Draw(graphics);

            foreach(Button button in picks)
                if(button != null)
                    button.Draw(graphics);

            if(selection.Image != null)
                graphics.DrawScaledImage(selection.Image, 604, 391, 70, 70);

            for (int i = 0; i < 13; ++i)
                for (int j = 0; j < 13; ++j)
                    if(map[i, j].Image != null)
                        graphics.DrawImage(map[i, j].Image, 126 + i * 34, 19 + j * 34, Color.White);

            graphics.DrawImage(Assets.Blocks[Block.Type.EAGLE], 330, 427, Color.White);

            graphics.DrawImage(Assets.EditorBoard);
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
    }
}