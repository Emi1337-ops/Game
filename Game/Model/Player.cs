using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Game.Model;

namespace Game
{
    public class Player
    {
        private readonly int BlockSize = 64;
        public int X { get; set; }
        public int Y { get; set; }

        public string Direction = "Right";
        public Bitmap BulletImage
        {
            get { return new Bitmap("Images\\fireball.png"); } 
        }
        public Bitmap Image
        {
            get { return new Bitmap($"Images\\DoomGuy{Direction}.png"); }
        }
        public MainForm Form { get; set; }
        public int Damage { get; }
        public int Speed { get; set; }

        private readonly int MaxHp = 100;
        private readonly int MaxAmmo = 30;
        private int hp;
        private int ammo;
        public int Hp 
        { 
           get { return hp; }
            set 
            {
                hp = value;
                if (hp > MaxHp)
                    hp = MaxHp;
                if (hp < 0)
                    hp = 0;
            }
        }
        public int Ammo
        {
            get { return ammo; }
            set
            {
                ammo = value;
                if (ammo > MaxAmmo)
                    ammo = MaxAmmo;
            }
        }

        #region Player
        public Player(int x, int y, string direction, int damage, MainForm form)
        {
            Hp = 100;
            Ammo = 30;
            X = x*64;
            Y = y*64;
            Direction = direction;
            Damage = damage;
            Speed = 4;
            Form = form;
        }
        #endregion

        #region MoveDirection
        public bool GoUp { get; set; }
        public bool GoBack { get; set; }
        public bool GoRight { get; set; }
        public bool GoLeft { get; set; }
        #endregion

        #region Move
        public void Move(Game game)
        {
            if (GoRight)
            {
                if ((X < game.Level.Width * game.BlockSize - game.BlockSize*2)
                    && CanMove(game, IsRightFree))
                    X += Speed;
                Direction = "Right";
            }

            if (GoLeft)
            {
                if ((X > 64) && CanMove(game, IsLeftFree))
                    X -= Speed;
                Direction = "Left";
            }

            if (GoBack)
            {
                if ((Y < game.Level.Height * game.BlockSize - game.BlockSize * 2)
                    && CanMove(game, IsBackFree))
                    Y += Speed;
                Direction = "Back";
            }

            if (GoUp)
            {
                if ((Y > 64) && CanMove(game, IsUpFree))
                    Y -= Speed;
                Direction = "Up";
            }
        }
        #endregion

        #region Fire
        public void Fire(Game game)
        {
            if (Ammo > 0)
            {
                game.Level.
                        Add(new Bullet(X, Y, Direction, Damage, BulletImage, 8));
                Ammo--;
            };
        }
        #endregion

        #region Damage
        public void GetDamage()
        {
            Hp -= 10;
        }
        #endregion

        #region Death
        public void Death()
        {
            Form.ChangeStage(GameStage.Death);
        }
        #endregion

        #region CanMove
        public bool CanMove(Game game, Func<Rectangle, Rectangle, bool> IsFree)
        {
            var flag = true;
            var recP = new Rectangle(X, Y, BlockSize, BlockSize);
            foreach (var monster in game.Level.DemonList)
            {
                var recM = new Rectangle(monster.X, monster.Y, BlockSize, BlockSize);
                if (!IsFree(recP, recM)) flag = false;
            };
            foreach (var wall in game.Level.WallList)
            {
                var recW = new Rectangle(wall.X, wall.Y, BlockSize, BlockSize);
                if (!IsFree(recP, recW)) flag = false;
            };
            return flag;
        }
        #endregion

        #region IsFree
        private bool IsUpFree(Rectangle recP, Rectangle recO)
        {
            if(Math.Abs((recP.Location.X + recP.Width/2) - (recO.Location.X + recO.Width / 2)) < BlockSize)
            {
                if ((recP.Location.Y - (recO.Location.Y + recO.Height) < 1d) && (recO.Location.Y < recP.Location.Y))
                    return false;
            }
            return true;
        }
        private bool IsBackFree(Rectangle recP, Rectangle recO)
        {
            if (Math.Abs((recP.Location.X + recP.Width / 2) - (recO.Location.X + recO.Width / 2)) < BlockSize)
            {
                if ((recO.Location.Y - (recP.Location.Y + recP.Height) < 1) && (recP.Location.Y < recO.Location.Y))
                    return false;
            }
            return true;
        }
        private bool IsRightFree(Rectangle recP, Rectangle recO)
        {
            if (Math.Abs((recP.Location.Y + recP.Height / 2) - (recO.Location.Y + recO.Height / 2)) < BlockSize)
            {
                if ((recO.Location.X - (recP.Location.X + recP.Width) < 4) && (recP.Location.X < recO.Location.X))
                    return false;
            }
            return true;
        }
        private bool IsLeftFree(Rectangle recP, Rectangle recO)
        {
            if (Math.Abs((recP.Location.Y + recP.Height / 2) - (recO.Location.Y + recO.Height / 2)) < BlockSize)
            {
                if ((recP.Location.X - (recO.Location.X + recO.Width) < 4) && (recO.Location.X < recP.Location.X))
                    return false;
            }
            return true;
        }
        #endregion
    }
}