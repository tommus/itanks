using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using iTanks.Game.Objects;
using iTanks.Game.AI;
using Microsoft.Xna.Framework;

namespace iTanks.Game
{
    public class Level
    {
        #region Internal Classes
        public class SpawnSpot
        {
            public static Vector2 ONE = new Vector2(180, 20);
            public static Vector2 TWO = new Vector2(385, 20);
            public static Vector2 THREE = new Vector2(588, 20);
        }
        #endregion
        #region Fields
        private static Level instance;

        private int easyEnemies;
        private int mediumEnemies;
        private int hardEnemies;
        private int enemies;

        private int currentSpawnSpot;
        private int number;

        private Map map;
        private Brigade brigade;

        private Music entrance;
        private Music outrance;
        private List<Bullet> bullets;

        private Bonus bonus;
        private float bonusTime;
        private float bonusTime2;

        private Boolean eagleShield;
        private float eagleTimer;

        private List<Explosion> explosions;
        private List<Spawn> spawns;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje informacjê, czy 'orze³ek' wci¹¿ ¿yje.
        /// </summary>
        public Boolean EagleAlive { get; set; }

        /// <summary>
        /// Parametr przechowuje informacjê, czy poziom zosta³ ju¿ wyczyszczony.
        /// Tj.: Informacjê o tym, czy zarówno w gara¿u jak i na mapie nie ma przeciwników.
        /// </summary>
        public Boolean Cleared { get; set; }

        /// <summary>
        /// Parametr przechowuje informacjê o po³o¿eniu 'orze³ka'.
        /// </summary>
        public Vector2 Eagle
        {
            get { return map.EaglePosition; }
        }

        /// <summary>
        /// Parametr przechowuje obiekt klasy 'Brigade'.
        /// </summary>
        public Brigade Brigade
        {
            get { return brigade; }
        }

        /// <summary>
        /// Parametr przechowuje informacje opisuj¹ce stan przeciwników na danym poziomie.
        /// </summary>
        public int[] Enemies
        {
            get { return new int[4] { easyEnemies - brigade.Easy, mediumEnemies - brigade.Medium, hardEnemies - brigade.Hard, enemies }; }
        }

        /// <summary>
        /// Parametr przechowuje informacjê o numerze aktualnego poziomu.
        /// </summary>
        public int Number
        {
            get { return number; }
        }

        /// <summary>
        /// Parametr przechowuje jedyn¹ instancjê klasy singletonowej.
        /// </summary>
        public static Level Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Level();
                }

