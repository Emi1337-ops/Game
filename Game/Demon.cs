using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Point = System.Drawing.Point;

namespace Game
{
    public class Demon
    {
        private readonly int BlockSize = 64;
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction = "Right";
        public string DirectionExtra;
        public readonly int Speed;
        public Bitmap BulletType
        {
            get { return new Bitmap("Images\\DemonBullet.png"); }
        }
        public Bitmap Image
        {
            get
            {
                return new Bitmap($"Images\\Demon{Direction}.png");
            }
        }

        public bool RemoveFlag { get; set; }
        public int Hp = 100;

        public readonly int Damage;

        private static int TickCount = 0;
        private int tickLine = TickCount;

        #region MoveDirection
        public bool GoUp { get; set; }
        public bool GoBack { get; set; }
        public bool GoRight { get; set; }
        public bool GoLeft { get; set; }
        #endregion

        #region Demon
        public Demon(int x, int y, int damage)
        {
            X = x * 64;
            Y = y * 64;
            RemoveFlag = false;
            Damage = damage;
            Speed = 1;
        }
        #endregion

        #region Move
        public void Move(Game game)
        {
            #region ChooseMoveDirection
                var monsterPoint = new Point(X, Y);
                var playerPoint = new Point(game.Player.X, game.Player.Y);
                GoLeft = false;
                GoBack = false;
                GoUp = false;
                GoRight = false;

                var direction = FindDirection(monsterPoint, playerPoint);
                if (direction == "Up")
                {
                    if (!CanMove(game, IsUpFree))
                    {
                        if (DirectionExtra == "Right") GoRight = true;
                        else GoLeft = true;
                    }
                    else GoUp = true;
                }
                if (direction == "Back")
                {
                    if (!CanMove(game, IsBackFree))
                    {
                        if (DirectionExtra == "Right") GoRight = true;
                        else GoLeft = true;
                    }
                    else GoBack = true;
                }
                if (direction == "Right")
                {
                    if (!CanMove(game, IsRightFree))
                    {
                        if (DirectionExtra == "Up") GoUp = true;
                        else GoBack = true;
                    }
                    else GoRight = true;
                }
                if (direction == "Left")
                {
                    if (!CanMove(game, IsLeftFree))
                    {
                        if (DirectionExtra == "Up") GoUp = true;
                        else GoBack = true;
                    }
                    else GoLeft = true;
                }
            #endregion

            #region Move
            if (GoRight 
                && (X < game.Level.Width * game.BlockSize - game.BlockSize * 2))
            {
                X += Speed;
                if (TickCount - tickLine > 35)
                {
                    Direction = "Right";
                    tickLine = TickCount;
                }    
            }
            if (GoLeft && X > 64)
            {
                X -= Speed;
                if (TickCount - tickLine > 35)
                {
                    Direction = "Left";
                    tickLine = TickCount;
                }
            }

            if (GoBack 
                && (Y < game.Level.Height * game.BlockSize - game.BlockSize * 2))
            {   
                Y += Speed;
                if (TickCount - tickLine > 35)
                {
                    Direction = "Back";
                    tickLine = TickCount;
                }
            }

            if (GoUp && (Y > 64))
            {
                Y -= Speed;
                if (TickCount - tickLine > 35)
                {
                    Direction = "Up";
                    tickLine = TickCount;
                }
            }
            #endregion

        }
        #endregion

