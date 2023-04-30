using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Drawing;
using System.Collections.Specialized;
using System.Security.Policy;
using System.Windows;
using Point = System.Drawing.Point;

namespace Game
{
    public class Game
    {
        public int BlockSize = 64; //все объекты (кроме пуль) имеют одинаковый размер 64*64 пикселя
        public int BulletSize = 15; //размер пули

        public List<Bullet> BulletList = new List<Bullet>();
        public List<Bullet> BulletRemove = new List<Bullet>();
        public List<Item> ItemRemove = new List<Item>();
        public List<Monster> MonsterRemove = new List<Monster>();

        public Level Level;
        public Player Player;

        #region KeyBoard
        public void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Player.GoUp = true;
            }
            if (e.KeyCode == Keys.A)
            {
                Player.GoLeft = true;
            }
            if (e.KeyCode == Keys.S)
            {
                Player.GoBack = true;
            }
            if (e.KeyCode == Keys.D)
            {
                Player.GoRight = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                Player.Fire(this);
            }
        }
        public void KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Player.GoUp = false;
            }
            if (e.KeyCode == Keys.A)
            {
                Player.GoLeft = false;
            }
            if (e.KeyCode == Keys.S)
            {
                Player.GoBack = false;
            }
            if (e.KeyCode == Keys.D)
            {
                Player.GoRight = false;
            }
        }
        #endregion

        #region Game
        public Game(Level level)
        {
            Level = level;
            Player = new Player(
                Level.PlayerStartPosition.X,
                Level.PlayerStartPosition.Y,
                "Right",
                30);
        }
        #endregion

        #region Move
        public void Move()
        {
            if (BulletList != null)
                UpdateBullets();
            if (Level.ItemList != null)
                UpdateItems();
            if (Level.MonsterList != null)
                UpdateMonsters();
            Player.Move(this);
        }
        #endregion

        #region UpdateBullets
        public void UpdateBullets()
        {
            foreach (Bullet bullet in BulletList)
            {
                if (bullet.RemoveFlag) BulletRemove.Add(bullet);
                bullet.Move(this);
            }

            if (BulletRemove != null)
            {
                foreach(var bullet in BulletRemove)
                {
                    BulletList.Remove(bullet);
                }
                BulletRemove.Clear();
            }
        }
        #endregion

        #region UpdateItems
        public void UpdateItems()
        {
            foreach(var item in Level.ItemList)
            {
                item.Update(this);
                if (item.RemoveFlag) ItemRemove.Add(item);
            }
            if (ItemRemove != null)
            {
                foreach (var item in ItemRemove)
                {
                    Level.ItemList.Remove(item);
                };
                ItemRemove.Clear();
            }
        }
        #endregion

        #region UpdateMonsters
        public void UpdateMonsters()
        {
            foreach(var monster in Level.MonsterList)
            {
                monster.Act(this);
                if (monster.RemoveFlag == true) MonsterRemove.Add(monster);
            }
            if (MonsterRemove != null)
            {
                foreach (var monster in MonsterRemove)
                {
                    Level.MonsterList.Remove(monster);
                };
                MonsterRemove.Clear();
            }
        }
        #endregion

        public Bullet GetBulletFirePosition(Point Position, string Direction, int Damage)
        {
            if (Direction == "Up")
                return new Bullet(Position.X + BlockSize/2 - BulletSize, Position.Y-BulletSize, Direction, Damage);

            if (Direction == "Back")
                return new Bullet(Position.X + BlockSize / 2 - BulletSize, Position.Y + BlockSize, Direction, Damage);

            if (Direction == "Right")
                return new Bullet(Position.X + BlockSize, Position.Y + BlockSize/2, Direction, Damage);

            else
                return new Bullet(Position.X-BulletSize, Position.Y + BlockSize/2, Direction, Damage);
        }

        public string FindSide(Point center, Point other)
        {
            var degrees = ((Math.Atan2(center.Y - other.Y, center.X - other.X) + 2 * Math.PI) * 180 / Math.PI) % 360;
            string side = null;
            if ((degrees > 0 && degrees <= 45) || (degrees <= 360 && degrees > 315)) side = "Left";
            if (degrees <= 135 && degrees > 45) side = "Up";
            if (degrees <= 225 && degrees > 135) side = "Right";
            if (degrees <= 315 && degrees > 225) side = "Back";
            return side;
        }
    }
}
