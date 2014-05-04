using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace iTanks.Game
{
    public class Button
    {
        #region Fields
        private Image background;
        private Image foreground;
        private Rectangle bounds;
        private SpriteFont font;

        private int radius;
        private int posX;
        private int posY;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje t³o przycisku.
        /// </summary>
        public Image Image
        {
            get 
            {
                if (background != null)
                    return background;
                else
                    return null;
            }
        }

        /// <summary>
        /// Parametr przechowuje informacjê o po³o¿eniu i wymiarach przycisku.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }
        #endregion
        #region Constructors
        public Button(int posX, int posY, int radius)
        {
            this.radius = radius;
            this.posX = posX;
            this.posY = posY;
        }

        public Button(SpriteFont font, int posX, int posY, int width, int height)
        {
            this.font = font;
            this.bounds = new Rectangle(posX, posY, width, height);
            radius = -1;
        }

        public Button(Image background, Image foreground, int posX, int posY)
        {
            this.background = background;
            this.foreground = foreground;
            if (background != null)
                this.bounds = new Rectangle(posX, posY, background.Width, background.Height);
            else
                this.bounds = new Rectangle(posX, posY, 0, 0);
            radius = -1;
        }
        #endregion
        #region Methods
        public void SetSize(int width, int height)
        {
            this.bounds.Width = width;
            this.bounds.Height = height;
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy Graphics umo¿liwiaj¹cy tworzenie obrazów, czcionek, rysowanie na ekranie.</param>
        public void Draw(Graphics graphics)
        {
            if(background != null)
                graphics.DrawImage(background, bounds.X, bounds.Y);
            if(foreground != null)
            {
                int posX = bounds.X + ((background.Width - foreground.Width) / 2);
                int posY = bounds.Y + (background.Height - foreground.Height);
                graphics.DrawImage(foreground, posX, posY);
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy Graphics umo¿liwiaj¹cy tworzenie obrazów, czcionek, rysowanie na ekranie.</param>
        /// <param name="text">Etykieta przycisku.</param>
        /// <param name="color">Przezroczystoœæ i kolor przycisku.</param>
        public void Draw(Graphics graphics, String text, Color color)
        {
            graphics.DrawString(font, text, bounds.X, bounds.Y, color);
        }

        /// <summary>
        /// Metoda sprawdzaj¹ca, czy punkt o danych wspó³rzêdnych zawiera siê w ramach przycisku.
        /// </summary>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <returns></returns>
        public Boolean Intersects(int x, int y)
        {
            double d = Math.Pow(posX - x, 2) + Math.Pow(posY - y, 2);

            if(radius == -1)
                return bounds.Intersects(new Rectangle(x, y, 1, 1));
            else
                return d <= Math.Pow(radius, 2);
        }
        #endregion
    }
}