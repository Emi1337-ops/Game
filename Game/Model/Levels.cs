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
        public List<Level> LevelsList = new List<Level>()
        {
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
            Level6,
            Level7,
            Level8,
            Level9,
            Level10,
        };
        
        public static Level LevelWhiteBox
        {
            get
            {
                var level = new Level(16, 8, new Point(2, 2), new Point(3,3), "Dirt");
                return level;
            }
        }

        public static Level Level1
        {
            get
            {
                var level = new Level(24, 14, new Point(1, 6), new Point(22, 6), "Dirt");

                return level;
            }
        }
        public static Level Level2
        {
            get
            {
                var level = new Level(24, 14, new Point(1, 6), new Point(22, 6), "Dirt");
                level.Add(new Demon(21, 6, 10));
                level.Add(new Item(18, 5, "ammo"));
                level.Add(new Item(18, 7, "aid"));

                return level;
            }
        }
        public static Level Level3
        {
            get
            {
                var level = new Level(24, 14, new Point(1, 11), new Point(21, 1), "Dirt");
                level.Add(new Item(10, 4, "aid"));
                level.Add(new Item(5, 5, "ammo"));
                level.Add(new Demon(20, 1, 50));
                level.Add(new Demon(8, 8, 50));
                level.Add(new Demon(3, 3, 50));
                for (var i = 1; i < level.Width; i++)
                {
                    if (i % 3 == 0)
                        level.Add(new Wall(i, 8, "Dirt"));
                }
                return level;
            }
        }
        public static Level Level4
        {
            get
            {
                var level = new Level(24, 14, new Point(10, 1), new Point(12, 1), "Dirt");
                level.Add(new Wall(3, 4, "Dirt"));
                level.Add(new Wall(4, 4, "Dirt"));
                level.Add(new Wall(5, 4, "Dirt"));
                level.Add(new Wall(5, 5, "Dirt"));
                level.Add(new Wall(5, 6, "Dirt"));

                level.Add(new Wall(18, 4, "Dirt"));
                level.Add(new Wall(19, 4, "Dirt"));
                level.Add(new Wall(20, 4, "Dirt"));
                level.Add(new Wall(18, 5, "Dirt"));
                level.Add(new Wall(18, 6, "Dirt"));

                for (var i = 0; i < 7; i++)
                {
                    level.Add(new Wall(8+i, 9, "Dirt"));
                }

                level.Add(new Demon(3, 6, 40));
                level.Add(new Demon(20, 6, 40));
                level.Add(new Demon(10, 10, 40));
                level.Add(new Demon(14, 10, 40));

                level.Add(new Item(4, 1, "ammo"));
                level.Add(new Item(18, 1, "ammo"));
                level.Add(new Item(19, 11, "aid"));
                level.Add(new Item(21, 11, "aid"));

                return level;
            }
        }

        public static Level Level5
        {
            get
            {
                var level = new Level(24, 14, new Point(1, 6), new Point(21, 2), "Dirt");
                for (var i = 0; i < 6; i++)
                {
                    level.Add(new Wall(9 + i, 3, "Dirt"));
                }
                for (var i = 0; i < 6; i++)
                {
                    level.Add(new Wall(9 + i, 10, "Dirt"));
                }
                for (var i = 0; i < 3; i++)
                {
                    level.Add(new Wall(3, 5+i, "Dirt"));
                }
                for (var i = 0; i < 3; i++)
                {
                    level.Add(new Wall(20, 5+i, "Dirt"));
                }

                level.Add(new Infernal(11, 6, 50));

                level.Add(new Item(11, 1, "ammo"));
                level.Add(new Item(11, 12, "ammo"));
                level.Add(new Item(14, 12, "aid"));
                level.Add(new Item(22, 6, "aid"));

                return level;
            }
        }

        public static Level Level6
        {
            get
            {
                var level = new Level(24, 14, new Point(1, 12), new Point(22, 1), "Dirt");
                for (var i = 0; i < 16; i++)
                {
                    if (i - 7 != 0 && i - 6 != 0 && i - 8 != 0)
                        level.Add(new Wall(4 + i, 2, "Dirt"));
                }
                for (var i = 0; i < 16; i++)
                {
                    if (i - 7 != 0 && i - 6 != 0 && i - 8 != 0)
                        level.Add(new Wall(4 + i, 11, "Dirt"));
                }
                for (var i = 0; i < 8; i++)
                {
                    if (i - 3 != 0 && i - 4 != 0)
                        level.Add(new Wall(3, 3 + i, "Dirt"));
                }
                for (var i = 0; i < 8; i++)
                {
                    if (i - 3 != 0 && i - 4 != 0)
                        level.Add(new Wall(20, 3 + i, "Dirt"));
                }
                level.Add(new Wall(9, 4, "Dirt"));

                level.Add(new Wall(13, 4, "Dirt"));

                level.Add(new Wall(9, 8, "Dirt"));

                level.Add(new Wall(13, 8, "Dirt"));

                level.Add(new Infernal(11, 6, 50));

                level.Add(new Item(5, 12, "ammo"));
                level.Add(new Item(1, 10, "ammo"));
                level.Add(new Item(17, 9, "aid"));
                level.Add(new Item(6, 3, "aid"));
                level.Add(new Item(22, 4, "aid"));

                level.Add(new Demon(2, 1, 30));
                level.Add(new Demon(5, 9, 30));
                level.Add(new Demon(18, 3, 30));
                level.Add(new Demon(21, 12, 30));

                return level;
            }
        }



        public static Level Level7
        {
            get
            {
                var level = new Level(15, 13, new Point(1, 1), new Point(10, 10), "Dirt");
                level.Add(new Item(4, 4, "ammo"));
                level.Add(new Item(5, 5, "ammo"));
                return level;
            }
        }

        public static Level Level8
        {
            get
            {
                var level = new Level(10, 10, new Point(1, 1), new Point(8, 8), "Dirt");
                level.Add(new Demon(1, 5, 30));
                level.Add(new Demon(5, 1, 30));
                level.Add(new Item(4,4, "aid"));
                return level;
            }
        }

        public static Level Level9
        {
            get
            {
                var level = new Level(7, 7, new Point(1, 1), new Point(5, 5), "Dirt");
                level.Add(new Demon(1, 5, 30));
                level.Add(new Demon(5, 5, 30));
                level.Add(new Demon(1, 5, 30));
                level.Add(new Item(3, 3, "aid"));
                level.Add(new Item(4, 4, "aid"));
                level.Add(new Item(3, 2, "ammo"));
                return level;
            }
        }

        public static Level Level10
        {
            get
            {
                var level = new Level(24, 14, new Point(10, 6), new Point(11, 7), "Dirt");

                level.Add(new Infernal(10, 2, 50));
                level.Add(new Infernal(12, 10, 50));
                level.Add(new Infernal(20, 5, 50));


                level.Add(new Item(2, 3, "ammo"));
                level.Add(new Item(2, 12, "ammo"));
                level.Add(new Item(20, 3, "ammo"));
                level.Add(new Item(14, 6, "ammo"));
                level.Add(new Item(12, 7, "ammo"));
                level.Add(new Item(20, 11, "aid"));
                level.Add(new Item(2, 2, "aid"));
                level.Add(new Item(2, 11, "aid"));
                level.Add(new Item(20, 2, "aid"));

                return level;
            }
        }

    }
}
