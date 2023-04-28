using System;
using System.Collections.Generic;
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
        public bool RemoveFlag;

        public Bullet(int x, int y, string direction)
        {
            RemoveFlag = false;
            X = x;
            Y = y;
            Direction = direction;
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
            if (ChekIntersection(game))
            {

            }
        }
        #endregion

        public bool ChekIntersection(Game game)
        {
            return true;
        }
    }
}
