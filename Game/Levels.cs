using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using System.IO.Packaging;

namespace Game
{
    public class Levels
    {
        public static Level LevelWhiteBox
        {
            get
            {
                var level = new Level(23, 13, new Point(2, 2), "Dirt");
                level.Add(new Demon(8, 8, 10));
                return level;
            }
        }
        public static Level Level1
        {
            get
            {
                var level = new Level(23, 13, new Point(2, 2), "Dirt");
                level.Add(new Item(10, 4, "aid"));
                level.Add(new Item(5, 5, "ammo"));
                level.Add(new Demon(20, 1, 50));
                level.Add(new Demon(8, 8, 50));
                level.Add(new Infernal(7, 7, 60));
                for (var i = 1; i < level.Height; i++)
                {
                    if (i%3 == 0)
                        level.Add(new Wall(4, i, "Dirt"));
                }
                for (var i = 1; i < level.Height; i++)
                {
                    if (i % 3 == 0)
                        level.Add(new Wall(9, i, "Dirt"));
                }
                for (var i = 1; i < level.Height; i++)
                {
                    if (i % 3 == 0)
                        level.Add(new Wall(14, i, "Dirt"));
                }
                for (var i = 1; i < level.Height; i++)
                {
                    if (i % 3 == 0)
                        level.Add(new Wall(19, i, "Dirt"));
                }
                return level;
            }
        }
        public static Level Level2
        {
            get
            {
                var level = new Level(10, 10, new Point(1, 1), "Dirt");
                level.Add(new Item(5, 5, "ammo"));
                level.Add(new Wall(7, 7, "Dirt"));
                level.Add(new Demon(2, 2, 10));
                return level;
            }
        }

        public static Level Level3
        {
            get
            {
                var level = new Level(24, 13, new Point(1, 6), "Dirt");
                level.Add(new Item(1, 1, "ammo"));
                level.Add(new Item(1, 11, "aid"));
                level.Add(new Item(22, 1, "aid"));
                level.Add(new Item(22, 11, "ammo"));
                for (var i = 0; i < 2; i++)
                {
                    level.Add(new Wall(3 + i * 16, 3, "Dirt"));
                    level.Add(new Wall(4 + i * 16, 3, "Dirt"));
                    level.Add(new Wall(3 + i * 16, 4, "Dirt"));
                    level.Add(new Wall(4 + i * 16, 4, "Dirt"));
                }

                for (var i = 0; i < 2; i++)
                {
                    level.Add(new Wall(3 + i * 16, 9, "Dirt"));
                    level.Add(new Wall(4 + i * 16, 9, "Dirt"));
                    level.Add(new Wall(3 + i * 16, 10, "Dirt"));
                    level.Add(new Wall(4 + i * 16, 10, "Dirt"));
                }

                level.Add(new Demon(7, 4, 10));
                level.Add(new Demon(7, 8, 10));
                level.Add(new Demon(16, 4, 10));
                level.Add(new Demon(16, 8, 10));

                level.Add(new Infernal(11, 6, 30));

                return level;
            }
        }

    }
}
