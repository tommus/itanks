using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace iTanks.Game
{
    public class MainGameScreen : Screen
    {
        #region Fields
        private Player player;
        private Level level;
        private Button ArrowLeft;
        private Button ArrowRight;
        private Button ArrowUp;
        private Button ArrowDown;
        private Button FireButton;
        private float timer;
        #endregion
        #region Constructors
        public MainGameScreen(global::GameFramework.Game game)
            : base(game)
        {
            player = Player.Instance;
            level = Level.Instance;
            ArrowLeft = new Button(48, 407, 18);
            ArrowRight = new Button(128, 407, 18);
            ArrowUp = new Button(90, 372, 18);
            ArrowDown = new Button(90, 440, 18);
            FireButton = new Button(712, 408, 32);

            level.PlayEntrance();
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
            if (player.Alive)
            {
                foreach (TouchLocation location in TouchPanel.GetState())
                {
                    int x = (int)location.Position.X;
                    int y = (int)location.Position.Y;

                    if (location.State == TouchLocationState.Pressed)
                    {
                        if (ArrowLeft.Intersects(x, y))
                        {
                            player.IsMoving = true;
                            player.Direction = Direction.LEFT;
                        }
                        if (ArrowRight.Intersects(x, y))
                        {
                            player.IsMoving = true;
                            player.Direction = Direction.RIGHT;
                        }
                        if (ArrowUp.Intersects(x, y))
                        {
                            player.IsMoving = true;
                            player.Direction = Direction.UP;
                        }
                        if (ArrowDown.Intersects(x, y))
                        {
                            player.IsMoving = true;
                            player.Direction = Direction.DOWN;
                        }
                        if (FireButton.Intersects(x, y))
                        {
                            player.PlayFire();
                            int direction = player.Direction;
                            int offsetX = 0;
                            int offsetY = 0;
                            switch (direction)
                            {
                                case Direction.UP:
                                    offsetX = player.Bounds.Width / 2 - 4;
                                    break;
                                case Direction.DOWN:
                                    offsetX = player.Bounds.Width / 2 - 4;
                                    offsetY = player.Bounds.Height;
                                    break;
                                case Direction.LEFT:
                                    offsetY = player.Bounds.Height / 2 - 4;
                                    break;
                                case Direction.RIGHT:
                                    offsetX = player.Bounds.Width;
                                    offsetY = player.Bounds.Height / 2 - 4;
                                    break;
                            }
                            level.AddBullet(new Objects.Bullet(player.Bounds.X + offsetX, player.Bounds.Y + offsetY, direction, player, player.BulletSpeed));
                        }

                        if (x >= 177 && y >= 18 && x <= 620 && y <= 460)
                        {
                            while (TouchPanel.IsGestureAvailable)
                            {
                                GestureSample sample = TouchPanel.ReadGesture();

                                if (sample.GestureType == GestureType.Hold)
                                {
                                    // it is good place to implement game menu
                                    // maybe i will add it in the future
                                }
                            }
                        }
                    }

                    if (location.State == TouchLocationState.Released)
                        player.IsMoving = false;
                }
            }

            if(level.Cleared && level.Number == 50)
                player.Alive = false;

            if (level.Cleared || !player.Alive || !level.EagleAlive)
            {
                if (!player.Alive || !level.EagleAlive)
                    level.PlayOutrance();

                timer += DeltaTime;

                if (timer >= 3000)
                {
                    TouchPanel.EnabledGestures = GestureType.None | GestureType.Tap;
                    game.Screen = new LevelSummaryScreen(game);
                }
            }


            level.CheckCollisions(player);
            level.Brigade.CheckCollisions(player);
            
            player.Update(DeltaTime);
            level.Update(DeltaTime);
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public override void Draw(float DeltaTime)
        {
            Graphics graphics = game.Graphics;

            graphics.DrawImage(Assets.BrickBackground);
            graphics.DrawImage(Assets.GameBoard);

            String text = "Enemies";
            int posX = graphics.Width - (int)Assets.BrickFont.MeasureString(text).X - 10;
            int posY = 20;
            graphics.DrawString(Assets.BrickFont, text, posX, posY, Color.White);

            text = level.Enemies[3].ToString();
            posX += (int)((Assets.BrickFont.MeasureString("Enemies").X - Assets.BrickFont.MeasureString(text).X) / 2);
            posY += 10 + (int)(Assets.BrickFont.MeasureString(text).Y);
            graphics.DrawString(Assets.BrickFont, text, posX, posY, Color.White);

            text = "Level";
            posX = 35;
            posY = 20;
            graphics.DrawString(Assets.BrickFont, text, posX, posY, Color.White);

            text = level.Number.ToString();
            posX += (int)((Assets.BrickFont.MeasureString("Level").X - Assets.BrickFont.MeasureString(text).X) / 2);
            posY += 10 + (int)(Assets.BrickFont.MeasureString(text).Y);
            graphics.DrawString(Assets.BrickFont, text, posX, posY, Color.White);

            text = "Lives";
            posX = 35 + (int)((Assets.BrickFont.MeasureString("Level").X - Assets.BrickFont.MeasureString(text).X) / 2);
            posY += 60 + (int)(Assets.BrickFont.MeasureString("Level").Y);
            graphics.DrawString(Assets.BrickFont, text, posX, posY, Color.White);

            text = player.Lives.ToString();
            posX += (int)((Assets.BrickFont.MeasureString("Lives").X - Assets.BrickFont.MeasureString(text).X) / 2);
            posY += 10 + (int)(Assets.BrickFont.MeasureString(text).Y);
            graphics.DrawString(Assets.BrickFont, text, posX, posY, Color.White);

            level.Draw(graphics);
            player.Draw(graphics);
            level.DrawTrees(graphics);
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