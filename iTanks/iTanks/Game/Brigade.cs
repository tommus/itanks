using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.AI;
using System.IO;
using Microsoft.Xna.Framework;
using iTanks.Game.Objects;
using GameFramework;

namespace iTanks.Game
{
    public class Brigade
    {
        #region Fields
        private List<Enemy> garage;
        private List<Enemy> enemies;
        private int enemiesCount;
        private Boolean freeze;
        private int freezeTime;
        private int freezeTime2;
        private Sound ExplosionSound;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje liczbê przeciwników o ³atwym poziomie trudnoœci.
        /// </summary>
        public int Easy { get; set; }

        /// <summary>
        /// Parametr przechowuje liczbê przeciwników o œrednim poziomie trudnoœci.
        /// </summary>
        public int Medium { get; set; }

        /// <summary>
        /// Parametr przechowuje liczbê przeciwników o trudnym poziomie trudnoœci.
        /// </summary>
        public int Hard { get; set; }

        /// <summary>
        /// Parametr przechowuje sumaryczn¹ liczbê przeciwników.
        /// </summary>
        public int Enemies
        {
            get { return enemiesCount; }
        }

        /// <summary>
        /// Parametr przechowuje listê przeciwników aktualnie nie oczekuj¹cych
        /// w gara¿u.
        /// </summary>
        public List<Enemy> Fleet
        {
            get { return enemies; }
        }
        #endregion
        #region Constructors
        public Brigade()
        {
            ExplosionSound = Assets.ExplosionSound;
            freezeTime = 0;
            freezeTime2 = Timer.FREEZE_TIME;
            freeze = false;
            enemiesCount = 20;
            garage = new List<Enemy>();
            enemies = new List<Enemy>();

            for (int i = 0; i < 20; ++i)
            {
                switch (iTanks.Game.Random.Instance.GlobalRandom.Next(0, 3))
                {
                    case 0:
                        garage.Add(new Easy());
                        ++Easy;
                        break;
                    case 1:
                        ++Medium;
                        garage.Add(new Medium());
                        break;
                    case 2:
                        ++Hard;
                        garage.Add(new Hard());
                        break;
                }
            }
        }

        public Brigade(int level)
        {
            ExplosionSound = Assets.ExplosionSound;
            freezeTime = 0;
            freezeTime2 = Timer.FREEZE_TIME;
            freeze = false;
            enemiesCount = 20;
            garage = new List<Enemy>();
            enemies = new List<Enemy>();

            String line = "";
            int j = 0;

            Stream filestream = TitleContainer.OpenStream("Content/Levels/map" + level + ".lvl");
            StreamReader reader = new StreamReader(filestream);

            while ((line = reader.ReadLine()) != null)
            {
                if (j != 14)
                    ++j;
                else
                {
                    for (int i = 0; i < line.Length; ++i)
                    {
                        char sign = line.ElementAt(i);

                        switch (sign)
                        {
                            case 'E':
                                garage.Add(new Easy());
                                ++Easy;
                                break;
                            case 'M':
                                ++Medium;
                                garage.Add(new Medium());
                                break;
                            case 'H':
                                ++Hard;
                                garage.Add(new Hard());
                                break;
                        }
                    }
                }
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public void Update(float DeltaTime)
        {
            if (garage.Count == 0)
                if (enemies.Count == 0)
                    Level.Instance.Cleared = true;

            for(int i = 0; i < enemies.Count; ++i)
            {
                Enemy enemy = enemies.ElementAt(i);
                if (enemy.ToRemove)
                {
                    if(enemy is Easy)
                    {
                        --Easy;
                    }
                    if (enemy is Medium)
                    {
                        --Medium;
                    }
                    if (enemy is Hard)
                    {
                        --Hard;
                    }
                   
                    enemies.Remove(enemy);
                    --enemiesCount;
                }
                else
                {
                    for (int j = 0; j < enemies.Count; ++j)
                    {
                        if (i != j)
                        {
                            Enemy anotherEnemy = enemies.ElementAt(j);
                            enemy.CheckCollision(anotherEnemy);
                        }
                    }

                    if (freeze)
                    {
                        freezeTime += (int)DeltaTime;

                        if (freezeTime >= freezeTime2)
                            freeze = false;

                        break;
                    }
                    else
                        enemy.Update(DeltaTime);
                }
            }
        }

        /// <summary>
        /// Metoda sprawdza wystêpowanie kolizji pomiêdzy parametrem
        /// do niej przekazanym a przeciwnikami obecnie przebywaj¹cymi
        /// na mapie.
        /// </summary>
        /// <param name="actor">Obiekt, którego kolizjê z przeciwnikami nale¿y zbadaæ.</param>
        public void CheckCollisions(Actor actor)
        {
            for(int i = 0; i < enemies.Count; ++i)
            {
                Enemy enemy = enemies.ElementAt(i);
                enemy.CheckCollision(actor);
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy Graphics pozwalaj¹cy
        /// na tworzenie nowych obrazów, czcionek, rysowanie na ekranie.</param>
        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < enemies.Count; ++i)
            {
                Enemy enemy = enemies.ElementAt(i);
                enemy.Draw(graphics);
            }
        }

        /// <summary>
        /// Metoda przenosi oczekuj¹cego przeciwnika z gara¿u na mapê.
        /// </summary>
        /// <param name="spot">Punkt spawnu.</param>
        public void Spawn(Vector2 spot)
        {
            if (garage.Count >= 1)
            {
                Enemy enemy = garage.First();
                enemy.SetSpawn(spot);
                enemies.Add(enemy);
                garage.Remove(enemy);
            }
        }

        /// <summary>
        /// Metoda usuwa wszystkie wrogie jednostki z mapy.
        /// </summary>
        public void Detonate()
        {
            ExplosionSound.Play(1.0f);
            enemies.Clear();
        }

        /// <summary>
        ///  Metoda zatrzymuje wszystkie wrogie jednostki na mapie.
        /// </summary>
        public void FreezeMoving()
        {
            freeze = true;
            freezeTime = 0;
        }
        #endregion
    }
}
