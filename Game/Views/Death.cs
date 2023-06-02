using System;
using System.Windows.Forms;
using System.Drawing;
using Game.Model;
using System.Windows.Forms.VisualStyles;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace Game
{
    public class Death : Form
    {
        public MainForm Form;
        public Death(MainForm form)
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            Form = form;

            var ButtonGame = new Button
            {
                Image = new Bitmap("Images\\PlayAgain.png"),
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
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));

            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 2; j++)
                    table.Controls.Add(new Control(), i, j);
            }

            table.Controls.Add(ButtonGame, 0, 1);
            table.Controls.Add(ButtonEx, 2, 1);

            table.Dock = DockStyle.Fill;

            var bg = new Bitmap("Images\\MenuBg.jpg");
            table.BackgroundImage = bg;
            table.BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(table);


            ButtonGame.Click += ButtonGame_Click;
            ButtonEx.Click += ButtonEx_Click;

        }

        private void ButtonGame_Click(object sender, EventArgs e)
        {
            Hide();
            Form.ChangeStage(GameStage.RestartLevel);
        }



        private void ButtonEx_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
