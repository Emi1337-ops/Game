using System;
using System.Collections.Generic;
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

        public int TickCount = 0;

        public bool GoUp;
        public bool GoBack;
        public bool GoRight;
        public bool GoLeft;

        #region Monster
        public Monster(int x, int y, string direction)
        {
            X = x;
            Y = y;
            Direction = direction;
            RemoveFlag = false;
        }
        #endregion

        #region Move
        public void Move(Game game)
        {
            if (GoRight)
            {
                if (X < game.Level.Width * game.BlockSize - game.BlockSize * 2)
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

        public void Act(Game game)
        {
            TickCount++;
            if (TickCount % 100 == 0)
                Fire(game);
        }

        public void Fire(Game game)
        {
            game.BulletList.Add(new Bullet(X + game.BlockSize / 2, Y + game.BlockSize / 2 - 13, Direction));
        }
    }
}
