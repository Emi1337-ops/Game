using System;
using System.Windows.Forms;
using System.Drawing;
using Game.Model;

namespace Game
{
    public class Menu : Form
    {
        public MainForm Form { get; set; }
        public Menu(MainForm form)
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Form = form;

            var ButtonGame = new Button
            {
                Image = new Bitmap("Images\\game.png"),
                FlatStyle = FlatStyle.Flat,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Center,
                Dock = DockStyle.Fill

            };
            var ButtonEx = new Button
            {
                Image = new Bitmap("Images\\exit.png"),
                FlatStyle = FlatStyle.Flat,
                ImageAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Center,
                Dock = DockStyle.Fill,
               
            };

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));

            table.Controls.Add(new Control(), 0, 0);
            table.Controls.Add(ButtonGame, 1, 1);
            table.Controls.Add(new Control(), 1, 2);
            table.Controls.Add(ButtonEx, 1, 3);
            table.Controls.Add(new Control(), 1, 4);
            table.Controls.Add(new Control(), 2, 0);

            table.Dock = DockStyle.Fill;

            var bg = new Bitmap("Images\\MainMenuBg.jpg");
            table.BackgroundImage = bg;
            table.BackgroundImageLayout = ImageLayout.Stretch;


            Controls.Add(table);


            ButtonGame.Click += ButtonGame_Click;
            ButtonEx.Click += ButtonEx_Click;

        }

        private void ButtonGame_Click(object sender, EventArgs e)
        {
            Hide();
            Form.ChangeStage(GameStage.Game);
        }



        private void ButtonEx_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
