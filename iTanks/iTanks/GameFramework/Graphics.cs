using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFramework
{
    public interface Graphics
    {
        #region Fields
        /// <summary>
        /// Parametr przechowuje szerokoœæ ekranu urz¹dzenia.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Parametr przechowuje po³owê szerokoœci ekranu urz¹dzenia.
        /// </summary>
        int HalfWidth { get; }

        /// <summary>
        /// Parametr przechowuje wysokoœæ ekranu urz¹dzenia.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Parametr przechowuje po³owê wysokoœci ekranu urz¹dzenia.
        /// </summary>
        int HalfHeight { get; }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda tworzy obiekt typu Image z pliku.
        /// </summary>
        /// <param name="filename">Œcie¿ka do pliku w katalogu 'Contents'.</param>
        /// <returns>Nowy obraz.</returns>
        Image NewImage(String filename);

        /// <summary>
        /// Metoda wczytuje czcionkê z pliku.
        /// </summary>
        /// <param name="filename">Œcie¿ka do pliku w katalogu 'Contents'.</param>
        /// <returns>Nowy obiekt typu 'SpriteFont'.</returns>
        SpriteFont NewFont(String filename);

        /// <summary>
        /// Metoda rysuje obraz w pocz¹tku uk³adu wspó³rzêdnych.
        /// </summary>
        /// <param name="image">Obraz do narysowania.</param>
        void DrawImage(Image image);

        /// <summary>
        /// Metoda rysuje obraz w okreœlonym punkcie.
        /// </summary>
        /// <param name="image">Obraz do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        void DrawImage(Image image, int x, int y);

        /// <summary>
        /// Metoda rysuje obraz w okreœlonym punkcie i przezroczystoœci.
        /// </summary>
        /// <param name="image">Obraz do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="width">Szerokoœæ obrazu.</param>
        /// <param name="height">Wysokoœæ obrazu.</param>
        void DrawImage(Image image, int x, int y, int width, int height);

        /// <summary>
        /// Metoda rysuje obraz w okreœlonym punkcie i o okreœlonych parametrach.
        /// </summary>
        /// <param name="image">Obraz do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="color">Przezroczystoœæ obrazu.</param>
        void DrawImage(Image image, int x, int y, Color color);

        /// <summary>
        /// Metoda rysuje obraz w okreœlonym punkcie i o okreœlonej skali.
        /// </summary>
        /// <param name="image">obraz do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="scaledWidth">Rozmiar docelowy szerokoœci rysowanego obrazu</param>
        /// <param name="scaledHeight">Rozmiar docelowy wysokoœci rysowanego obrazu</param>
        void DrawScaledImage(Image image, int x, int y, int scaledWidth, int scaledHeight);

        /// <summary>
        /// Metoda rysuje tekst w okreœlonym punkcie ekranu i o okreœlonej przezroczystoœci.
        /// </summary>
        /// <param name="font">U¿yta czcionka.</param>
        /// <param name="text">Tekst do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="color">Przezroczystoœæ obrazu.</param>
        void DrawString(SpriteFont font, String text, int x, int y, Color color);

        /// <summary>
        /// Metoda rysuje tekst w okreœlonym punkcie ekranu i o okreœlonej przezroczystoœci.
        /// </summary>
        /// <param name="font">U¿yta czcionka.</param>
        /// <param name="text">Tekst do narysowania.</param>
        /// <param name="x">Wspó³rzêdna osi X.</param>
        /// <param name="y">Wspó³rzêdna osi Y.</param>
        /// <param name="scale">Skala czcionki.</param>
        /// <param name="color">Przezroczystoœæ obrazu.</param>
        void DrawString(SpriteFont font, String text, int x, int y, float scale, Color color);

        /// <summary>
        /// Metoda czyœci ekran wypelniaj¹c go kolorem.
        /// </summary>
        /// <param name="color">Kolor, jakim zamalowany zostanie ekran.</param>
        void Clear(Color color);

        /// <summary>
        /// Metoda zwalnia obiekt typu SpriteBatch.
        /// </summary>
        void Dispose();
        #endregion
    }
}