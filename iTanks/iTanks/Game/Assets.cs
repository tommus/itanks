using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework.Graphics;
using iTanks.Game.Objects;

namespace iTanks.Game
{
    /// <summary>
    /// Punkt przechowuj¹cy wszystkie obiekty potrzebne w grze.
    /// Zgodnie z wzorcami projektowymi nale¿a³oby za-enkapsu³owaæ obiekty
    /// w klasie singletonowej, jednak w tym jednym wypadku zrobiê wyj¹tek pozostawiaj¹c
    /// elementy tej klasy polami publicznymi i statycznymi.
    /// Klasy singletonowe u¿yte w grze to np.: Player, Level - wystêpuj¹ce jedynie w
    /// jedynych, niepowtarzalnych przypadkach.
    /// </summary>
    public static class Assets
    {
        // Defaults
        public static Image BlackBox;
        public static SpriteFont BrickFont;

        // SplashScreen
        public static Image Splash;

        // GameLoadingScreen, LevelLoadingScreen
        public static Image LoadingBorder;
        public static Image LoadingText;
        public static Image LoadingBar;

        // MainGameMenu
        public static Image BrickBackground;
        public static Image ButtonBackground;
        public static Image NewGameText;
        public static Image TutorialText;
        public static Image HighscoresText;
        public static Image EditorText;
        public static Image ExitText;

        // TutorialsScreen
        public static Image ArrowRight;
        public static Image ArrowLeft;
        public static Image TutorialBorder;
        public static Image GoalsText;
        public static Image PlayerText;
        public static Image EnemiesText;
        public static Image BonusesText;

        // HighscoresScreen
        public static Image ResetText;

        // LevelSelectionScreen
        public static Image LevelText;

        // MainGameScreen
        public static Image GameBoard;
        public static Music GameStart;
        public static Music GameOver;
        public static Sound Move;
        public static Sound Fire;
        public static Sound Steel;
        public static Sound BrickSound;
        public static Image Ship;
        public static Animation Shield;
        public static Sound BonusSound;

        public static Sound ExplosionSound;

        // LevelSummaryScreen
        public static Image SummaryScreenImage;
        public static Sound ScoreSound;

        // Textures
        public static Image[] Blocks = new Image[11];
        public static Animation[] Animations = new Animation[5];
        public static Animation[] Player = new Animation[8];
        public static Animation[] Bonuses = new Animation[7];
        public static Animation[] Enemy = new Animation[16];

        // EditorScreen
        public static Image EditorBoard;

        // Explosion
        public static Animation Explosion;

        // Spawn
        public static Animation Spawn;

        /// <summary>
        /// Metoda zwalnia wszystkie przydzielone powy¿ej zasoby.
        /// </summary>
        public static void DisposeAll()
        {
            EditorBoard.Dispose();
            Spawn.Dispose();
            Explosion.Dispose();
            LoadingBar.Dispose();
            LoadingText.Dispose();
            LoadingBar.Dispose();
            BrickBackground.Dispose();
            ButtonBackground.Dispose();
            NewGameText.Dispose();
            TutorialText.Dispose();
            HighscoresText.Dispose();
            EditorText.Dispose();
            ExitText.Dispose();
            ArrowRight.Dispose();
            ArrowLeft.Dispose();
            BlackBox.Dispose();
            TutorialBorder.Dispose();
            GoalsText.Dispose();
            PlayerText.Dispose();
            EnemiesText.Dispose();
            BonusesText.Dispose();
            ResetText.Dispose();
            LevelText.Dispose();
            GameBoard.Dispose();
            GameStart.Dispose();
            GameOver.Dispose();
            Move.Dispose();
            Fire.Dispose();
            Steel.Dispose();
            BrickSound.Dispose();
            Shield.Dispose();
            Ship.Dispose();
            BonusSound.Dispose();
            SummaryScreenImage.Dispose();
            ScoreSound.Dispose();

            for (int i = 0; i < Blocks.Length; ++i)
            {
                if (Blocks[i] != null)
                    Blocks[i].Dispose();
            }

            for(int i = 0; i < Animations.Length; ++i)
            {
                if (Animations[i] != null)
                    Animations[i].Dispose();
            }

            for(int i = 0; i < Player.Length; ++i)
            {
                if (Player[i] != null)
                    Player[i].Dispose();
            }

            for(int i = 0; i < Bonuses.Length; ++i)
            {
                if (Bonuses[i] != null)
                    Bonuses[i].Dispose();
            }

            for(int i = 0; i < Enemy.Length; ++i)
            {
                if (Enemy[i] != null)
                    Enemy[i].Dispose();
            }
        }
    }
}
