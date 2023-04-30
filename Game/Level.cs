using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class Level
    {
        public List<Monster> MonsterList = new List<Monster>();
        public List<Item> ItemList = new List<Item>();

        public readonly int Width;
        public readonly int Height;
        //background
        public readonly Point PlayerStartPosition;

        public Level(int width, int height, Point position)
        {
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