                return instance;
            }
        }
        #endregion
        #region Constructors
        private Level()
        {
            entrance = Assets.GameStart;
            outrance = Assets.GameOver;
            bonusTime2 = Bonus.AVAILABLE_TIME;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda przygotowuje poziom w oparciu o dane z edytora map.
        /// </summary>
        /// <param name="map">Mapa przygotowana w edytorze.</param>
        /// <param name="brigade">Obiekt klasy 'Brigade' przechowuj¹cy informacje o przeciwnikach.</param>
        public void PrepareLevel(Map map, Brigade brigade)
        {
            EagleAlive = true;
            currentSpawnSpot = 0;
            this.brigade = brigade;
            enemies = brigade.Enemies;
            easyEnemies = brigade.Easy;
            mediumEnemies = brigade.Medium;
            hardEnemies = brigade.Hard;
            Cleared = false;
            bonusTime = 0;
            bonus = null;
            spawns = new List<Spawn>();
            bullets = new List<Bullet>();
            explosions = new List<Explosion>();
            this.number = 0;
            this.map = map;
            Spawn();
            Spawn();
            Spawn();
        }

        /// <summary>
        /// Metoda przygotowuje poziom w oparciu o wybrany numer poziomu.
        /// </summary>
        /// <param name="number">Numer poziomu.</param>
        public void PrepareLevel(int number)
        {
            EagleAlive = true;
            currentSpawnSpot = 0;
            brigade = new Brigade(number);
            enemies = brigade.Enemies;
            easyEnemies = brigade.Easy;
            mediumEnemies = brigade.Medium;
            hardEnemies = brigade.Hard;
            Cleared = false;
            bonusTime = 0;
            bonus = null;
            spawns = new List<Spawn>();
            bullets = new List<Bullet>();
            explosions = new List<Explosion>();
            this.number = number;
            this.map = new Map(number);
            Spawn();
            Spawn();
            Spawn();
        }

        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public void Update(float DeltaTime)
        {
            enemies = brigade.Enemies;

            if(eagleShield)
            {
                eagleTimer += DeltaTime;

                if(eagleTimer >= Shield.SHIELD_TIME)
                {
                    map.SetShield(Block.Type.BRICK);
                    eagleShield = false;
                }
            }

            if(bonus != null)
            {
                bonusTime += DeltaTime;

                if(bonusTime >= bonusTime2 || bonus.ToRemove)
                {
                    bonusTime = 0;
                    bonus = null;
                }
                else
                    bonus.Update(DeltaTime);
            }

            map.Update(DeltaTime);
            brigade.Update(DeltaTime);

            for(int i = 0; i < bullets.Count; ++i)
            {
                Bullet bullet = bullets.ElementAt(i);
                if (bullet.ToRemove)
                {
                    if (bullet.Hited == Block.Type.BRICK)
                        bullet.PlayBrick();
                    
                    if(bullet.Hited == Block.Type.STONE || bullet.Hited == 0)
                        bullet.PlaySteel();

                    bullets.Remove(bullet);
                }
                else
                {
                    if(Player.Instance.Alive)
                        bullet.Update(DeltaTime);

                    if (bullet.Bounds.X <= 179 || bullet.Bounds.X + bullet.Bounds.Width >= 620 || bullet.Bounds.Y <= 19 || bullet.Bounds.Y - bullet.Bounds.Height >= 460)
                    {
                        AddExplosion(new Explosion(bullet.Bounds.X - 12, bullet.Bounds.Y - 12));
                        bullet.ToRemove = true;
                    }
                }
            }

            for(int i = 0; i < explosions.Count; ++i)
            {
                Explosion explosion = explosions.ElementAt(i);
                if (explosion.ToRemove)
                    explosions.Remove(explosion);
                else
                    explosion.Update(DeltaTime);
            }

            for(int i = 0; i < spawns.Count; ++i)
            {
                Spawn spawn = spawns.ElementAt(i);
                if (spawn.ToRemove)
                    spawns.Remove(spawn);
                else
                    spawn.Update(DeltaTime);
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy 'Graphics' pozwalaj¹cy tworzyæ nowe obrazy, czcionki, rysowaæ na ekranie.</param>
        public void Draw(Graphics graphics)
        {
            map.Draw(graphics);
            brigade.Draw(graphics);

            for(int i = 0; i < bullets.Count; ++i)
            {
                Bullet bullet = bullets.ElementAt(i);
                bullet.Draw(graphics);
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie wierzchnich obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy 'Graphics' pozwalaj¹cy tworzyæ nowe obrazy, czcionki, rysowaæ na ekranie.</param>
        public void DrawTrees(Graphics graphics)
        {
            map.DrawTrees(graphics);

            if (bonus != null)
                bonus.Draw(graphics);

            for(int i = 0; i < explosions.Count; ++i)
            {
                Explosion explosion = explosions.ElementAt(i);
                explosion.Draw(graphics);
            }

            for(int i = 0; i < spawns.Count; ++i)
            {
                Spawn spawn = spawns.ElementAt(i);
                spawn.Draw(graphics);
            }
        }

        /// <summary>
        /// Metoda sprawdza kolizjê obiektów z obiektem przekazanym jako parametr.
        /// </summary>
        /// <param name="actor">Obiekt, z którym kolizjê nale¿y zbadaæ.</param>
        public void CheckCollisions(Actor actor)
        {
            for(int i = 0; i < brigade.Fleet.Count; ++i)
            {
                Enemy enemy = brigade.Fleet.ElementAt(i);
                map.CheckCollisions(enemy);
                map.CheckShieldCollisions(enemy);
            }

            map.CheckCollisions(actor);
            map.CheckShieldCollisions(actor);
            brigade.CheckCollisions(actor);

            for(int i = 0; i < bullets.Count; ++i)
            {
                Bullet bullet = bullets.ElementAt(i);
                brigade.CheckCollisions(bullet);
                map.CheckCollisions(bullet);
                map.CheckShieldCollisions(bullet);

                Player.Instance.CheckCollision(bullet);

                for(int j = 0; j < bullets.Count; ++j)
                {
                    if(i != j)
                    {
                        Bullet anotherBullet = bullets.ElementAt(j);
                        bullet.CheckCollision(anotherBullet);
                    }
                }
            }

            if(bonus != null)
                bonus.CheckCollision(actor);
        }

        /// <summary>
        /// Spawnuje przeciwnika na mapie.
        /// </summary>
        public void Spawn()
        {
            switch(currentSpawnSpot)
            {
                case 0:
                    spawns.Add(new Spawn((int)SpawnSpot.ONE.X, (int)SpawnSpot.ONE.Y));
                    brigade.Spawn(SpawnSpot.ONE);
                    break;
                case 1:
                    spawns.Add(new Spawn((int)SpawnSpot.TWO.X, (int)SpawnSpot.TWO.Y));
                    brigade.Spawn(SpawnSpot.TWO);
                    break;
                default:
                    spawns.Add(new Spawn((int)SpawnSpot.THREE.X, (int)SpawnSpot.THREE.Y));
                    brigade.Spawn(SpawnSpot.THREE);
                    break;
            }

            ++currentSpawnSpot;
            currentSpawnSpot %= 3;
        }

        /// <summary>
        /// Metoda dodaje animacjê spawnu do listy animacji.
        /// </summary>
        /// <param name="spawn">Animacja spawnu.</param>
        public void AddSpawn(Spawn spawn)
        {
            spawns.Add(spawn);
        }

        /// <summary>
        /// Metoda dodaje animacjê eksplozji do listy animacji.
        /// </summary>
        /// <param name="explosion">Animacja eksplozji.</param>
        public void AddExplosion(Explosion explosion)
        {
            explosions.Add(explosion);
        }

        /// <summary>
        /// Metoda dodaje pocisk do listy nadzoruj¹cej pociskami.
        /// </summary>
        /// <param name="b">Pocisk.</param>
        public void AddBullet(Bullet b)
        {
            bullets.Add(b);
        }

        /// <summary>
        /// Metoda uruchamia muzykê wejœciow¹ poziomu.
        /// </summary>
        public void PlayEntrance()
        {
            entrance.Play();
        }

        /// <summary>
        /// Metoda uruchamia muzykê koñcow¹ gry.
        /// </summary>
        public void PlayOutrance()
        {
            outrance.Play();
        }

        /// <summary>
        /// Metoda usuwa wszystkie wrogie jednostki na mapie.
        /// </summary>
        public void DetonateBrigade()
        {
            brigade.Detonate();
        }

        /// <summary>
        /// Metoda zatrzymuje wszystkie wrogie jednostki na mapie.
        /// </summary>
        public void FreezeBrigade()
        {
            brigade.FreezeMoving();
        }

        /// <summary>
        /// Metoda ustawia typ bariery przy 'orze³ku'.
        /// </summary>
        public void ProtectEagle()
        {
            eagleTimer = 0;
            eagleShield = true;
            map.SetShield(Block.Type.STONE);
        }

        /// <summary>
        /// Metoda generuje nowy bonus na mapie.
        /// </summary>
        public void NewBonus()
        {
            System.Random random = new System.Random();
            int lottery = random.Next(Bonus.Type.GRANADE, Bonus.Type.TIMER);
            int posX = random.Next(179, 620 - 34);
            int posY = random.Next(19, 460 - 34);
            switch(lottery)
            {
                case Bonus.Type.GRANADE:
                    bonus = new Granade(posX, posY);
                    break;
                case Bonus.Type.SHIELD:
                    bonus = new Shield(posX, posY);
                    break;
                case Bonus.Type.SHIP:
                    bonus = new Ship(posX, posY);
                    break;
                case Bonus.Type.SHOVEL:
                    bonus = new Shovel(posX, posY);
                    break;
                case Bonus.Type.STAR:
                    bonus = new Star(posX, posY);
                    break;
                case Bonus.Type.TANK:
                    bonus = new Tank(posX, posY);
                    break;
                case Bonus.Type.TIMER:
                    bonus = new Timer(posX, posY);
                    break;
            }
        }
        #endregion
    }
}