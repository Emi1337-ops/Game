using Game.Model;
using Game.Views;
using System.Windows.Forms;

namespace Game
{
    public class Game
    {
        public int BlockSize = 64; //все объекты (кроме снарядов) имеют одинаковый размер 64*64 пикселя

        public Level Level { get; set; }
        public Player Player { get; set; }
        public MainForm Form { get; set; }

        #region Game
        public Game(Level level, MainForm form)
        {
            Level = level;
            Form = form;
            Player = new Player(
                Level.PlayerStartPosition.X,
                Level.PlayerStartPosition.Y,
                "Right",
                30,
                form);
            Player.Hp = 100;
            Player.Ammo = 30;
 
        }
        #endregion

        #region KeyBoard
        public void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Form.ChangeStage(GameStage.EscapeMenu);
            }
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
            if (e.KeyCode == Keys.Escape)
            {

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
        public void Update(GameView view)
        {
            Level.Update(this, view);
            Player.Move(this);
        }
        #endregion
    }
}
