using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    partial class View : Form
    {
        public View(Game game)
        {
            InitializeComponent();
            KeyPreview = true;
            DoubleBuffered = true;

            var ammoT = new TextBox()
            {
                Location = new Point(64, 16),
                Size = new Size(32, 32),
                Font = new Font("Times New Roman", 15),
                Enabled = false,
            };
            var hpT = new ProgressBar()
            {
                Location = new Point(128, 16),
                Size = new Size(256, 32),
                BackColor = Color.Brown,
                ForeColor = Color.LightGreen,
                Maximum = 100,
                Enabled = false,
            };
            Controls.Add(ammoT);
            Controls.Add(hpT);

            KeyDown += (s, e) => game.KeyDown(e);
            KeyUp += (s, e) => game.KeyUp(e);

            #region Paint
            Paint += (sender, args) =>
            {
                ammoT.Text = game.Player.Ammo.ToString();
                hpT.Value = game.Player.Hp;

                #region Background
                var wallim = new Bitmap("Images\\BRICK_1A.png");
                var groim = new Bitmap("Images\\DIRT_1A.png");
                for (var i = 0; i < game.Level.Width; i++)
                {
                    for (var j = 0; j < game.Level.Height; j++)
                    {
                        if (i==0 || j==0 || i== game.Level.Width -1 || j== game.Level.Height -1)
                            args.Graphics.DrawImage(wallim, i*64, j*64);
                        else 
                            args.Graphics.DrawImage(groim, i * 64, j * 64);
                    }
                }
                #endregion

                #region Wall
                foreach(var wall in game.Level.WallList)
                {
                    args.Graphics.DrawImage(wall.Image, wall.X, wall.Y);
                }
                #endregion

                #region Player
                args.Graphics.DrawImage(game.Player.Image, game.Player.X, game.Player.Y);
                #endregion

                #region Supplies
                foreach (var item in game.Level.ItemList)
                {
                    args.Graphics.DrawImage(item.Image, item.X, item.Y);
                }
                #endregion
                
                #region Bullets
                foreach(var bullet in game.Level.BulletList)
                {
                    args.Graphics.DrawImage(bullet.Image, bullet.X, bullet.Y);
                }
                #endregion

                #region Monsters
                foreach (var demon in game.Level.DemonList)
                {
                    args.Graphics.DrawImage(demon.Image, demon.X, demon.Y);
                }
                #endregion

                #region Infernals
                foreach (var infernal in game.Level.InfernalList)
                {
                    args.Graphics.DrawImage(infernal.Image, infernal.X, infernal.Y);
                }
                #endregion

                args.Graphics.ResetTransform();
            };
            #endregion

            var timer = new Timer();
            timer.Interval = 10;
            timer.Tick += (sender, args) =>
            {
                game.Update();
                Invalidate();
            };
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
