using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Game
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var model = new Game(new Level(20, 10, new Point(128, 320)));
            model.Level.Add(new Item(128, 128, "aid"));
            model.Level.Add(new Item(256, 256, "ammo"));
            model.Level.Add(new Monster(640, 320, "Left"));
            Application.Run(new Form1(model) { ClientSize = new Size(model.Level.Width*64, model.Level.Height * 64) });
        }
    }
}
