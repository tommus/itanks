using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace GameFramework.Implementation
{
    public class WPMusic : Music
    {
        #region Fields
        private Song song;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr zwraca 'true', je¿eli w danej chwili czasu odtwarzany jest w³aœnie jakiœ plik muzyczny.
        ///  W przeciwnym razie zwraca 'false'.
        /// </summary>
        public Boolean IsPlaying
        {
            get
            {
                return (MediaPlayer.State == MediaState.Playing);
            }
        }

        /// <summary>
        /// Parametr zwraca 'true', je¿eli w danej chwili czasu nie odtwarzany jest w³aœnie jakiœ plik muzyczny.
        ///  W przeciwnym razie zwraca 'false'.
        /// </summary>
        public Boolean IsStopped
        {
            get
            {
                return (MediaPlayer.State == MediaState.Stopped);
            }
        }

        /// <summary>
        /// Parametr zwraca 'true', je¿eli w danej chwili czasu odtwarzany jest w pêtli jakiœ plik muzyczny.
        ///  W przeciwnym razie zwraca 'false'.
        /// </summary>
        public bool IsLooping
        {
            get
            {
                return MediaPlayer.IsRepeating;
            }
        }
        #endregion
        #region Constructors
        public WPMusic(Song song)
        {
            this.song = song;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda pozwala na rozpoczêcie odtwarzania pliku muzycznego.
        /// Tylko jeden plik mo¿e byæ odtwarzany jednoczeœnie.
        /// </summary>
        public void Play()
        {
            if (MediaPlayer.State == MediaState.Playing)
                return;

            MediaPlayer.Play(song);
        }

        /// <summary>
        /// Metoda pauzuje odtwarzanie aktualnego pliku, je¿eli jakiœ jest odtwarzany.
        /// </summary>
        public void Pause()
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Pause();
        }

        /// <summary>
        /// Metoda zatrzymuje odtwarzanie pliku muzycznego.
        /// </summary>
        public void Stop()
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();
        }

        /// <summary>
        /// Metoda ustawia odtwarzanie pliku w pêtli.
        /// </summary>
        /// <param name="looping">'true' - odtwarzaj w pêtli, 'false' - nie odtwarzaj w pêtli.</param>
        public void SetLooping(bool looping)
        {
            MediaPlayer.IsRepeating = looping;
        }

        /// <summary>
        /// Ustawia g³oœnoœæ odtwarzania pliku.
        /// </summary>
        /// <param name="volume">Wartoœæ g³oœnoœci w zakresie 0.0f - 1.0f</param>
        public void SetVolume(float volume)
        {
            MediaPlayer.Volume = volume;
        }

        /// <summary>
        /// Metoda zwalnia przydzielone zasoby.
        /// </summary>
        public void Dispose()
        {
            song.Dispose();
        }
        #endregion
    }
}