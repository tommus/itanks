using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTanks.Game.Objects;
using GameFramework;
using Microsoft.Xna.Framework;

namespace iTanks.Game.AI
{
    public abstract class Enemy : Actor
    {
        #region Fields
        private const float SPAWN_IMMUNE = 3000;

        protected int direction;
        protected int lastX;
        protected int lastY;
        protected Boolean isMoving;

        protected Sound Explosion;
        protected Animation up;
        protected Animation down;
        protected Animation left;
        protected Animation right;

        protected float BulletSpeed;
        protected float Speed;
        protected Boolean isCollision;
        protected Vector2 playerPosition;
        protected Vector2 eaglePosition;
        protected float cannonTimer;
        protected float cannonTimer2;
        protected Boolean cannonReady;
        protected int lives;
        protected Boolean alreadySpawned;
        protected float spawnTimer;
        protected Boolean hide;
        protected float hideTimer;
        #endregion
        #region Internal Classes
        /// <summary>
        /// Klasa przechowuje informacjê o poziomie trudnoœci przeciwnika.
        /// </summary>
        public class Type
        {
            public const int EASY = 1;
            public const int MEDIUM = 5;
            public const int HARD = 9;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Parametr przechowuje informacjê o liczbie ¿yæ przeciwnika.
        /// </summary>
        public int Lives { get; set; }
        #endregion
        #region Constructors
        public Enemy(int x, int y, int type) : base(Type.EASY, x, y)
        {
            hideTimer = 0;
            hide = false;
            spawnTimer = 0;
            lives = 1;
            cannonTimer = 0;
            cannonTimer2 = 700;
            cannonReady = true;
            isMoving = true;
            isCollision = false;
            Explosion = Assets.ExplosionSound;
            lastX = x;
            lastY = y;
            direction = iTanks.Game.Direction.DOWN;
            up = Assets.Enemy[type];
            down = Assets.Enemy[type + 1];
            left = Assets.Enemy[type + 2];
            right = Assets.Enemy[type + 3];

            switch(direction)
            {
                case iTanks.Game.Direction.DOWN:
                case iTanks.Game.Direction.UP:
                    width = up.Image.Width;
                    height = up.Image.Height;
                    break;
                case iTanks.Game.Direction.LEFT:
                case iTanks.Game.Direction.RIGHT:
                    width = left.Image.Width;
                    height = left.Image.Height;
                    break;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Metoda aktualizuj¹ca stan obiektów na ekranie.
        /// Odpowiada za takie funkcje jak: aktualizacja, pobranie danych I/O.
        /// </summary>
        /// <param name="DeltaTime">Informacja opisuj¹ca up³ywaj¹cy czas.</param>
        public override void Update(float DeltaTime)
        {
            if(hide)
            {
                hideTimer += DeltaTime;

                if (hideTimer >= 1200)
                    hide = false;
            }

            if(alreadySpawned)
            {
                spawnTimer += DeltaTime;

                if (spawnTimer >= SPAWN_IMMUNE)
                    alreadySpawned = false;
            }

            if (lives <= 0)
            {
                Explosion.Play(1.0f);
                ToRemove = true;
                Level.Instance.Spawn();
            }

            if(!cannonReady)
            {
                cannonTimer += DeltaTime;

                if (cannonTimer >= cannonTimer2)
                    cannonReady = true;
            }

            playerPosition = Player.Instance.Position;
            eaglePosition = Level.Instance.Eagle;

            if(Math.Abs(x - playerPosition.X) < 2 || Math.Abs(x - eaglePosition.X) < 2)
            {
                if (y < playerPosition.Y || y < eaglePosition.Y)
                {
                    if(direction == iTanks.Game.Direction.DOWN)
                        Shot();
                    else
                        direction = iTanks.Game.Direction.DOWN;
                }
                else
                {
                    if (direction == iTanks.Game.Direction.UP)
                        Shot();
                    else
                        direction = iTanks.Game.Direction.UP;
                }
            }

            if(Math.Abs(y - playerPosition.Y) < 2 || Math.Abs(y - eaglePosition.Y) < 2)
            {
                if (x < playerPosition.X || x < eaglePosition.X)
                {
                    if (direction == iTanks.Game.Direction.RIGHT)
                        Shot();
                    else
                        direction = iTanks.Game.Direction.RIGHT;
                }
                else
                {
                    if (direction == iTanks.Game.Direction.LEFT)
                        Shot();
                    else
                        direction = iTanks.Game.Direction.LEFT;
                }
            }

            if (isMoving && !isCollision && !hide)
            {
                lastX = x;
                lastY = y;
                int delta = (int)Math.Round(Speed * DeltaTime / 10);

                switch (direction)
                {
                    case iTanks.Game.Direction.UP:
                        if (y - delta >= 19)
                            y -= delta;
                        else
                            isCollision = true;
                        up.Update(DeltaTime);
                        break;
                    case iTanks.Game.Direction.DOWN:
                        if (y + delta <= 460 - down.Image.Height)
                            y += delta;
                        else
                            isCollision = true;
                        down.Update(DeltaTime);
                        break;
                    case iTanks.Game.Direction.LEFT:
                        if (x - delta >= 179)
                            x -= delta;
                        else
                            isCollision = true;
                        left.Update(DeltaTime);
                        break;
                    case iTanks.Game.Direction.RIGHT:
                        if (x + delta <= 620 - right.Image.Width)
                            x += delta;
                        else
                            isCollision = true;
                        right.Update(DeltaTime);
                        break;
                }
            }
            if (isCollision)
            {
                x = lastX;
                y = lastY;
                isCollision = false;
                Calculatedirection();
            }
        }

        /// <summary>
        ///  Zadaniem metody jest okreœlenie nowego kierunku poruszania siê obiektu.
        /// </summary>
        private void Calculatedirection()
        {
            Boolean loop = true;
            int temp = 0;
            while(loop)
            {
                temp = Random.Instance.GlobalRandom.Next(1, 5);
                if (temp != direction)
                {
                    direction = temp;
                    loop = false;
                }
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za rysowanie obiektów na ekranie.
        /// </summary>
        /// <param name="graphics">Obiekt klasy Graphics pozwalaj¹cy
        /// na tworzenie nowych obrazów, czcionek, rysowanie na ekranie.</param>
        public override void Draw(Graphics graphics)
        {
            if (!hide)
            {
                switch (direction)
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
            }
        }

        /// <summary>
        /// Metoda okreœla miejsce spawnu nowej jednostki.
        /// </summary>
        /// <param name="spot">Punkt spawnu przeciwnika.</param>
        public void SetSpawn(Vector2 spot)
        {
            this.x = (int)spot.X;
            this.y = (int)spot.Y;
            hide = true;
            hideTimer = 0;
        }

        /// <summary>
        /// Metoda umo¿liwiaj¹ca wrogim jednostkom strzelanie.
        /// </summary>
        public void Shot()
        {
            if (cannonReady)
            {
                int offsetX = 0;
                int offsetY = 0;
                switch (direction)
                {
                    case iTanks.Game.Direction.UP:
                        offsetX = Bounds.Width / 2 - 4;
                        break;
                    case iTanks.Game.Direction.DOWN:
                        offsetX = Bounds.Width / 2 - 4;
                        offsetY = Bounds.Height;
                        break;
                    case iTanks.Game.Direction.LEFT:
                        offsetY = Bounds.Height / 2 - 4;
                        break;
                    case iTanks.Game.Direction.RIGHT:
                        offsetX = Bounds.Width;
                        offsetY = Bounds.Height / 2 - 4;
                        break;
                }
                Level.Instance.AddBullet(new Objects.Bullet(Bounds.X + offsetX, Bounds.Y + offsetY, direction, this, BulletSpeed));

                cannonReady = false;
                cannonTimer = 0;
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za reakcjê na kolizjê z obiektem.
        /// </summary>
        /// <param name="a">Obiekt, z którym nast¹pi³a kolizja.</param>
        public override void Collision(Actor a)
        {
            if (a is Water || a is Stone || a is Enemy || a is Player || a is Eagle)
            {
                if (!(a is Enemy && alreadySpawned))
                    isCollision = true;
            }

            if (a is Brick)
            {
                isCollision = true;
                Shot();
            }

            if (a is Bullet)
            {
                if (!alreadySpawned)
                {
                    Bullet bullet = (Bullet)a;
                    if (bullet.Owner is Player && !bullet.ToRemove)
                    {
                        Player player = (Player)bullet.Owner;
                        if (player.Cannon == Player.TankType.NORMAL)
                            --lives;
                        else
                            lives -= 2;
                    }
                }
            }
        }
        #endregion
    }
}