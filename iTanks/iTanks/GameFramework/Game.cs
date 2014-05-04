using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameFramework
{
    public interface Game
    {
        #region Properties
        /// <summary>
        /// Parametr zwracaj¹cy instacjê implementacji interfejsu Audio.
        /// W frameworku odpowiedzialny za ³adowanie muzyki, dŸwiêku i ich¿e odtwarzanie.
        /// </summary>
        Audio Audio { get; }

        /// <summary>
        /// Parametr zwracaj¹cy instancjê implementacji interfejsu Graphics.
        /// W frameworku odpowiedzialny za ³adowanie obrazów i animacji oraz wyœwietlaniu ich na ekranie.
        /// </summary>
        Graphics Graphics { get; }

        /// <summary>
        /// Parametr 'Screen' - reprezentant klasy abstrakcyjnej odpowiedzialnej za wykonywanie podstawowych
        /// metod dzia³ania gry - aktualizacji, rysowania, zwalniania zasobów i obs³ugi przycisku wstecz.
        /// </summary>
        Screen Screen { get; set; }
        #endregion
        #region Abstracts to implement in derrived classes
        /// <summary>
        /// Punkt zaczepny gry.
        /// W celu okreœlenia ekranu pocz¹tkowego nale¿y zaimplementowaæ tê w³¹œnie metodê.
        /// </summary>
        /// <returns></returns>
        Screen GetStartScreen();

        /// <summary>
        /// Metoda pozwala na konfiguracjê parametrów urz¹dzenia takich jak preferowana szerokoœæ i wysokoœæ ekranu itp.
        /// </summary>
        /// <param name="device">Urz¹dzenie, którego kalibracji nale¿y dokonaæ.</param>
        void SetupDevice(GraphicsDeviceManager device);
        #endregion
    }
}