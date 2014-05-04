using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFramework
{
    public interface Audio
    {
        /// <summary>
        /// Metoda tworzy instancjê klasy WPMusic, pozwalaj¹c na odtwarzanie pliku.
        /// </summary>
        /// <param name="filename">Œcie¿ka do pliku muzycznego w katalogu 'Contents'</param>
        /// <returns></returns>
        Music NewMusic(String filename);

        /// <summary>
        /// Metoda tworzy instancjê klasy WPSound, pozwalaj¹c na odtwarzanie pliku.
        /// </summary>
        /// <param name="filename">Œcie¿ka do pliku muzycznego w katalofu 'Contents'.</param>
        /// <returns></returns>
        Sound NewSound(String filename);
    }
}
