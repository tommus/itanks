using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTanks.Game
{
    /// <summary>
    /// Klasa singletonowa.
    /// </summary>
    public class Random
    {
        #region Fields
        private static Random instance;
        private System.Random random;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr zwraca jedyn¹ instancjê klasy.
        /// </summary>
        public static Random Instance
        {
            get
            {
                if (instance == null)
                    instance = new Random();

                return instance;
            }
        }

        /// <summary>
        /// Parametr zwraca globalny obiekt klasy 'Random'.
        /// </summary>
        public System.Random GlobalRandom
        {
            get
            {
                return random;
            }
        }
        #endregion
        #region Constructors
        private Random()
        {
            random = new System.Random();
        }
        #endregion
    }
}