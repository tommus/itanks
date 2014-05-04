using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFramework
{
    public interface Music
    {
        #region Properties
        /// <summary>
        /// Parametr zwraca 'true', je¿eli w danej chwili czasu odtwarzany jest w³aœnie jakiœ plik muzyczny.
        ///  W przeciwnym razie zwraca 'false'.
        /// </summary>
        Boolean IsPlaying { get; }

        /// <summary>
        /// Parametr zwraca 'true', je¿eli w danej chwili czasu nie odtwarzany jest w³aœnie jakiœ plik muzyczny.
        ///  W przeciwnym razie zwraca 'false'.
        /// </summary>
        Boolean IsStopped { get; }

        /// <summary>
        /// Parametr zwraca 'true', je¿eli w danej chwili czasu odtwarzany jest w pêtli jakiœ plik muzyczny.
        ///  W przeciwnym razie zwraca 'false'.
        /// </summary>
        Boolean IsLooping { get; }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda pozwala na rozpoczêcie odtwarzania pliku muzycznego.
        /// Tylko jeden plik mo¿e byæ odtwarzany jednoczeœnie.
        /// </summary>
        void Play();

        /// <summary>
        /// Metoda pauzuje odtwarzanie aktualnego pliku, je¿eli jakiœ jest odtwarzany.
        /// </summary>
        void Pause();

        /// <summary>
        /// Metoda zatrzymuje odtwarzanie pliku muzycznego.
        /// </summary>
        void Stop();

        /// <summary>
        /// Metoda ustawia odtwarzanie pliku w pêtli.
        /// </summary>
        /// <param name="looping">'true' - odtwarzaj w pêtli, 'false' - nie odtwarzaj w pêtli.</param>
        void SetLooping(Boolean looping);

        /// <summary>
        /// Ustawia g³oœnoœæ odtwarzania pliku.
        /// </summary>
        /// <param name="volume">Wartoœæ g³oœnoœci w zakresie 0.0f - 1.0f</param>
        void SetVolume(float volume);

        /// <summary>
        /// Metoda zwalnia przydzielone zasoby.
        /// </summary>
        void Dispose();
        #endregion
    }
}