        public void Act(Game game)
        {
            TickCount++;
            if (game.TickCount % 200 == 0)
            {
                Fire(game);
            }
            else Move(game);
            
        }
        public void Fire(Game game)
        {
            game.Level.Add(new Bullet(X, Y, Direction, Damage, BulletType, 8));
        }
        public void GetDamage(Game game)
        {
            Hp -= 20;
            if (Hp <= 0) game.Level.DemonRemove.Add(this);
        }
        public string FindDirection(Point center, Point other)
        {
            var degrees = ((Math.Atan2(center.Y - other.Y, center.X - other.X) + 2 * Math.PI) * 180 / Math.PI) % 360;
            string side = null;
            string sideEx = null;

            if (degrees > 315 && degrees < 360)
            { 
                side = "Left";
                sideEx = "Down";
            }
            if (degrees >= 0 && degrees <= 45) 
            {
                side = "Left";
                sideEx = "Up";
            }
            
            if (degrees > 45 && degrees <= 90)
            {
                side = "Up";
                sideEx = "Right";
            }
            if (degrees > 90 && degrees <= 135)
            {
                side = "Up";
                sideEx = "Left";
            }

            if (degrees > 135 && degrees <= 180)
            {
                side = "Right";
                sideEx = "Up";
            }
            if (degrees > 180 && degrees <= 225)
            {
                side = "Right";
                sideEx = "Down";
            }

            if (degrees > 225 && degrees <= 270)
            {
                side = "Back";
                sideEx = "Right";
            }
            if (degrees > 270 && degrees <= 315)
            {
                side = "Back";
                sideEx = "Left";
            }

            DirectionExtra = sideEx;
            return side;
        }

        #region CanMove
        public bool CanMove(Game game, Func<Rectangle, Rectangle, bool> IsFree)
        {
            var flag = true;
            var recMain = new Rectangle(X, Y, BlockSize, BlockSize);

            var recPlayer = new Rectangle(game.Player.X, game.Player.Y, BlockSize, BlockSize);
            if (!IsFree(recMain, recPlayer)) flag = false;

            foreach (var monster in game.Level.DemonList)
            {
                var recMon = new Rectangle(monster.X, monster.Y, BlockSize, BlockSize);
                if (!IsFree(recMain, recMon)) flag = false;
            };
            foreach (var infernal in game.Level.InfernalList)
            {
                var recMon = new Rectangle(infernal.X, infernal.Y, BlockSize, BlockSize);
                if (!IsFree(recMain, recMon)) flag = false;
            };
            foreach (var wall in game.Level.WallList)
            {
                var recWall = new Rectangle(wall.X, wall.Y, BlockSize, BlockSize);
                if (!IsFree(recMain, recWall)) flag = false;
            };
            return flag;
        }
        #endregion

        #region IsFree
        private bool IsUpFree(Rectangle recMain, Rectangle recOther)
        {
            if (Math.Abs((recMain.Location.X + recMain.Width / 2)
                - (recOther.Location.X + recOther.Width / 2)) < BlockSize)
            {
                if ((recMain.Location.Y - (recOther.Location.Y + recOther.Height) < 1d)
                    && (recOther.Location.Y < recMain.Location.Y))
                    return false;
            }
            return true;
        }
        private bool IsBackFree(Rectangle recMain, Rectangle recOther)
        {
            if (Math.Abs((recMain.Location.X + recMain.Width / 2)
                - (recOther.Location.X + recOther.Width / 2)) < BlockSize)
            {
                if ((recOther.Location.Y - (recMain.Location.Y + recMain.Height) < 1)
                    && (recMain.Location.Y < recOther.Location.Y))
                    return false;
            }
            return true;
        }
        private bool IsRightFree(Rectangle recMain, Rectangle recOther)
        {
            if (Math.Abs((recMain.Location.Y + recMain.Height / 2)
                - (recOther.Location.Y + recOther.Height / 2)) < BlockSize)
            {
                if ((recOther.Location.X - (recMain.Location.X + recMain.Width) < 4)
                    && (recMain.Location.X < recOther.Location.X))
                    return false;
            }
            return true;
        }
        private bool IsLeftFree(Rectangle recMain, Rectangle recOther)
        {
            if (Math.Abs((recMain.Location.Y + recMain.Height / 2)
                - (recOther.Location.Y + recOther.Height / 2)) < BlockSize)
            {
                if ((recMain.Location.X - (recOther.Location.X + recOther.Width) < 4)
                    && (recOther.Location.X < recMain.Location.X))
                    return false;
            }
            return true;
        }
        #endregion
    }
}
