using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace GameFramework.Implementation
{
    public class WPAudio : Audio
    {
        #region Fields
        private ContentManager content;
        #endregion
        #region Constructors
        public WPAudio(ContentManager content)
        {
            this.content = content;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda tworzy instancjê klasy WPMusic, pozwalaj¹c na odtwarzanie pliku.
        /// </summary>
        /// <param name="filename">Œcie¿ka do pliku muzycznego w katalogu 'Contents'</param>
        /// <returns></returns>
        public Music NewMusic(string filename)
        {
            Song song = content.Load<Song>(filename);
            return new WPMusic(song);
        }

        /// <summary>
        /// Metoda tworzy instancjê klasy WPSound, pozwalaj¹c na odtwarzanie pliku.
        /// </summary>
        /// <param name="filename">Œcie¿ka do pliku muzycznego w katalofu 'Contents'.</param>
        /// <returns></returns>
        public Sound NewSound(string filename)
        {
            SoundEffect effect = content.Load<SoundEffect>(filename);
            return new WPSound(effect);
        }
        #endregion
    }
}
