using Game.Model;
using Game.Views;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Game
{
    public partial class MainForm 
    {
        public int TickCount { get; set; }
        public Levels Levels = new Levels();

        public event Action<GameStage> StageChanged;
        public GameView game = null;
        public Level L = null;
        public GameStage Stage { get; set; }

        public void Run()
        {
            StageChanged += Game_OnStageChanged;
            ShowStartScreen();
        }

        public Level GetLevel()
        {
            if (Levels.LevelsList.Count == 0)
                return null;
            L = Levels.LevelsList.First();
            var level = L;
            Levels.LevelsList.RemoveAt(0);
            return level;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Game_OnStageChanged(GameStage stage)
        {
            switch (stage)
            {
                case GameStage.Game:
                    ShowGame();
                    break;
                case GameStage.EscapeMenu:
                    ShowEscapeMenu();
                    break;
                case GameStage.RestartLevel:
                    RestartLevel();
                    break;
                case GameStage.Death:
                    ShowDeathScreen();
                    break;
                case GameStage.Finished:
                    ShowFinishScreen();
                    break;
                default:
                    ShowStartScreen();
                    break;
            }
        }

        private void RestartLevel()
        {
            game.gameModel.Level = L;
            game.gameModel.Player.Hp = 100;
            game.gameModel.Player.X = game.gameModel.Level.PlayerStartPosition.X * 64;
            game.gameModel.Player.Y = game.gameModel.Level.PlayerStartPosition.Y * 64;
            game.Timer.Start();
        }

        private void ShowFinishScreen()
        {
            game.Timer.Stop(); 
            game.Hide();
            var finish = new FinishGame(this);
            finish.ShowDialog();
        }

        private void ShowDeathScreen()
        {
            game.Timer.Stop();
            game.TopMost = false;
            var deathScreen = new Death(this);
            deathScreen.ShowDialog();
        }

        private void ShowStartScreen()
        {
            var menu = new Menu(this);
            menu.ShowDialog();
        }

        private void ShowGame()
        {
            var level = GetLevel();
            game = new GameView(new Game(level, this));
            game.ShowDialog();
        }


        private void ShowEscapeMenu()
        {
            game.Timer.Stop();
            game.TopMost = false;
            var EscMenu = new EscMenu();
            EscMenu.ShowDialog();
            game.Timer.Start();
        }

        public void ChangeStage(GameStage stage)
        {
            Stage = stage;
            StageChanged(stage);
        }
    }
}
