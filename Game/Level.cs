using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Game
{
    public class Level
    {
        public List<Monster> MonsterList;
        public List<Item> ItemList;

        public Dictionary<string, object> Objects = new Dictionary<string, object>();
        public readonly int Width;
        public readonly int Height;
        //background
        public readonly Point PlayerStartPosition;

        public Level(int width, int height, Point position)
        {
            ItemList = new List<Item>();
            MonsterList = new List<Monster>();
            Width = width;
            Height = height;
            PlayerStartPosition = position;
        }

        public void Add(Monster monster)
        {
            MonsterList.Add(monster);
        }

        public void Add(Item item)
        {
            ItemList.Add(item);
        }
    }
}
