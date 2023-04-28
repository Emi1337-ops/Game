using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class Item
    {
        public int X;
        public int Y;
        public string Type;

        public bool RemoveFlag = false;

        delegate void Func(Player player);
        private Func Help;

        public Item(int x, int y, string type)
        {
            X = x;
            Y = y;
            Type = type;
            if (type == "aid") Help = Aid;
            if (type == "ammo") Help = Ammo;
            void Aid(Player player) => player.Hp += 10;
            void Ammo(Player player) => player.Ammo += 10;
        }

        public void Action(Player player)
        {
            Help(player);
            RemoveFlag = true;
        }

        public void Update(Game game)
        {
            var rectIt = new Rectangle(X, Y, game.BlockSize, game.BlockSize);
            var rectPl = new Rectangle(game.Player.X, game.Player.Y, game.BlockSize, game.BlockSize);
            if (rectIt.IntersectsWith(rectPl))
                Action(game.Player);
        }
    }
}
