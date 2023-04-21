using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{

    partial class Form1 : Form
    {
        public Form1()
        {
            KeyPreview = true;
            DoubleBuffered = true;
            var upCount = 0;
            var backCount = 0;
            var rightCount = 0;
            var leftCount = 0;

            ClientSize = new Size(1280, 640);
            var Width = ClientSize.Width / 64;
            var Height = ClientSize.Height / 64; 
            var X = 32;
            var Y = 32;
            var speedTop = 0;
            var speedBottom = 0;
            String side = "Forward";

            var timerLEFT = new Timer();
            timerLEFT.Interval = 10;
            var timerRIGHT = new Timer();
            timerRIGHT.Interval = 10;
            var timerBACK = new Timer();
            timerBACK.Interval = 10;
            var timerUP = new Timer();
            timerUP.Interval = 10;

            #region KeyDown
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.W)
                {
                    speedTop -= 1;
                    side = "Back";
                }
                if (e.KeyCode == Keys.A )
                {
                    speedBottom -= 1;
                    side = "Left";
                }
                if (e.KeyCode == Keys.S)
                {
                    speedTop += 1;
                    side = "Forward";
                }    
                if (e.KeyCode == Keys.D)
                {
                    speedBottom += 1;
                    side = "timerRIGHT";
                }

                if (speedBottom == 1)
                {
                    if (rightCount == 0)
                    {
                        rightCount++;
                        timerRIGHT.Tick += (sender, args) =>
                        {
                            if (X < Width * 64 - 128)
                                X += 5;
                            Invalidate();
                        };
                    }
                    timerRIGHT.Start();
                }
                if (speedBottom == -1)
                {
                    if (leftCount == 0)
                    {
                        leftCount++;
                        timerLEFT.Tick += (sender, args) =>
                        {
                            if (X > 64)
                                X -= 5;
                            Invalidate();
                        };
                    }
                    timerLEFT.Start();
                }
                if (speedTop == 1)
                {
                    if (backCount == 0)
                    {
                        backCount++;
                        timerBACK.Tick += (sender, args) =>
                        {
                            if (Y < Height * 64 - 128)
                                Y += 5;
                            Invalidate();
                        };
                    }
                    timerBACK.Start();
                }
                if (speedTop == -1)
                {
                    if (upCount == 0)
                    {
                        upCount++;
                        timerUP.Tick += (sender, args) =>
                        {
                            if (Y > 64)
                                Y -= 5;
                            Invalidate();
                        };
                    }
                    timerUP.Start();
                }

                Invalidate();
            };
            #endregion

            #region KeyUP
            KeyUp += (s, e) =>
            {
                if (e.KeyCode == Keys.W)
                {
                    speedTop = 0;
                    timerUP.Stop();
                    timerUP.Dispose();
                }
                if (e.KeyCode == Keys.A)
                {
                    speedBottom = 0;
                    timerLEFT.Stop();
                    timerLEFT.Dispose();
                }
                if (e.KeyCode == Keys.S)
                {
                    speedTop = 0;
                    timerBACK.Stop();
                    timerBACK.Dispose();
                }
                if (e.KeyCode == Keys.D)
                {
                    speedBottom = 0;
                    timerRIGHT.Stop();
                    timerRIGHT.Dispose();
                }
            };
            #endregion

            #region Paint
            Paint += (sender, args) =>
            {
                var wallim = new PictureBox();
                wallim.Image = new Bitmap("D:\\ProjectGame\\retro-texture-pack-v9\\png\\BRICK_1A.png");
                var groim = new PictureBox();
                groim.Image = new Bitmap("D:\\ProjectGame\\retro-texture-pack-v9\\png\\DIRT_1A.png");
                for (var i = 0; i < Width; i++)
                {
                    for (var j = 0; j < Height; j++)
                    {
                        if (i==0 || j==0 || i== Width-1 || j==Height-1) args.Graphics.DrawImage(wallim.Image, i*64, j*64);
                        else args.Graphics.DrawImage(groim.Image, i * 64, j * 64);
                    }
                }

                var pb = new PictureBox();
                if (side == "Back") pb.Image = new Bitmap("D:\\ProjectGame\\DoomGuyBack.png");
                if (side == "Forward") pb.Image = new Bitmap("D:\\ProjectGame\\DoomGuy1.png");
                if (side == "timerRIGHT") pb.Image = new Bitmap("D:\\ProjectGame\\DoomGuyRight.png");
                if (side == "Left") pb.Image = new Bitmap("D:\\ProjectGame\\DoomGuyLeft.png");
                args.Graphics.DrawImage(pb.Image, X, Y);
                args.Graphics.ResetTransform();
            };
            #endregion

            InitializeComponent();

        }
    }
}
