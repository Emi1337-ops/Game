using System.Drawing;

namespace Game
{
    public class Item
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Type { get; set; }

        public bool RemoveFlag = false;
        public Bitmap Image
        {
            get 
            { return new Bitmap($"Images\\{Type}.png");

            }
        }

        delegate void Func(Player player);
        private Func Help { get; }

        public Item(int x, int y, string type)
        {
            X = x * 64;
            Y = y * 64;
            Type = type;
            if (type == "aid") Help = Aid;
            if (type == "ammo") Help = Ammo;
            void Aid(Player player) => player.Hp += 15;
            void Ammo(Player player) => player.Ammo += 15;
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
