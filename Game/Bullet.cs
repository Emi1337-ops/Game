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
        public int X;
        public int Y;
        public readonly string Direction;
        public int Damage;
        public bool RemoveFlag;

        public Bullet(int x, int y, string direction,int damage)
        {
            RemoveFlag = false;
            X = x;
            Y = y;
            Direction = direction;
            Damage = damage;
        }

        #region Move
        public void Move(Game game)
        {
            if (Direction == "Up")
            {
                if (Y <= 64)
                    RemoveFlag = true;
                else Y -= 6;
            }
            if (Direction == "Back")
            {
                if (Y > game.Level.Height * 64 - 64 - 15)
                    RemoveFlag = true;
                Y += 6;
            }
            if (Direction == "Right")
            {
                if (X > game.Level.Width * 64 - 64 - 15)
                    RemoveFlag = true;
                X += 6;
            }
            if (Direction == "Left")
            {
                if (X <= 64)
                    RemoveFlag = true;
                else X -= 6;
            }
            ChekIntersection(game);
        }
        #endregion

        #region ChekIntersection
        public void ChekIntersection(Game game)
        {
            var recB = new Rectangle(X, Y, game.BulletSize, game.BulletSize);
            foreach(var monster in game.Level.MonsterList)
            {
                var recM = new Rectangle(monster.X, monster.Y, game.BlockSize, game.BlockSize);  
                if(recB.IntersectsWith(recM))
                {
                    monster.GetDamage(game);
                    RemoveFlag = true;
                }
            };
            var recP = new Rectangle(game.Player.X, game.Player.Y, game.BlockSize, game.BlockSize);
            if (recB.IntersectsWith(recP))
            {
                game.Player.GetDamage();
                RemoveFlag = true;
            }
        }
        #endregion
    }
}
