using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework;
using iTanks.Game.Objects;
using GameFramework.Implementation;
using iTanks.Game.AI;

namespace iTanks.Game
{
    /// <summary>
    /// Celem klasy jest za³adowanie wszystkich potrzebnych elementów do pamiêci.
    /// </summary>
    public class GameLoadingScreen : Screen
    {
        #region Fields
        private int percentage;
        private float accumulatedTime;
        #endregion
        #region Constructors
        public  GameLoadingScreen(global::GameFramework.Game game) : base(game)
        {
            percentage = 0;
            accumulatedTime = .0f;

            Graphics graphics = game.Graphics;
            Assets.BlackBox = graphics.NewImage("GUI/BlackBox");
            Assets.BrickFont = graphics.NewFont("GUI/BrickFont");

            Assets.LoadingText = graphics.NewImage("GUI/LoadingText");
            Assets.LoadingBorder = graphics.NewImage("GUI/LoadingBorder");
            Assets.LoadingBar = graphics.NewImage("GUI/LoadingBar");

            Assets.BrickBackground = graphics.NewImage("GUI/BrickBackground");
            Assets.ButtonBackground = graphics.NewImage("GUI/ButtonBackground");
            Assets.NewGameText = graphics.NewImage("GUI/NewGameText");
            Assets.TutorialText = graphics.NewImage("GUI/TutorialText");
            Assets.EditorText = graphics.NewImage("GUI/EditorText");
            Assets.HighscoresText = graphics.NewImage("GUI/HighscoresText");
            Assets.ExitText = graphics.NewImage("GUI/ExitText");

            Assets.ArrowLeft = graphics.NewImage("GUI/ArrowLeft");
            Assets.ArrowRight = graphics.NewImage("GUI/ArrowRight");
            Assets.TutorialBorder = graphics.NewImage("GUI/TutorialBorder");
            Assets.GoalsText = graphics.NewImage("GUI/GoalsText");
            Assets.PlayerText = graphics.NewImage("GUI/PlayerText");
            Assets.BonusesText = graphics.NewImage("GUI/BonusesText");
            Assets.EnemiesText = graphics.NewImage("GUI/EnemiesText");

            Assets.ResetText = graphics.NewImage("GUI/ResetText");

            Assets.LevelText = graphics.NewImage("GUI/LevelText");

            Assets.GameBoard = graphics.NewImage("GUI/GameBoard");

            Assets.Blocks[Block.Type.BRICK] = graphics.NewImage("Blocks/Brick");
            Assets.Blocks[Block.Type.STONE] = graphics.NewImage("Blocks/Stone");
            Assets.Blocks[Block.Type.EAGLE] = graphics.NewImage("Blocks/EagleAlive");
            Assets.Blocks[Block.Type.TREE] = graphics.NewImage("Blocks/Tree");
            Assets.Blocks[Block.Type.ROAD] = graphics.NewImage("Blocks/Road");
            Assets.Blocks[Block.Type.BULLET] = graphics.NewImage("Bullet/Up");
            Assets.Blocks[Block.Type.BULLET + 1] = graphics.NewImage("Bullet/Down");
            Assets.Blocks[Block.Type.BULLET + 2] = graphics.NewImage("Bullet/Left");
            Assets.Blocks[Block.Type.BULLET + 3] = graphics.NewImage("Bullet/Right");

            Assets.EditorBoard = graphics.NewImage("GUI/EditorBoard");

            Audio audio = game.Audio;
            Assets.GameStart = audio.NewMusic("Audio/Gamestart");
            Assets.GameOver = audio.NewMusic("Audio/Gameover");
            Assets.Move = audio.NewSound("Audio/Moving");
            Assets.Fire = audio.NewSound("Audio/Fire");
            Assets.Steel = audio.NewSound("Audio/Steel");
            Assets.BrickSound = audio.NewSound("Audio/Brick");
            Assets.Ship = graphics.NewImage("Miscelinous/Ship");
            Assets.Shield = new Animation();
            Assets.Shield.AddFrame(graphics.NewImage("Miscelinous/ShieldOne"), 100);
            Assets.Shield.AddFrame(graphics.NewImage("Miscelinous/ShieldTwo"), 100);
            Assets.BonusSound = audio.NewSound("Audio/Bonus");
            Assets.ExplosionSound = audio.NewSound("Audio/Explosion");

            Assets.SummaryScreenImage = graphics.NewImage("GUI/SummaryScreen");
            Assets.ScoreSound = audio.NewSound("Audio/Score");

            Assets.Explosion = new Animation();
            Assets.Explosion.AddFrame(graphics.NewImage("Miscelinous/ExplosionOne"), 50);
            Assets.Explosion.AddFrame(graphics.NewImage("Miscelinous/ExplosionTwo"), 50);
            Assets.Explosion.AddFrame(graphics.NewImage("Miscelinous/ExplosionThree"), 50);
            Assets.Explosion.AddFrame(graphics.NewImage("Miscelinous/ExplosionFour"), 50);
            Assets.Explosion.AddFrame(graphics.NewImage("Miscelinous/ExplosionThree"), 50);
            Assets.Explosion.AddFrame(graphics.NewImage("Miscelinous/ExplosionTwo"), 50);

            Assets.Spawn = new Animation();
            Assets.Spawn.AddFrame(graphics.NewImage("Miscelinous/SpawnOne"), 100);
            Assets.Spawn.AddFrame(graphics.NewImage("Miscelinous/SpawnTwo"), 100);
            Assets.Spawn.AddFrame(graphics.NewImage("Miscelinous/SpawnThree"), 100);
            Assets.Spawn.AddFrame(graphics.NewImage("Miscelinous/SpawnTwo"), 100);

            Assets.Animations[Block.Type.WATER] = new Animation();
            Assets.Animations[Block.Type.WATER].AddFrame(graphics.NewImage("Blocks/WaterOne"), 100);
            Assets.Animations[Block.Type.WATER].AddFrame(graphics.NewImage("Blocks/WaterTwo"), 100);

            Assets.Player[Player.TankType.NORMAL] = new Animation();
            Assets.Player[Player.TankType.NORMAL].AddFrame(graphics.NewImage("Player/1/y1u"), 100);
            Assets.Player[Player.TankType.NORMAL].AddFrame(graphics.NewImage("Player/1/y2u"), 100);

            Assets.Player[Player.TankType.NORMAL + 1] = new Animation();
            Assets.Player[Player.TankType.NORMAL + 1].AddFrame(graphics.NewImage("Player/1/y1d"), 100);
            Assets.Player[Player.TankType.NORMAL + 1].AddFrame(graphics.NewImage("Player/1/y2d"), 100);

            Assets.Player[Player.TankType.NORMAL + 2] = new Animation();
            Assets.Player[Player.TankType.NORMAL + 2].AddFrame(graphics.NewImage("Player/1/y1l"), 100);
            Assets.Player[Player.TankType.NORMAL + 2].AddFrame(graphics.NewImage("Player/1/y2l"), 100);

            Assets.Player[Player.TankType.NORMAL + 3] = new Animation();
            Assets.Player[Player.TankType.NORMAL + 3].AddFrame(graphics.NewImage("Player/1/y1r"), 100);
            Assets.Player[Player.TankType.NORMAL + 3].AddFrame(graphics.NewImage("Player/1/y2r"), 100);

            Assets.Player[Player.TankType.HEAVY] = new Animation();
            Assets.Player[Player.TankType.HEAVY].AddFrame(graphics.NewImage("Player/2/y1u"), 100);
            Assets.Player[Player.TankType.HEAVY].AddFrame(graphics.NewImage("Player/2/y2u"), 100);

            Assets.Player[Player.TankType.HEAVY + 1] = new Animation();
            Assets.Player[Player.TankType.HEAVY + 1].AddFrame(graphics.NewImage("Player/2/y1d"), 100);
            Assets.Player[Player.TankType.HEAVY + 1].AddFrame(graphics.NewImage("Player/2/y2d"), 100);

            Assets.Player[Player.TankType.HEAVY + 2] = new Animation();
            Assets.Player[Player.TankType.HEAVY + 2].AddFrame(graphics.NewImage("Player/2/y1l"), 100);
            Assets.Player[Player.TankType.HEAVY + 2].AddFrame(graphics.NewImage("Player/2/y2l"), 100);

            Assets.Player[Player.TankType.HEAVY + 3] = new Animation();
            Assets.Player[Player.TankType.HEAVY + 3].AddFrame(graphics.NewImage("Player/2/y1r"), 100);
            Assets.Player[Player.TankType.HEAVY + 3].AddFrame(graphics.NewImage("Player/2/y2r"), 100);

            int bonusFrame = 200;

            Assets.Bonuses[Bonus.Type.SHIP] = new Animation();
            Assets.Bonuses[Bonus.Type.SHIP].AddFrame(graphics.NewImage("Bonuses/Ship"), bonusFrame);
            Assets.Bonuses[Bonus.Type.SHIP].AddFrame(graphics.NewImage("Miscelinous/Transparent"), bonusFrame);

            Assets.Bonuses[Bonus.Type.GRANADE] = new Animation();
            Assets.Bonuses[Bonus.Type.GRANADE].AddFrame(graphics.NewImage("Bonuses/Granade"), bonusFrame);
            Assets.Bonuses[Bonus.Type.GRANADE].AddFrame(graphics.NewImage("Miscelinous/Transparent"), bonusFrame);

            Assets.Bonuses[Bonus.Type.SHIELD] = new Animation();
            Assets.Bonuses[Bonus.Type.SHIELD].AddFrame(graphics.NewImage("Bonuses/Shield"), bonusFrame);
            Assets.Bonuses[Bonus.Type.SHIELD].AddFrame(graphics.NewImage("Miscelinous/Transparent"), bonusFrame);

            Assets.Bonuses[Bonus.Type.SHOVEL] = new Animation();
            Assets.Bonuses[Bonus.Type.SHOVEL].AddFrame(graphics.NewImage("Bonuses/Shovel"), bonusFrame);
            Assets.Bonuses[Bonus.Type.SHOVEL].AddFrame(graphics.NewImage("Miscelinous/Transparent"), bonusFrame);

            Assets.Bonuses[Bonus.Type.STAR] = new Animation();
            Assets.Bonuses[Bonus.Type.STAR].AddFrame(graphics.NewImage("Bonuses/Star"), bonusFrame);
            Assets.Bonuses[Bonus.Type.STAR].AddFrame(graphics.NewImage("Miscelinous/Transparent"), bonusFrame);

            Assets.Bonuses[Bonus.Type.TANK] = new Animation();
            Assets.Bonuses[Bonus.Type.TANK].AddFrame(graphics.NewImage("Bonuses/Tank"), bonusFrame);
            Assets.Bonuses[Bonus.Type.TANK].AddFrame(graphics.NewImage("Miscelinous/Transparent"), bonusFrame);

            Assets.Bonuses[Bonus.Type.TIMER] = new Animation();
            Assets.Bonuses[Bonus.Type.TIMER].AddFrame(graphics.NewImage("Bonuses/Timer"), bonusFrame);
            Assets.Bonuses[Bonus.Type.TIMER].AddFrame(graphics.NewImage("Miscelinous/Transparent"), bonusFrame);

            Assets.Enemy[Enemy.Type.EASY] = new Animation();
            Assets.Enemy[Enemy.Type.EASY].AddFrame(graphics.NewImage("AI/Easy/e1u"), 100);
            Assets.Enemy[Enemy.Type.EASY].AddFrame(graphics.NewImage("AI/Easy/e2u"), 100);

            Assets.Enemy[Enemy.Type.EASY + 1] = new Animation();
            Assets.Enemy[Enemy.Type.EASY + 1].AddFrame(graphics.NewImage("AI/Easy/e1d"), 100);
            Assets.Enemy[Enemy.Type.EASY + 1].AddFrame(graphics.NewImage("AI/Easy/e2d"), 100);

            Assets.Enemy[Enemy.Type.EASY + 2] = new Animation();
            Assets.Enemy[Enemy.Type.EASY + 2].AddFrame(graphics.NewImage("AI/Easy/e1l"), 100);
            Assets.Enemy[Enemy.Type.EASY + 2].AddFrame(graphics.NewImage("AI/Easy/e2l"), 100);

            Assets.Enemy[Enemy.Type.EASY + 3] = new Animation();
            Assets.Enemy[Enemy.Type.EASY + 3].AddFrame(graphics.NewImage("AI/Easy/e1r"), 100);
            Assets.Enemy[Enemy.Type.EASY + 3].AddFrame(graphics.NewImage("AI/Easy/e2r"), 100);

            Assets.Enemy[Enemy.Type.MEDIUM] = new Animation();
            Assets.Enemy[Enemy.Type.MEDIUM].AddFrame(graphics.NewImage("AI/Medium/e1u"), 100);
            Assets.Enemy[Enemy.Type.MEDIUM].AddFrame(graphics.NewImage("AI/Medium/e2u"), 100);

            Assets.Enemy[Enemy.Type.MEDIUM + 1] = new Animation();
            Assets.Enemy[Enemy.Type.MEDIUM + 1].AddFrame(graphics.NewImage("AI/Medium/e1d"), 100);
            Assets.Enemy[Enemy.Type.MEDIUM + 1].AddFrame(graphics.NewImage("AI/Medium/e2d"), 100);

            Assets.Enemy[Enemy.Type.MEDIUM + 2] = new Animation();
            Assets.Enemy[Enemy.Type.MEDIUM + 2].AddFrame(graphics.NewImage("AI/Medium/e1l"), 100);
            Assets.Enemy[Enemy.Type.MEDIUM + 2].AddFrame(graphics.NewImage("AI/Medium/e2l"), 100);

            Assets.Enemy[Enemy.Type.MEDIUM + 3] = new Animation();
            Assets.Enemy[Enemy.Type.MEDIUM + 3].AddFrame(graphics.NewImage("AI/Medium/e1r"), 100);
            Assets.Enemy[Enemy.Type.MEDIUM + 3].AddFrame(graphics.NewImage("AI/Medium/e2r"), 100);

            Assets.Enemy[Enemy.Type.HARD] = new Animation();
            Assets.Enemy[Enemy.Type.HARD].AddFrame(graphics.NewImage("AI/Hard/e1u"), 100);
            Assets.Enemy[Enemy.Type.HARD].AddFrame(graphics.NewImage("AI/Hard/e2u"), 100);

            Assets.Enemy[Enemy.Type.HARD + 1] = new Animation();
            Assets.Enemy[Enemy.Type.HARD + 1].AddFrame(graphics.NewImage("AI/Hard/e1d"), 100);
            Assets.Enemy[Enemy.Type.HARD + 1].AddFrame(graphics.NewImage("AI/Hard/e2d"), 100);

            Assets.Enemy[Enemy.Type.HARD + 2] = new Animation();
            Assets.Enemy[Enemy.Type.HARD + 2].AddFrame(graphics.NewImage("AI/Hard/e1l"), 100);
            Assets.Enemy[Enemy.Type.HARD + 2].AddFrame(graphics.NewImage("AI/Hard/e2l"), 100);

            Assets.Enemy[Enemy.Type.HARD + 3] = new Animation();
            Assets.Enemy[Enemy.Type.HARD + 3].AddFrame(graphics.NewImage("AI/Hard/e1r"), 100);
            Assets.Enemy[Enemy.Type.HARD + 3].AddFrame(graphics.NewImage("AI/Hard/e2r"), 100);

            if (!Highscores.IsCreated())
            {
                Highscores.CreateFile();
                Highscores.LoadHighscores();
            }
            else
            {
                Highscores.LoadHighscores();
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
            
            accumulatedTime += DeltaTime;
            percentage = (int)(accumulatedTime / 25);

            
            if (percentage > 100)
            {
                game.Screen = new MainMenuScreen(game);
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