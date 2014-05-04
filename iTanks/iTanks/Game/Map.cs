using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.Objects;
using GameFramework;
using Microsoft.Xna.Framework;
using System.IO;

namespace iTanks.Game
{
    public class Map
    {
        #region Fields
        private int level;
        private List<Actor> map;
        private List<Actor> shield;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje informacjê o pozycji 'orze³ka'.
        /// </summary>
        public Vector2 EaglePosition
        {
            get
            {
                Vector2 eagle = Vector2.Zero;

                for(int i = 0; i < map.Count; ++i)
                {
                    Actor actor = map.ElementAt(i);
                    if (actor is Eagle)
                    {
                        eagle = new Vector2(actor.Bounds.X, actor.Bounds.Y);
                        break;
                    }
                }

                return eagle;
            }
        }
        #endregion
        #region Constructors
        public Map(EditorScreen.Part[,] map)
        {
            this.level = 0;
            this.map = new List<Actor>();
            shield = new List<Actor>();

            for(int i = 0; i < 13; ++i)
            {
                for(int j = 0; j < 13; ++j)
                {
                    if ((j == 11 || j == 12) && (i == 5 || i == 6 || i == 7))
                    {
                        if (j == 12 && i == 6)
                        {
                            this.map.Add(new Eagle(i * 34 + 179, j * 34 + 19));
                        }
                        else
                        {
                            Interpret(map[i, j].Sign, i, j, true);
                        }
                    }
                    else
                    {
                        Interpret(map[i, j].Sign, i, j, false);
                    }
                }
            }
        }

