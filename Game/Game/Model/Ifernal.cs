using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Point = System.Drawing.Point;

namespace Game
{
    public class Infernal
    {
        public int X { get; set; }
        public int Y { get; set; }

        public string Direction = "Left";

        public Bitmap BulletFirst
        {
            get {  return new Bitmap($"Images\\Soul{Direction}.png"); }
        }
        public Bitmap BulletSecond
        {
            get { return new Bitmap($"Images\\SoulBullet.png"); }
        }
        public Bitmap Image
        {
            get { return new Bitmap($"Images\\Infernal{Direction}.png"); }
        }

        public bool RemoveFlag = false;
        public int Hp = 400;

        public readonly int Damage;

        private static int TickCount = 0;
        private int tickLine = TickCount;

        #region Infernal
        public Infernal(int x, int y, int damage)
        {
            X = x * 64;
            Y = y * 64;
            RemoveFlag = false;
            Damage = damage;
        }
        #endregion

        #region ChooseDirection
        public void ChooseDirection(Game game)
        {
            var monsterPoint = new Point(X, Y);
            var playerPoint = new Point(game.Player.X, game.Player.Y);
            if (TickCount - tickLine > 35)
                FindDirection(monsterPoint, playerPoint);

        }
        #endregion

        public void Act(Game game)
        {
            TickCount++;
            if (TickCount % 250 == 0)
            {
                Fire(game);
            }
            else ChooseDirection(game);

        }
        public void Fire(Game game)
        {
            var bullet = new Bullet(X, Y, Direction, Damage, BulletFirst, 7);
            bullet.parent = this;
            game.Level.Add(bullet);
        }
        public void FireSecond(Game game, int x, int y, string dimension)
        {
            
            if (dimension == "X")
            {
                game.Level.BulletList.Add(
                    new Bullet(x, y, "Right", 15, new Bitmap(($"Images\\SoulBullet.png")), 8));
                game.Level.BulletList.Add(
                    new Bullet(x, y, "Left", 15, new Bitmap(($"Images\\SoulBullet.png")), 8));
            }
            else
            {
                game.Level.BulletList.Add(
                    new Bullet(x, y, "Up", 15, new Bitmap(($"Images\\SoulBullet.png")), 8));
                game.Level.BulletList.Add(
                    new Bullet(x, y, "Back", 15, new Bitmap(($"Images\\SoulBullet.png")), 8));
            }
        }
        public void GetDamage(Game game)
        {
            Hp -= 20;
            if (Hp <= 0) game.Level.InfernalRemove.Add(this);
        }
        public void FindDirection(Point center, Point other)
        {
            var degrees = ((Math.Atan2(center.Y - other.Y, center.X - other.X) + 2 * Math.PI) * 180 / Math.PI) % 360;
            string side = null;

            if (degrees > 315 && degrees < 360) side = "Left";
            if (degrees >= 0 && degrees <= 45) side = "Left";

            if (degrees > 45 && degrees <= 90) side = "Up";
            if (degrees > 90 && degrees <= 135) side = "Up";

            if (degrees > 135 && degrees <= 180) side = "Right";
            if (degrees > 180 && degrees <= 225) side = "Right";

            if (degrees > 225 && degrees <= 270) side = "Back";
            if (degrees > 270 && degrees <= 315) side = "Back";

            Direction =  side;
        }
    }
}
