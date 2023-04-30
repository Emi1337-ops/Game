using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    partial class Form1 : Form
    {
        private readonly Dictionary<string, Bitmap> Images = new Dictionary<string, Bitmap>();

        public Form1(Game Game, DirectoryInfo imagesDirectory = null)
        {
            InitializeComponent();
            KeyPreview = true;
            DoubleBuffered = true;

            var ammoT = new TextBox()
            {
                Location = new Point(64, 32),
                Size = new Size(32, 32),
            };
            Controls.Add(ammoT);
            var hpT = new ProgressBar()
            {
                Location = new Point(128, 32),
                Size = new Size(256, 32),
                BackColor = Color.White,
                ForeColor = Color.LightGreen,
                Maximum = 100,
            };
            Controls.Add(hpT);

            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                Images[e.Name] = (Bitmap)Image.FromFile(e.FullName);

            KeyDown += (s, e) => Game.KeyDown(e);
           
            KeyUp += (s, e) => Game.KeyUp(e);

            #region Paint
            Paint += (sender, args) =>
            {
                ammoT.Text = Game.Player.Ammo.ToString();
                hpT.Value = Game.Player.Hp;

                #region Background
                var wallim = Images["BRICK_1A.png"];
                var groim = Images["DIRT_1A.png"];
                for (var i = 0; i < Game.Level.Width; i++)
                {
                    for (var j = 0; j < Game.Level.Height; j++)
                    {
                        if (i==0 || j==0 || i== Game.Level.Width -1 || j== Game.Level.Height -1)
                            args.Graphics.DrawImage(wallim, i*64, j*64);
                        else 
                            args.Graphics.DrawImage(groim, i * 64, j * 64);
                    }
                }
                #endregion

                #region Player
                var pb = Images["DoomGuyRight.png"];
                if (Game.Player.Direction == "Up") pb = Images["DoomGuyUp.png"];
                if (Game.Player.Direction == "Back") pb = Images["DoomGuyBack.png"];
                if (Game.Player.Direction == "Right") pb = Images["DoomGuyRight.png"];
                if (Game.Player.Direction == "Left") pb = Images["DoomGuyLeft.png"];
                args.Graphics.DrawImage(pb, Game.Player.X, Game.Player.Y);
                #endregion

                #region Supplies
                var aid = Images["aid.png"];
                var ammo = Images["ammo.png"];
                foreach (var item in Game.Level.ItemList)
                {
                    if (item.Type == "aid")
                        args.Graphics.DrawImage(aid, item.X, item.Y);
                    if (item.Type == "ammo")
                        args.Graphics.DrawImage(ammo, item.X, item.Y);
                }
                #endregion

                #region Bullets
                var bulletP = Images["fireball.png"];
                foreach(var bullet in Game.BulletList)
                {
                    args.Graphics.DrawImage(bulletP, bullet.X, bullet.Y);
                }
                #endregion

                #region Monsters
                foreach (var monster in Game.Level.MonsterList)
                {
                    var mb = Images["DemonRight.png"];
                    if (monster.Direction == "Up") mb = Images["DemonUp.png"];
                    if (monster.Direction == "Back") mb = Images["DemonBack.png"];
                    if (monster.Direction == "Right") mb = Images["DemonRight.png"];
                    if (monster.Direction == "Left") mb = Images["DemonLeft.png"];
                    args.Graphics.DrawImage(mb, monster.X, monster.Y);
                }
                #endregion

                args.Graphics.ResetTransform();
            };
            #endregion

            var timer = new Timer();
            timer.Interval = 1;
            timer.Tick += (sender, args) =>
            {
                Game.Move();
                Invalidate();
            };
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