        public Map(int level)
        {
            this.level = level;
            map = new List<Actor>();
            shield = new List<Actor>();

            String line = "";
            int j = 0;

            Stream fileStream = TitleContainer.OpenStream("Content/Levels/map" + level + ".lvl");
            StreamReader reader = new StreamReader(fileStream);

            while ((line = reader.ReadLine()) != null)
            {
                for (int i = 0; i < line.Length; ++i)
                {
                    if ((j == 11 || j == 12) && (i == 5 || i == 6 || i == 7))
                    {
                        if (j == 12 && i == 6)
                        {
                            map.Add(new Eagle(i * 34 + 179, j * 34 + 19));
                        }
                    }
                    else
                    {
                        char sign = line.ElementAt(i);
                        Interpret(sign, i, j, false);
                    }
                }
                ++j;
            }

            SetShield(Block.Type.BRICK);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda sprawdza kolizjê z obiektem przekazanym jako parametr.
        /// </summary>
        /// <param name="a">Obiekt, z którym kolizjê nale¿y zbadaæ.</param>
        public void CheckCollisions(Actor a)
        {
            for(int i = 0; i < map.Count; ++i)
            {
                Actor actor = map.ElementAt(i);
                actor.CheckCollision(a);
            }
        }

        /// <summary>
        /// Metoda sprawdza kolizjê tarczy z obiektem przekazanym jako parametr.
        /// </summary>
        /// <param name="a">Obiekt, z którym kolizjê nale¿y zbadaæ.</param>
        public void CheckShieldCollisions(Actor a)
        {
            for(int i = 0; i < shield.Count; ++i)
            {
                Actor actor = shield.ElementAt(i);
                actor.CheckCollision(a);
            }
        }

        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public void Update(float DeltaTime)
        {
            for(int i = 0; i < map.Count; ++i)
            {
                Actor actor = map.ElementAt(i);
                if (actor.ToRemove)
                {
                    map.Remove(actor);
                }
                else
                {
                    actor.Update(DeltaTime);
                }
            }

            for(int i = 0; i < shield.Count; ++i)
            {
                Actor actor = shield.ElementAt(i);
                if (actor.ToRemove)
                {
                    shield.Remove(actor);
                }
                else
                {
                    actor.Update(DeltaTime);
                }
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy 'Graphics' pozwalaj¹cy tworzyæ nowe obrazy, czcionki, rysowaæ na ekranie.</param>
        public void Draw(Graphics graphics)
        {
            for(int i = 0; i < map.Count; ++i)
            {
                Actor actor = map.ElementAt(i);
                if(!(actor is Tree))
                {
                    actor.Draw(graphics);
                }
            }

            for(int i = 0; i < shield.Count; ++i)
            {
                Actor actor = shield.ElementAt(i);
                actor.Draw(graphics);
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie drzew na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy 'Graphics' pozwalaj¹cy tworzyæ nowe obrazy, czcionki, rysowaæ na ekranie.</param>
        public void DrawTrees(Graphics graphics)
        {
            for(int i = 0; i < map.Count; ++i)
            {
                Actor actor = map.ElementAt(i);
                if(actor is Tree)
                {
                    actor.Draw(graphics);
                }
            }
        }

        /// <summary>
        /// Metoda ustawia rodzaj tarczy przy 'orze³ku' w oparciu o przekazany parametr.
        /// </summary>
        /// <param name="type">Rodzaj tarczy do ustawienia.</param>
        public void SetShield(int type)
        {
            shield.Clear();
            if (type == Block.Type.BRICK)
            {
                for (int i = 1; i < 2; ++i)
                {
                    for (int j = 1; j < 2; ++j)
                    {
                        shield.Add(new Brick(5 * 34 + 179 + i * 34 / 2, 11 * 34 + j * 34 / 2 + 19));
                    }
                }

                for (int i = 0; i < 2; ++i)
                {
                    for (int j = 1; j < 2; ++j)
                    {
                        shield.Add(new Brick(6 * 34 + 179 + i * 34 / 2, 11 * 34 + j * 34 / 2 + 19));
                    }
                }

                for (int i = 0; i < 1; ++i)
                {
                    for (int j = 1; j < 2; ++j)
                    {
                        shield.Add(new Brick(7 * 34 + 179 + i * 34 / 2, 11 * 34 + j * 34 / 2 + 19));
                    }
                }

                for (int i = 1; i < 2; ++i)
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        shield.Add(new Brick(5 * 34 + 179 + i * 34 / 2, 12 * 34 + j * 34 / 2 + 19));
                    }
                }

                for (int i = 0; i < 1; ++i)
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        shield.Add(new Brick(7 * 34 + 179 + i * 34 / 2, 12 * 34 + j * 34 / 2 + 19));
                    }
                }
            }
            if (type == Block.Type.STONE)
            {
                for (int i = 1; i < 2; ++i)
                {
                    for (int j = 1; j < 2; ++j)
                    {
                        shield.Add(new Stone(5 * 34 + 179 + i * 34 / 2, 11 * 34 + j * 34 / 2 + 19));
                    }
                }

                for (int i = 0; i < 2; ++i)
                {
                    for (int j = 1; j < 2; ++j)
                    {
                        shield.Add(new Stone(6 * 34 + 179 + i * 34 / 2, 11 * 34 + j * 34 / 2 + 19));
                    }
                }

                for (int i = 0; i < 1; ++i)
                {
                    for (int j = 1; j < 2; ++j)
                    {
                        shield.Add(new Stone(7 * 34 + 179 + i * 34 / 2, 11 * 34 + j * 34 / 2 + 19));
                    }
                }

                for (int i = 1; i < 2; ++i)
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        shield.Add(new Stone(5 * 34 + 179 + i * 34 / 2, 12 * 34 + j * 34 / 2 + 19));
                    }
                }

                for (int i = 0; i < 1; ++i)
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        shield.Add(new Stone(7 * 34 + 179 + i * 34 / 2, 12 * 34 + j * 34 / 2 + 19));
                    }
                }
            }
        }

        /// <summary>
        /// Metoda pomocnicza, interpretuj¹ca logiczn¹ reprezentacjê mapy.
        /// </summary>
        /// <param name="sign">Znak reprezentuj¹cy rodzaj pola.</param>
        /// <param name="x">Wspó³rzêdne obiektu na osi X.</param>
        /// <param name="y">Wspó³rzêdne obiektu na osi Y.</param>
        /// <param name="isShield">Informacja, czy interpretowane s¹ obiekty mapy czy tarczy.</param>
        public void Interpret(char sign, int x, int y, Boolean isShield)
        {
            switch(sign)
            {
                #region Road
                case 'A':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Road(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Road(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                #endregion
                #region Tree
                case 'B':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Tree(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Tree(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                #endregion
                #region Water
                case 'C':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Water(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Water(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                #endregion
                #region Stone
                case 'D':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'E':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 0; j < 1; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'F':
                    for (int i = 0; i < 1; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'G':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 1; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'H':
                    for (int i = 1; i < 2; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'I':
                    for (int i = 1; i < 2; ++i)
                    {
                        for (int j = 0; j < 1; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'J':
                    for (int i = 0; i < 1; ++i)
                    {
                        for (int j = 0; j < 1; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'K':
                    for (int i = 1; i < 2; ++i)
                    {
                        for (int j = 1; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'L':
                    for (int i = 0; i < 1; ++i)
                    {
                        for (int j = 1; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Stone(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                #endregion
                #region Brick
                case 'M':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'N':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 0; j < 1; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'O':
                    for (int i = 0; i < 1; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'P':
                    for (int i = 0; i < 2; ++i)
                    {
                        for (int j = 1; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'Q':
                    for (int i = 1; i < 2; ++i)
                    {
                        for (int j = 0; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'R':
                    for (int i = 1; i < 2; ++i)
                    {
                        for (int j = 0; j < 1; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'S':
                    for (int i = 0; i < 1; ++i)
                    {
                        for (int j = 0; j < 1; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'T':
                    for (int i = 1; i < 2; ++i)
                    {
                        for (int j = 1; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                case 'U':
                    for (int i = 0; i < 1; ++i)
                    {
                        for (int j = 1; j < 2; ++j)
                        {
                            if(isShield)
                                shield.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                            else
                                map.Add(new Brick(x * 34 + 179 + i * 34 / 2, y * 34 + j * 34 / 2 + 19));
                        }
                    }
                    break;
                #endregion
            }
        }
        #endregion
    }
}