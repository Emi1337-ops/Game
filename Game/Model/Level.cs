using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using Game.Model;
using Game.Views;

namespace Game
{
    public class Level
    {
        public List<Demon> DemonList = new List<Demon>();
        public List<Infernal> InfernalList = new List<Infernal>();
        public List<Item> ItemList = new List<Item>();
        public List<Bullet> BulletList = new List<Bullet>();
        public List<Wall> WallList = new List<Wall>();
 
        public List<Demon> DemonRemove = new List<Demon>();
        public List<Infernal> InfernalRemove = new List<Infernal>();
        public List<Item> ItemRemove = new List<Item>();
        public List<Bullet> BulletRemove = new List<Bullet>();
        
        public ExitDoor ExitDoor = null;
        private Point ExitPosition { get; }

        public int Width { get; }
        public int Height { get; }
        public Point PlayerStartPosition { get; }

        public Bitmap BackgroundImage
        { 
            get { return new Bitmap($"Images\\{BackgroundType}.jpg"); }
        }
        public string BackgroundType { get; set; }

        public Level(int width, int height, Point position, Point endPosition, string backgroundType)
        {
            Width = width;
            Height = height;
            PlayerStartPosition = position;
            BackgroundType = backgroundType;
            ExitPosition = endPosition;
        }

        #region Add
        public void Add(Demon monster)
        {
            DemonList.Add(monster);
        }
        public void Add(Infernal infernal)
        {
            InfernalList.Add(infernal);
        }
        public void Add(Item item)
        {
            ItemList.Add(item);
        }
        public void Add(Bullet bullet)
        {
            BulletList.Add(bullet);
        }
        public void Add(Wall wall)
        {
            WallList.Add(wall);
        }
        #endregion

        #region UpdateBullets
        public void UpdateBullets(Game game)
        {
            if (BulletList != null)
            {
                foreach (Bullet bullet in BulletList)
                {
                    if (bullet.RemoveFlag) BulletRemove.Add(bullet);
                    bullet.Move(game);
                }
            }
            
            if (BulletRemove != null)
            {
                foreach (var bullet in BulletRemove)
                {
                    if (bullet.parent != null)
                    {
                        if (bullet.Direction == "Up")
                            bullet.parent.FireSecond(game, bullet.X, bullet.Y + bullet.Image.Height/2, "X");
                        else if (bullet.Direction == "Back")
                            bullet.parent.FireSecond(game, bullet.X, bullet.Y - bullet.Image.Height/2, "X");
                        else if (bullet.Direction == "Right")
                            bullet.parent.FireSecond(game, bullet.X - bullet.Image.Width/2, bullet.Y, "Y");
                        else
                            bullet.parent.FireSecond(game, bullet.X + bullet.Image.Width/2, bullet.Y, "Y");
                    }
                    BulletList.Remove(bullet);
                }
                BulletRemove.Clear();
            }
        }
        #endregion

        #region UpdateItems
        public void UpdateItems(Game game)
        {
            if(ItemList!=null)
            {
                foreach (var item in ItemList)
                {
                    item.Update(game);
                    if (item.RemoveFlag) ItemRemove.Add(item);
                }
            }

            if (ItemRemove != null)
            {
                foreach (var item in ItemRemove)
                {
                    ItemList.Remove(item);
                };
                ItemRemove.Clear();
            }
        }
        #endregion

        #region UpdateMonsters
        public void UpdateDemons(Game game)
        {
            if (DemonList != null)
            {
                foreach (var monster in DemonList)
                {
                    monster.Act(game);
                    if (monster.RemoveFlag == true) DemonRemove.Add(monster);
                }
            }
            
            if (DemonRemove != null)
            {
                foreach (var monster in DemonRemove)
                {
                    DemonList.Remove(monster);
                };
                DemonRemove.Clear();
            }
        }
        #endregion

        #region UpdateInfernals
        public void UpdateInfernals(Game game)
        {
            if (InfernalList != null)
            {
                foreach (var infernal in InfernalList)
                {
                    infernal.Act(game);
                    if (infernal.RemoveFlag == true) InfernalRemove.Add(infernal);
                }
            }

            if (InfernalRemove != null)
            {
                foreach (var infernal in InfernalRemove)
                {
                    InfernalList.Remove(infernal);
                };
                InfernalRemove.Clear();
            }
        }
        #endregion

        #region UpdateExit
        public void UpdateExit(Game game, GameView view)
        {
            if (DemonList.Count == 0 && InfernalList.Count == 0)
                ExitDoor = new ExitDoor(ExitPosition);
            if (ExitDoor != null)
            {
                var recD = new Rectangle(ExitDoor.X, ExitDoor.Y, ExitDoor.Image.Width, ExitDoor.Image.Height);
                var player = game.Player;
                var recP = new Rectangle(player.X, player.Y, player.Image.Width, player.Image.Height);
                if (recD.IntersectsWith(recP))
                {
                    if (game.Form.Levels.LevelsList.Count == 0)
                        game.Form.ChangeStage(GameStage.Finished);
                    else
                    {
                        game.Level = game.Form.GetLevel();
                        game.Player.X = game.Level.PlayerStartPosition.X * 64;
                        game.Player.Y = game.Level.PlayerStartPosition.Y * 64;
                    }
                }
            }
        }
        #endregion

        #region UpdateDeath
        public void UpdateDeath(Game game, GameView view)
        {
            if (game.Player.Hp <= 0)
                game.Form.ChangeStage(GameStage.Death);
        }
        #endregion

        public void Update(Game game, GameView view)
        {
            UpdateExit(game, view);
            UpdateDeath(game, view);
            UpdateBullets(game);
            UpdateItems(game);
            UpdateDemons(game);
            UpdateInfernals(game);
        }
    }
}
