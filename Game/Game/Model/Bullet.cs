using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Bullet
    {
        public readonly int BlockSize = 64;
        public int X { get; set; }
        public int Y { get; set; }

        public readonly string Direction;
        public readonly int Speed;
        public readonly int Size;
        public readonly int Damage;
        public bool RemoveFlag = false;
        public readonly Bitmap Image;
        public Infernal parent = null;

        public Bullet(int ObjectX, int ObjectY, string direction, int damage, Bitmap type, int speed)
        {
            Direction = direction;
            Damage = damage;
            Image = type;
            Size = Image.Height;
            var Start = GetBulletFirePosition(ObjectX, ObjectY);
            X = Start.X;
            Y = Start.Y;//все объекты в игре квадратные
            Speed = speed;
        }

        #region Move
        public void Move(Game game)
        {

            ChekIntersection(game);
            if (Direction == "Up")
            {
                if (Y <= BlockSize)
                    RemoveFlag = true;
                else Y -= Speed;
            }
            if (Direction == "Back")
            {
                if (Y > game.Level.Height * BlockSize - BlockSize - Size)
                    RemoveFlag = true;
                else Y += Speed;
            }
            if (Direction == "Right")
            {
                if (X > game.Level.Width * BlockSize - BlockSize - Size)
                    RemoveFlag = true;
                else X += Speed;
            }
            if (Direction == "Left")
            {
                if (X <= BlockSize + Size)
                    RemoveFlag = true;
                else X -= Speed;
            }
        }
        #endregion

        #region ChekIntersection
        public void ChekIntersection(Game game)
        {
            var recB = new Rectangle(X, Y, Size, Size);
            foreach(var monster in game.Level.DemonList)
            {
                var recM = new Rectangle(monster.X, monster.Y, BlockSize, BlockSize);  
                if(recB.IntersectsWith(recM))
                {
                    monster.GetDamage(game);
                    RemoveFlag = true;
                }
            };
            foreach (var infernal in game.Level.InfernalList)
            {
                var recM = new Rectangle(infernal.X, infernal.Y, BlockSize, BlockSize);
                if (recB.IntersectsWith(recM))
                {
                    infernal.GetDamage(game);
                    RemoveFlag = true;
                }
            };
            foreach (var wall in game.Level.WallList)
            {
                var recM = new Rectangle(wall.X, wall.Y, BlockSize, BlockSize);
                if (recB.IntersectsWith(recM))
                    RemoveFlag = true;
            };

            var recP = new Rectangle(game.Player.X, game.Player.Y, BlockSize, BlockSize);
            if (recB.IntersectsWith(recP))
            {
                game.Player.GetDamage();
                RemoveFlag = true;
            }
        }
        #endregion

        public Point GetBulletFirePosition(int ObjectX,int ObjectY)
        {
            if (Direction == "Up")
                return new Point(ObjectX + BlockSize / 2 - Size, ObjectY - Size);

            if (Direction == "Back")
                return new Point(ObjectX + BlockSize / 2 - Size, ObjectY + BlockSize);

            if (Direction == "Right")
                return new Point(ObjectX + BlockSize, ObjectY + BlockSize / 2);

            else
                return new Point(ObjectX - Size, ObjectY + BlockSize / 2);
        }
    }
}
