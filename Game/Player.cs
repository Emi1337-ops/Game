using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Game
{
    public class Player
    {
        public int X;
        public int Y;
        public string Direction;

        public int Damage;
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

        public bool GoUp;
        public bool GoBack;
        public bool GoRight;
        public bool GoLeft;

        #region Player
        public Player(int x, int y, string direction, int damage)
        {
            Hp = 100;
            Ammo = 30;
            X = x;
            Y = y;
            Direction = direction;
            Damage = damage;
        }
        #endregion

        #region Move
        public void Move(Game game)
        {
            if (GoRight)
            {
                if (X < game.Level.Width * game.BlockSize - game.BlockSize*2)
                    X += 4;
                Direction = "Right";
            }

            if (GoLeft)
            {
                if (X > 64)
                    X -= 4;
                Direction = "Left";
            }

            if (GoBack)
            {
                if (Y < game.Level.Height * game.BlockSize - game.BlockSize * 2)
                    Y += 4;
                Direction = "Back";
            }

            if (GoUp)
            {
                if (Y > 64)
                    Y -= 4;
                Direction = "Up";
            }
        }
        #endregion

        #region Fire
        public void Fire(Game game)
        {
            if (Ammo > 0)
            {
                game.BulletList.
                        Add(game.GetBulletFirePosition(new Point(X, Y), Direction, Damage));
                Ammo--;
            };
        }
        #endregion

        #region Damage
        public void GetDamage()
        {
            Hp -= 10;
            if (Hp <= 0)
            {
                Death();
            }
        }
        #endregion

        #region Death
        public void Death()
        {

        }
        #endregion
    }
}