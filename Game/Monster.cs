using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    public class Monster
    {
        public readonly string Type;
        public int X;
        public int Y;
        public string Direction;
        public bool RemoveFlag;
        public int Hp = 100;
        public int Damage;

        public int TickCount = 0;

        public bool GoUp;
        public bool GoBack;
        public bool GoRight;
        public bool GoLeft;

        private bool IsFire = false;

        #region Monster
        public Monster(int x, int y, int damage)
        {
            X = x;
            Y = y;
            RemoveFlag = false;
            Damage = damage;
        }
        #endregion

        #region Move
        public void Move(Game game)
        {
            if (!IsFire)
            {
                #region ChooseMoveDirection
                var monsterPoint = new Point(X, Y);
                var playerPoint = new Point(game.Player.X, game.Player.Y);
                GoLeft = false;
                GoBack = false;
                GoUp = false;
                GoRight = false;

                if (game.FindSide(monsterPoint, playerPoint) == "Up")
                {
                    GoUp = true;
                }
                if (game.FindSide(monsterPoint, playerPoint) == "Back")
                {
                    GoBack = true;
                }
                if (game.FindSide(monsterPoint, playerPoint) == "Right")
                {
                    GoRight = true;
                }
                if (game.FindSide(monsterPoint, playerPoint) == "Left")
                {
                    GoLeft = true;
                }
                #endregion

                #region Move
                if (GoRight)
                {
                    if (X < game.Level.Width * game.BlockSize - game.BlockSize * 2)
                        X += 1;
                    Direction = "Right";
                }
                if (GoLeft)
                {
                    if (X > 64)
                        X -= 1;
                    Direction = "Left";
                }
                if (GoBack)
                {
                    if (Y < game.Level.Height * game.BlockSize - game.BlockSize * 2)
                        Y += 1;
                    Direction = "Back";
                }
                if (GoUp)
                {
                    if (Y > 64)
                        Y -= 1;
                    Direction = "Up";
                }
                #endregion
            }
        }
        #endregion

        public void Act(Game game)
        {
            TickCount++;
            if (TickCount % 80 == 0 )
            {
                Fire(game);
                if (TickCount % 500 == 0) IsFire = false;
            }
            else
            {
                IsFire = false;
                Move(game);
            }
        }

        public void Fire(Game game)
        {
            game.BulletList.Add(game.GetBulletFirePosition(new Point(X, Y), Direction, Damage));
        }

        public void GetDamage(Game game)
        {
            Hp -= 20;
            if (Hp <= 0) game.MonsterRemove.Add(this);
        }
    }
}
