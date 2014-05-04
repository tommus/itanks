using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework
{
    public interface Image
    {
        #region Properties
        /// <summary>
        /// Parametr przechowuje informacjê o szerokoœci obrazu.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Parametr przechowuje informacjê o wysokoœci obrazu.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Parametr przechowuje teksturê obrazu.
        /// </summary>
        Texture2D Texture { get; }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda zwalnia przydzielone zasoby.
        /// </summary>
        void Dispose();
        #endregion
    }
}