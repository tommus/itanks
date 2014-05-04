using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;

namespace iTanks.Game.Objects
{
    public class Animation
    {
        #region Fields
        private List<SingleFrame> frames;
        private int currentFrame;
        private double animationTime;
        private double totalDuration;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje teksturê aktualnej ramki animacji.
        /// </summary>
        public Image Image
        {
            get
            {
                lock (typeof(Animation))
                {
                    if (frames.Count == 0)
                        return null;
                    else
                        return GetFrame(currentFrame).texture;
                }
            }
        }
        #endregion
        #region Constructors
        public Animation()
        {
            frames = new List<SingleFrame>();
            totalDuration = 0;
            Start();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda dodaje ramkê animacji.
        /// </summary>
        /// <param name="texture">Tekstura ramki.</param>
        /// <param name="duration">D³ugoœæ trwania ramki.</param>
        public void AddFrame(Image texture, double duration)
        {
            lock(typeof(Animation))
            {
                totalDuration += duration;
                frames.Add(new SingleFrame(texture, totalDuration));
            }
        }

        /// <summary>
        /// Metoda uruchamia animacjê od pocz¹tku.
        /// </summary>
        public void Start()
        {
            lock(typeof(Animation))
            {
                animationTime = 0;
                currentFrame = 0;
            }
        }

        /// <summary>
        /// Metoda aktualizuj¹ca animacjê.
        /// </summary>
        /// <param name="DeltaTime">Informacja o up³ywaj¹cym czasie.</param>
        public void Update(float DeltaTime)
        {
            lock(typeof(Animation))
            {
                if(frames.Count > 1)
                {
                    animationTime += DeltaTime;

                    if(animationTime >= totalDuration)
                    {
                        animationTime %= totalDuration;
                        currentFrame = 0;
                    }

                    while(animationTime > GetFrame(currentFrame).endTime)
                    {
                        ++currentFrame;
                    }
                }
            }
        }

        /// <summary>
        ///  Metoda zwalniaj¹ca przydzielone zasoby.
        /// </summary>
        public void Dispose()
        {
            foreach (SingleFrame frame in frames)
            {
                frame.texture.Dispose();
            }
        }
        #endregion
        #region Internal Classes
        /// <summary>
        ///  Metoda zwraca ramkê.
        /// </summary>
        /// <param name="i">Numer ramki do zwrócenia.</param>
        /// <returns></returns>
        private SingleFrame GetFrame(int i)
        {
            return (SingleFrame)frames.ElementAt(i);
        }

        /// <summary>
        /// Klasa przechowuje informacje o pojedynczej ramce animacji.
        /// </summary>
        private class SingleFrame
        {
            public Image texture;
            public double endTime;

            public SingleFrame(Image texture, double endTime)
            {
                this.texture = texture;
                this.endTime = endTime;
            }
        }
        #endregion
    }
}