using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Views
{
    public class FinishGame : Form
    {
        public FinishGame(MainForm form)
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

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
            var Picture = new PictureBox
            {
                Image = new Bitmap("Images\\Finish.png"),
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Center,
                Dock = DockStyle.Fill,
            };

            var table = new TableLayoutPanel();
            var bg = new Bitmap("Images\\FinishBg.jpg");
            table.BackgroundImage = bg;
            table.BackgroundImageLayout = ImageLayout.Stretch;

            table.RowStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    table.Controls.Add(new Control(), i, j);
                }
            }

            table.Controls.Add(Picture, 0, 1);
            table.Controls.Add(ButtonEx, 2, 1);

            table.Dock = DockStyle.Fill;

            

            ButtonEx.Click += (sender, args) => Application.Exit();

            Controls.Add(table);
        }
    }
}
