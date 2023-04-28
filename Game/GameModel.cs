using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Game
{
    public class Game
    {
        public int BlockSize; //все объекты (кроме пуль) имеют одинаковый размер 64*64 пикселя
        public int BulletSize; //размер пули

        public List<Bullet> BulletList;
        public List<Bullet> BulletRemove;
        public List<Item> ItemRemove;
        public List<Monster> MonsterRemove;

        public Level Level;
        public Player Player;

        #region Game
        public Game(Level level)
        {
            BlockSize = 64;
            BulletSize = 15;
            
            BulletList = new List<Bullet>();
            BulletRemove = new List<Bullet>();
            ItemRemove = new List<Item>();  

            Level = level;
            Player = new Player(
                Level.PlayerStartPosition.X,
                Level.PlayerStartPosition.Y,
                "Right");
        }
        #endregion

        #region Move
        public void Move()
        {
            if (BulletList != null)
                MoveBullets();
            if (Level.ItemList != null)
                UpdateItems();
            if (Level.MonsterList != null)
                UpdateMonsters();
            Player.Move(this);
        }
        #endregion

        #region MoveBullet
        public void MoveBullets()
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
    }
}
