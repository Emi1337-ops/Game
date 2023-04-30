using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;

namespace Game
{
    public class Levels
    {
        public static Level Level1
        {
            get
            {
                var level = new Level(23, 13, new Point(128, 128));
                level.Add(new Item(512, 256, "aid"));
                level.Add(new Item(256, 256, "ammo"));
                level.Add(new Monster(480, 480, 50));
                level.Add(new Monster(512, 64, 50));
                level.Add(new Monster(64, 320, 50));
                level.Add(new Monster(512, 512, 50));
                level.Add(new Monster(320, 128, 50));
                return level;
            }
        }
        public static Level Level2
        {
            get
            {
                var level = new Level(10, 10, new Point(70, 70));
                level.Add(new Item(256, 256, "ammo"));
                level.Add(new Monster(128, 320, 50));
                level.Add(new Monster(480, 240, 50));
                level.Add(new Monster(480, 480, 50));
                return level;
            }
        }

    }
}
