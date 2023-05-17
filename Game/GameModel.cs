using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Drawing;
using System.Collections.Specialized;
using System.Security.Policy;
using System.Windows;
using Point = System.Drawing.Point;
using System.IO;

namespace Game
{
    public class Game
    {
        public int BlockSize = 64; //все объекты (кроме пуль) имеют одинаковый размер 64*64 пикселя

        public Level Level { get; set; }
        public Player Player { get; set; }

        public int TickCount = 0;

        #region Game
        public Game(Level level)
        {
            Level = level;
            Player = new Player(
                Level.PlayerStartPosition.X,
                Level.PlayerStartPosition.Y,
                "Right",
                30);
        }
        #endregion

        #region KeyBoard
        public void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Player.GoUp = true;
            }
            if (e.KeyCode == Keys.A)
            {
                Player.GoLeft = true;
            }
            if (e.KeyCode == Keys.S)
            {
                Player.GoBack = true;
            }
            if (e.KeyCode == Keys.D)
            {
                Player.GoRight = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                Player.Fire(this);
            }
        }
        public void KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Player.GoUp = false;
            }
            if (e.KeyCode == Keys.A)
            {
                Player.GoLeft = false;
            }
            if (e.KeyCode == Keys.S)
            {
                Player.GoBack = false;
            }
            if (e.KeyCode == Keys.D)
            {
                Player.GoRight = false;
            }
        }
        #endregion

        #region Update
        public void Update()
        {
            TickCount++;
            Level.Update(this);
            Player.Move(this);
        }
        #endregion

        
    }
}
