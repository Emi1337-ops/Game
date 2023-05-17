using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Game
{
    public class Level
    {
        public List<Demon> DemonList = new List<Demon>();
        public List<Infernal> InfernalList = new List<Infernal>();
        public List<Item> ItemList = new List<Item>();
        public List<Bullet> BulletList = new List<Bullet>();
        public List<Wall> WallList = new List<Wall>();
 
        public List<Demon> DemonRemove = new List<Demon>();
        public List<Infernal> InfernalRemove = new List<Infernal>();
        public List<Item> ItemRemove = new List<Item>();
        public List<Bullet> BulletRemove = new List<Bullet>();


        public int Width { get; }
        public int Height { get; }
        public Point PlayerStartPosition { get; }
        public string BackgroundType { get; set; }

        public Level(int width, int height, Point position, string backgroundType)
        {
            Width = width;
            Height = height;
            PlayerStartPosition = position;
            BackgroundType = backgroundType;
        }

        public void Add(Demon monster)
        {
            DemonList.Add(monster);
        }
        public void Add(Infernal infernal)
        {
            InfernalList.Add(infernal);
        }
        public void Add(Item item)
        {
            ItemList.Add(item);
        }
        public void Add(Bullet bullet)
        {
            BulletList.Add(bullet);
        }
        public void Add(Wall wall)
        {
            WallList.Add(wall);
        }

        #region UpdateBullets
        public void UpdateBullets(Game game)
        {
            if (BulletList != null)
            {
                foreach (Bullet bullet in BulletList)
                {
                    if (bullet.RemoveFlag) BulletRemove.Add(bullet);
                    bullet.Move(game);
                }
            }
            
            if (BulletRemove != null)
            {
                foreach (var bullet in BulletRemove)
                {
                    if (bullet.parent != null)
                    {
                        if (bullet.Direction == "Up" || bullet.Direction == "Back")
                            bullet.parent.FireSecond(game, bullet.X, bullet.Y, "X");
                        else
                            bullet.parent.FireSecond(game, bullet.X, bullet.Y, "Y");
                    }
                    BulletList.Remove(bullet);
                }
                BulletRemove.Clear();
            }
        }
        #endregion

        #region UpdateItems
        public void UpdateItems(Game game)
        {
            if(ItemList!=null)
            {
                foreach (var item in ItemList)
                {
                    item.Update(game);
                    if (item.RemoveFlag) ItemRemove.Add(item);
                }
            }

            if (ItemRemove != null)
            {
                foreach (var item in ItemRemove)
                {
                    ItemList.Remove(item);
                };
                ItemRemove.Clear();
            }
        }
        #endregion

        #region UpdateMonsters
        public void UpdateMonsters(Game game)
        {
            if (DemonList != null)
            {
                foreach (var monster in DemonList)
                {
                    monster.Act(game);
                    if (monster.RemoveFlag == true) DemonRemove.Add(monster);
                }
            }
            
            if (DemonRemove != null)
            {
                foreach (var monster in DemonRemove)
                {
                    DemonList.Remove(monster);
                };
                DemonRemove.Clear();
            }
        }
        #endregion

        #region UpdateInfernals
        public void UpdateInfernals(Game game)
        {
            if (InfernalList != null)
            {
                foreach (var infernal in InfernalList)
                {
                    infernal.Act(game);
                    if (infernal.RemoveFlag == true) InfernalRemove.Add(infernal);
                }
            }

            if (InfernalRemove != null)
            {
                foreach (var infernal in InfernalRemove)
                {
                    InfernalList.Remove(infernal);
                };
                InfernalRemove.Clear();
            }
        }
        #endregion

        public void Update(Game game)
        {
            UpdateBullets(game);
            UpdateItems(game);
            UpdateMonsters(game);
            UpdateInfernals(game);
        }
    }
}
