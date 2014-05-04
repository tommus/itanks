using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameFramework.Implementation
{
    public class WPGraphics : Graphics
    {
        #region Fields
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ContentManager content;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje szerokoœæ ekranu urz¹dzenia.
        /// </summary>
        public int Width
        {
            get { return spriteBatch.GraphicsDevice.Viewport.Width; }
        }

        /// <summary>
        /// Parametr przechowuje po³owê szerokoœci ekranu urz¹dzenia.
        /// </summary>
        public int HalfWidth
        {
            get { return Width / 2; }
        }

        /// <summary>
        /// Parametr przechowuje wysokoœæ ekranu urz¹dzenia.
        /// </summary>
        public int Height
        {
            get { return spriteBatch.GraphicsDevice.Viewport.Height; }
        }

        /// <summary>
        /// Parametr przechowuje po³owê wysokoœci ekranu urz¹dzenia.
        /// </summary>
        public int HalfHeight
        {
            get { return Height / 2; }
        }
        #endregion
        #region Constructors
        public WPGraphics(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, ContentManager content)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            this.content = content;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda czyœci ekran wypelniaj¹c go kolorem.
        /// </summary>
        /// <param name="color">Kolor, jakim zamalowany zostanie ekran.</param>
        public void Clear(Color color)
        {
            spriteBatch.GraphicsDevice.Clear(color);
        }

        /// <summary>
        /// Metoda zwalnia obiekt typu SpriteBatch.
        /// </summary>
        public void Dispose()
        {
            spriteBatch.Dispose();
        }

        /// <summary>
        /// Metoda tworzy obiekt typu Image z pliku.
        /// </summary>
        /// <param name="filename">Œcie¿ka do pliku w katalogu 'Contents'.</param>
        /// <returns>Nowy obraz.</returns>
        public Image NewImage(String filename)
        {
            Texture2D image = content.Load<Texture2D>(filename);
            return new WPImage(image);
        }

        /// <summary>
        /// Metoda wczytuje czcionkê z pliku.
        /// </summary>
        /// <param name="filename">Œcie¿ka do pliku w katalogu 'Contents'.</param>
        /// <returns>Nowy obiekt typu 'SpriteFont'.</returns>
        public SpriteFont NewFont(String filename)
        {
            return content.Load<SpriteFont>(filename);
        }

        /// <summary>
        /// Metoda rysuje obraz w pocz¹tku uk³adu wspó³rzêdnych.
        /// </summary>
        /// <param name="image">Obraz do narysowania.</param>
        public void DrawImage(Image image)
        {
            Texture2D texture = image.Texture;
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }

        /// <summary>
        /// Metoda rysuje obraz w okreœlonym punkcie i o okreœlonych parametrach.
        /// </summary>
        /// <param name="image">Obraz do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="color">Przezroczystoœæ obrazu.</param>
        public void DrawImage(Image image, int x, int y, Color color)
        {
            Texture2D texture = image.Texture;
            spriteBatch.Draw(texture, new Vector2(x, y), color);
        }

        /// <summary>
        /// Metoda rysuje obraz w okreœlonym punkcie.
        /// </summary>
        /// <param name="image">Obraz do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        public void DrawImage(Image image, int x, int y)
        {
            Texture2D texture = image.Texture;
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White);
        }

        /// <summary>
        /// Metoda rysuje obraz w okreœlonym punkcie i przezroczystoœci.
        /// </summary>
        /// <param name="image">Obraz do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="width">Szerokoœæ obrazu.</param>
        /// <param name="height">Wysokoœæ obrazu.</param>
        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            Texture2D texture = image.Texture;
            spriteBatch.Draw(texture, new Vector2(x, y), new Rectangle(0, 0, width, height), Color.White);
        }

        /// <summary>
        /// Metoda rysuje obraz w okreœlonym punkcie i o okreœlonej skali.
        /// </summary>
        /// <param name="image">obraz do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="scaledWidth">Rozmiar docelowy szerokoœci rysowanego obrazu</param>
        /// <param name="scaledHeight">Rozmiar docelowy wysokoœci rysowanego obrazu</param>
        public void DrawScaledImage(Image image, int x, int y, int scaledWidth, int scaledHeight)
        {
            Texture2D texture = image.Texture;
            spriteBatch.Draw(texture, new Rectangle(x, y, scaledWidth, scaledHeight), null, Color.White);
        }

        /// <summary>
        /// Metoda rysuje tekst w okreœlonym punkcie ekranu i o okreœlonej przezroczystoœci.
        /// </summary>
        /// <param name="font">U¿yta czcionka.</param>
        /// <param name="text">Tekst do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="color">Przezroczystoœæ obrazu.</param>
        public void DrawString(SpriteFont font, String text, int x, int y, Color color)
        {
            spriteBatch.DrawString(font, text, new Vector2(x, y), color);
        }

        /// <summary>
        /// Metoda rysuje tekst w okreœlonym punkcie ekranu i o okreœlonej przezroczystoœci.
        /// </summary>
        /// <param name="font">U¿yta czcionka.</param>
        /// <param name="text">Tekst do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="scale">Skala czcionki.</param>
        /// <param name="color">Przezroczystoœæ obrazu.</param>
        public void DrawString(SpriteFont font, String text, int x, int y, float scale, Color color)
        {
            spriteBatch.DrawString(font, text, new Vector2(x, y), color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
        #endregion
    }
}