using System.Drawing;
using System.Windows.Forms;

namespace Game.Views
{
    public class GameView : Form
    {
        public Timer Timer = new Timer();
        public Game gameModel { get; set; }
        public GameView(Game game)
        {
            gameModel = game;
            DoubleBuffered = true;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(gameModel.Level.Width * 64, gameModel.Level.Height * 64);

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

            KeyDown += (s, e) => gameModel.KeyDown(e);
            KeyUp += (s, e) => gameModel.KeyUp(e);

            #region Paint
            Paint += (sender, args) =>
            {
                ammoT.Text = gameModel.Player.Ammo.ToString();
                hpT.Value = gameModel.Player.Hp;
                BackColor = Color.Black;    

                #region Background
                var wallim = new Bitmap("Images\\BRICK_1A.png");
                var groim = new Bitmap("Images\\DIRT_1A.png");
                for (var i = 0; i < gameModel.Level.Width; i++)
                {
                    for (var j = 0; j < gameModel.Level.Height; j++)
                    {
                        if (i == 0 || j == 0 || i == gameModel.Level.Width - 1 || j == gameModel.Level.Height - 1)
                            args.Graphics.DrawImage(wallim, i * 64, j * 64);
                        else
                            args.Graphics.DrawImage(groim, i * 64, j * 64);
                    }
                }
                #endregion

                #region Wall
                foreach (var wall in gameModel.Level.WallList)
                {
                    args.Graphics.DrawImage(wall.Image, wall.X, wall.Y);
                }
                #endregion

                #region ExitDoor
                if (gameModel.Level.ExitDoor != null)
                    args.Graphics.DrawImage(gameModel.Level.ExitDoor.Image, gameModel.Level.ExitDoor.X, gameModel.Level.ExitDoor.Y);
                #endregion

                #region Player
                args.Graphics.DrawImage(gameModel.Player.Image, gameModel.Player.X, gameModel.Player.Y);
                #endregion

                #region Supplies
                foreach (var item in gameModel.Level.ItemList)
                {
                    args.Graphics.DrawImage(item.Image, item.X, item.Y);
                }
                #endregion

                #region Bullets
                foreach (var bullet in gameModel.Level.BulletList)
                {
                    args.Graphics.DrawImage(bullet.Image, bullet.X, bullet.Y);
                }
                #endregion

                #region Demons
                foreach (var demon in gameModel.Level.DemonList)
                {
                    args.Graphics.DrawImage(demon.Image, demon.X, demon.Y);
                }
                #endregion

                #region Infernals
                foreach (var infernal in gameModel.Level.InfernalList)
                {
                    args.Graphics.DrawImage(infernal.Image, infernal.X, infernal.Y);
                }
                #endregion

               

                args.Graphics.ResetTransform();
            };
            #endregion

            Timer.Interval = 15;
            Timer.Tick += (sender, args) =>
            {
                gameModel.Update(this);
                Invalidate();
            };
            Timer.Start();
        }
    }
}
