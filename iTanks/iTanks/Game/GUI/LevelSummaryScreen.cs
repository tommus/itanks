using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace iTanks.Game
{
    public class LevelSummaryScreen : Screen
    {
        #region Fields
        private Player player;
        private Level level;
        private int easyPoints;
        private int mediumPoints;
        private int hardPoints;
        private int score;
        private float accumulatedTime;
        private Sound scoreSound;

        private int updateValue = 1;
        private int tempEasy;
        private int tempMedium;
        private int tempHard;
        private Boolean newScore;
        #endregion
        #region Constructors
        public LevelSummaryScreen(global::GameFramework.Game game) : base(game)
        {
            newScore = false;
            level = Level.Instance;
            player = Player.Instance;
            easyPoints = 0;
            mediumPoints = 0;
            hardPoints = 0;
            accumulatedTime = 0;
            scoreSound = Assets.ScoreSound;
            tempEasy = level.Enemies[0];
            tempMedium = level.Enemies[1];
            tempHard = level.Enemies[2];
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
            int value = (int)accumulatedTime / 20;

            switch(updateValue)
            {
                case 1:
                    if(value > 5)
                    {
                        accumulatedTime = 0;
                        if(easyPoints == tempEasy)
                        {
                            ++updateValue;
                            break;
                        }
                        ++easyPoints;
                        scoreSound.Play(1.0f);
                    }
                    break;
                case 2:
                    if (value > 5)
                    {
                        accumulatedTime = 0;
                        if (mediumPoints == tempMedium)
                        {
                            ++updateValue;
                            break;
                        }
                        ++mediumPoints;
                        scoreSound.Play(1.0f);
                    }
                    break;
                case 3:
                    if (value > 5)
                    {
                        accumulatedTime = 0;
                        if (hardPoints == tempHard)
                        {
                            ++updateValue;
                            break;
                        }
                        ++hardPoints;
                        scoreSound.Play(1.0f);
                    }
                    break;
                case 4:
                    if(value > 20)
                    {
                        player.Score += score;
                        if(!player.Alive || !level.EagleAlive)
                        {
                            if (!newScore)
                                if (Highscores.Add(player.Score))
                                {
                                    newScore = true;
                                    Highscores.SaveHighscores();
                                }
                            if(value > 200)
                                {
                                    TouchPanel.EnabledGestures = GestureType.None;
                                    game.Screen = new MainMenuScreen(game);
                                }
                        }
                        else
                        {
                            if (value > 200)
                            {
                                player.PreparePlayer(false);
                                game.Screen = new LevelLoadingScreen(game, level.Number + 1);
                                TouchPanel.EnabledGestures = GestureType.None;
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public override void Draw(float DeltaTime)
        {
            Graphics graphics = game.Graphics;
            graphics.DrawImage(Assets.SummaryScreenImage);

            int posX = 388 - (int)Assets.BrickFont.MeasureString(easyPoints.ToString()).X;
            int posY = 150;
            graphics.DrawString(Assets.BrickFont, easyPoints.ToString(), posX, posY, Color.White);

            posX = 606 - (int)Assets.BrickFont.MeasureString(easyPoints.ToString()).X;
            graphics.DrawString(Assets.BrickFont, easyPoints.ToString(), posX, posY, Color.White);

            posX = 388 - (int)Assets.BrickFont.MeasureString(mediumPoints.ToString()).X;
            posY += 51;
            graphics.DrawString(Assets.BrickFont, mediumPoints.ToString(), posX, posY, Color.White);

            posX = 606 - (int)Assets.BrickFont.MeasureString((mediumPoints*2).ToString()).X;
            graphics.DrawString(Assets.BrickFont, (mediumPoints * 2).ToString(), posX, posY, Color.White);

            posX = 388 - (int)Assets.BrickFont.MeasureString(hardPoints.ToString()).X;
            posY += 56;
            graphics.DrawString(Assets.BrickFont, hardPoints.ToString(), posX, posY, Color.White);

            posX = 606 - (int)Assets.BrickFont.MeasureString((hardPoints*3).ToString()).X;
            graphics.DrawString(Assets.BrickFont, (hardPoints*3).ToString(), posX, posY, Color.White);

            score = easyPoints + (mediumPoints * 2) + (hardPoints * 3);
            posX = 606 - (int)Assets.BrickFont.MeasureString(score.ToString()).X;
            posY = 334;
            graphics.DrawString(Assets.BrickFont, score.ToString(), posX, posY, Color.White);

            if(newScore)
            {
                String text = "New highscore!\n\nCheck it out!";
                int width = (int)Assets.BrickFont.MeasureString(text).X;
                int height = (int)Assets.BrickFont.MeasureString(text).Y;
                posX = graphics.HalfWidth - (width / 2);
                posY = graphics.HalfHeight - (height / 2);
                graphics.DrawImage(Assets.BlackBox, posX - 30, posY - 30, width + 60, height + 60);
                graphics.DrawString(Assets.BrickFont, text, posX, posY, Color.White);
            }
        }

        /// <summary>
        /// Metoda obs³uguj¹ca przycisk powrotu.
        /// </summary>
        public override void Back()
        {
            TouchPanel.EnabledGestures = GestureType.None;
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