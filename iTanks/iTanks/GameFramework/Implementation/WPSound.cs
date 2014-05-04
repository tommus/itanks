using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameFramework.Implementation
{
    public class WPSound : Sound
    {
        #region Fields
        private SoundEffect effect;
        #endregion
        #region Constructors
        public WPSound(SoundEffect effect)
        {
            this.effect = effect;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda pozwala na odtworzenie pliku dŸwiêkowego z okreœlon¹ g³oœnoœci¹.
        /// </summary>
        /// <param name="volume">G³oœnoœæ wyra¿ona w zakresie 0.0f - 1.0f.</param>
        public void Play(float volume)
        {
            effect.Play(volume, 0.0f, 0.0f);
        }

        /// <summary>
        /// Metoda zwalnia przydzielone zasoby.
        /// </summary>
        public void Dispose()
        {
            effect.Dispose();
        }
        #endregion
    }
}