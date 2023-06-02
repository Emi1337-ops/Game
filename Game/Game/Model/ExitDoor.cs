using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Model
{
    public class ExitDoor
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Bitmap Image
        {
            get { return new Bitmap("Images\\ExitDoor.png"); }
        }

        public ExitDoor(Point position)
        {
            X = position.X * 64;
            Y = position.Y * 64;
        }
    }
}
