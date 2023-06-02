using System.Drawing;

namespace Game
{
    public class Wall
    {
        public int X { get; set; }
        public int Y { get; set; }

        public readonly string Type;

        public int Size = 64;

        public Bitmap Image
        { 
            get 
            { 
                return new Bitmap("Images\\BRICK_1A.png");
            }
        }

        public Wall(int x, int y, string type)
        {
            X = x * 64;
            Y = y * 64;
            Type = type;
        }
    }
}
