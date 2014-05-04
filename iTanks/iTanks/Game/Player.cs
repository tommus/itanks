using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.Objects;
using GameFramework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using iTanks.Game.AI;
using Microsoft.Xna.Framework;

namespace iTanks.Game
{
    public class Player : Actor
    {
        #region Internal Classes
        public class TankType
        {
            public static int NORMAL = 0;
            public static int HEAVY = 4;
        }
        #endregion
        #region Fields
        private static Player instance;
        private Animation up;
        private Animation down;
        private Animation left;
        private Animation right;

        private Boolean hidePlayer;
        private Boolean isCollision;
        private Boolean isShipped;
        private float shipTime;
        private float shipTime2;
        private Image ship;
        private Boolean isShielded;
        private float shieldTime;
        private float shieldTime2;
        private Animation shield;
        private float speed;
        private float bulletSpeed;
        private int lastX;
        private int lastY;
        private int cannon;
        private float cannonTime;
        private float cannonTime2;
        private Boolean cannonReady;
        private Sound fire;
        private Sound move;
        private Boolean alreadySpawned;
        private float spawnTimer;
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuj¹cy jedyn¹ instancjê obiektu.
        /// </summary>
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();

                return instance;
            }
        }

        /// <summary>
        /// Parametr przechowuj¹cy aktualn¹ pozycjê gracza.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2(x, y);
            }
        }

        /// <summary>
        /// Parametr przechowuj¹cy liczbê pozosta³ych graczowi ¿yæ.
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// Parametr przechowuj¹cy aktualn¹ punktacjê gracza.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Parametr przechowuj¹cy aktualny kierunek gracza.
        /// </summary>
        public int Direction { get; set; }

        /// <summary>
        /// Parametr przechowuj¹cy informacjê, czy w danej chwili gracz siê porusza.
        /// </summary>
        public Boolean IsMoving { get; set; }

        /// <summary>
        /// Parametr przechowuj¹cy informacjê, czy w danej chwili gracz jest ¿ywy.
        /// </summary>
        public Boolean Alive { get; set; }

        /// <summary>
        ///  Parametr przechowuj¹cy informacjê o szybkoœci pocisków gracza.
        /// </summary>
        public float BulletSpeed
        {
            get { return bulletSpeed; }
        }

        /// <summary>
        /// Parametr przechowuj¹cy informacjê o rodzaju dzia³a gracza.
        /// </summary>
        public int Cannon
        {
            get { return cannon; }
        }
        #endregion
        #region Constructors
        private Player() : base(Block.Type.PLAYER, 317, 429)
        {
            ship = Assets.Ship;
            shield = Assets.Shield;
            move = Assets.Move;
            fire = Assets.Fire;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda ustawia parametry pocz¹tkowe gracza w zale¿noœci od przekazanego parametru.
        /// </summary>
        /// <param name="newGame">Parametr informuj¹cy czy nale¿y przygotowaæ gracza do nowej gry.</param>
        public void PreparePlayer(Boolean newGame)
        {
            hidePlayer = false;
            cannonReady = true;
            cannonTime = 0;
            cannonTime2 = 600;
            Alive = true;
            isShipped = false;
            shipTime = 0;
           
            isShielded = false;
            shieldTime = 0;

            isCollision = false;
            if (newGame)
            {
                Score = 0;
                Lives = 3;
            }
            x = 317;
            y = 429;
            Direction = iTanks.Game.Direction.UP;
            IsMoving = false;
            SetTank(TankType.NORMAL);
            SetShield(5000);
        }

        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public override void Update(float DeltaTime)
        {
            if(alreadySpawned)
            {
                spawnTimer += DeltaTime;

                if (spawnTimer >= 1200)
                    alreadySpawned = false;
            }

            if(!cannonReady)
            {
                cannonTime += DeltaTime;

                if (cannonTime >= cannonTime2)
                    cannonReady = true;
            }

            if(IsMoving && !isCollision)
            {
                lastX = x;
                lastY = y;
                int delta = (int)Math.Round(speed * DeltaTime / 10);
                switch(Direction)
                {
                    case iTanks.Game.Direction.UP:
                        up.Update(DeltaTime);
                        if (y - delta >= 19)
                        {
                            y -= delta;
                        }
                        break;
                    case iTanks.Game.Direction.DOWN:
                        down.Update(DeltaTime);
                        if (y + delta <= 460 - down.Image.Height)
                        {
                            y += delta;
                        }
                        break;
                    case iTanks.Game.Direction.LEFT:
                        left.Update(DeltaTime);
                        if(x - delta >= 179)
                        {
                            x -= delta;
                        }
                        break;
                    case iTanks.Game.Direction.RIGHT:
                        right.Update(DeltaTime);
                        if(x + delta <= 620 - right.Image.Width)
                        {
                            x += delta;
                        }
                        break;
                }
            }
            if(isCollision)
            {
                x = lastX;
                y = lastY;
                isCollision = false;
            }
            if (isShielded)
            {
                shieldTime += DeltaTime;

                if(shieldTime >= shieldTime2)
                    isShielded = false;

                shield.Update(DeltaTime);
            }
            if(isShipped)
            {
                shipTime += DeltaTime;

                if (shipTime >= shipTime2)
                    isShipped = false;
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy 'Graphics' pozwalaj¹cy tworzyæ nowe obrazy, czcionki, rysowaæ na ekranie.</param>
        public override void Draw(Graphics graphics)
        {
            if (!alreadySpawned)
            {
                switch (Direction)
                {
                    case iTanks.Game.Direction.UP:
                        graphics.DrawImage(up.Image, x, y);
                        break;
                    case iTanks.Game.Direction.DOWN:
                        graphics.DrawImage(down.Image, x, y);
                        break;
                    case iTanks.Game.Direction.LEFT:
                        graphics.DrawImage(left.Image, x, y);
                        break;
                    case iTanks.Game.Direction.RIGHT:
                        graphics.DrawImage(right.Image, x, y);
                        break;
                }
                if (isShielded)
                {
                    int posX = x - ((shield.Image.Width - width) / 2);
                    int posY = y - ((shield.Image.Height - height) / 2);
                    graphics.DrawImage(shield.Image, posX, posY);
                }
                if (isShipped)
                {
                    int posX = x - ((ship.Width - width) / 2);
                    int posY = y - ((ship.Height - height) / 2);
                    graphics.DrawImage(ship, posX, posY);
                }
            }
        }

        /// <summary>
        /// Metoda ustawia typ czo³gu gracza.
        /// </summary>
        /// <param name="tankType">Typ czo³gu gracza. Element klasy TankType.</param>
        public void SetTank(int tankType)
        {
            if(tankType == TankType.NORMAL)
            {
                speed = 1.0f;
            }
            else
            {
                speed = 1.5f;
            }

            bulletSpeed = speed * 2;
            cannon = tankType;

            up = Assets.Player[tankType];
            down = Assets.Player[tankType + 1];
            left = Assets.Player[tankType + 2];
            right = Assets.Player[tankType + 3];

            switch (Direction)
            {
                case iTanks.Game.Direction.UP:
                case iTanks.Game.Direction.DOWN:
                    height = up.Image.Height;
                    width = up.Image.Width;
                    break;
                case iTanks.Game.Direction.LEFT:
                case iTanks.Game.Direction.RIGHT:
                    height = left.Image.Height;
                    width = left.Image.Width;
                    break;
            }
        }

        /// <summary>
        /// Metoda umo¿liwia poruszanie siê graczowi po wodzie na okreœlony okres czasu.
        /// </summary>
        /// <param name="time">Czas dzia³ania bonusu.</param>
        public void SetShip(int time)
        {
            shipTime2 = time;
            isShipped = true;
            shipTime = 0;
        }

        /// <summary>
        /// Metoda ustawia tarczê gracza na aktywn¹ na okres czasu.
        /// </summary>
        /// <param name="time">Czas dzia³ania bonusu.</param>
        public void SetShield(int time)
        {
            shieldTime2 = time;
            isShielded = true;
            shieldTime = 0;
        }

        /// <summary>
        /// Metoda odtwrza dŸwiêk wystrza³u.
        /// </summary>
        public void PlayFire()
        {
            fire.Play(1.0f);
            cannonReady = false;
        }

        /// <summary>
        /// Metoda odpowiedzialna za reakcjê na kolizjê z obiektem.
        /// </summary>
        /// <param name="a">Obiekt, na którego kolizjê nale¿y zareagowaæ.</param>
        public override void Collision(Actor a)
        {
            if (a is Brick || a is Stone || a is Enemy || a is Eagle)
                isCollision = true;

            if (a is Water && !isShipped)
                isCollision = true;

            if (a is Bullet)
            {
                Bullet bullet = (Bullet)a;
                if (!(bullet.Owner is Player) && !isShielded)
                {
                    if (Lives >= 1)
                    {
                        --Lives;
                        PreparePlayer(false);
                        Level.Instance.AddSpawn(new Spawn(x, y));
                        alreadySpawned = true;
                    }
                    else
                        Alive = false;
                }
            }
        }
        #endregion
    }
}