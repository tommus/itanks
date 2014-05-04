using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using GameFramework.Implementation;
using iTanks.Game;
using GameFramework;

namespace iTanks
{
    /// <summary>
    /// Klasa Main dziedziczy z klasy WPGame, której klas¹ bazow¹ jest klasa Microsoft.Xna.Framework.Game.
    /// </summary>
    public class Main : WPGame
    {
        #region Methods
        /// <summary>
        /// Pocz¹tkowy punkt gry.
        /// Nale¿y zaimplementowaæ metodê zwracaj¹c¹ obiekt klasy dziedzicz¹cej z klasy abstrakcyjnej 'Screen'.
        /// </summary>
        /// <returns>Obiekt klasy screen reprezentuj¹cy wydzielon¹ logicznie sekcjê gry / menu / etc.</returns>
        public override Screen GetStartScreen()
        {
            return new SplashScreen(this);
        }

        /// <summary>
        /// Metoda pozwalaj¹ca na konfiguracjê parametrów urz¹dzenia.
        /// </summary>
        /// <param name="device">Obiekt klasy GraphicsDeviceManager, na którym konfiguracji nale¿y dokonaæ.</param>
        public override void SetupDevice(GraphicsDeviceManager device)
        {
            device.PreferredBackBufferHeight = 480;
            device.PreferredBackBufferWidth = 800;
            device.ToggleFullScreen();
            device.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }
        #endregion
    }
}
