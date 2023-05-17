using System.Xml.Linq;
using Game;
using System.Drawing;
using System.Windows.Forms;

namespace GameTests
{
    public class Tests
    {
        [TestFixture]
        public class GameTests
        {
            [Test]
            public void Map_ShouldInitializeRight()
            {
                var playerPosition = new Point(3, 3);
                var width = 10;
                var heigth = 10;
                var level = new Level(width, heigth, new Point(3, 3), "Dirt");
                var game = new Game.Game(level);

                Assert.AreEqual(game.Level.Width, width);
                Assert.AreEqual(game.Level.Height, heigth);
                Assert.AreEqual(game.Level.PlayerStartPosition.X, playerPosition.X);
                Assert.AreEqual(game.Level.PlayerStartPosition.Y, playerPosition.Y);
            }

            [Test]
            public void Player_ShouldMoveRight()
            {
                var playerPosition = new Point(3, 3);
                var width = 10;
                var heigth = 10;
                var level = new Level(width, heigth, playerPosition, "Dirt");
                var game = new Game.Game(level);
                game.Player.GoRight = true;
                game.Player.Move(game);
                Assert.AreEqual(game.Player.X, game.Level.PlayerStartPosition.X*64 + 4);
                Assert.AreEqual(game.Player.Y, game.Level.PlayerStartPosition.Y*64);

                game.Player.GoRight = false;
                game.Player.GoUp = true;
                game.Player.Move(game);
                Assert.AreEqual(game.Player.X, game.Level.PlayerStartPosition.X * 64 + 4);
                Assert.AreEqual(game.Player.Y, game.Level.PlayerStartPosition.Y * 64 - 4);
            }

            [Test]
            public void Player_ShouldShootRight()
            {
                var playerPosition = new Point(3, 3);
                var width = 10;
                var heigth = 10;
                var level = new Level(width, heigth, playerPosition, "Dirt");
                var game = new Game.Game(level);
            }

        }
        public class LevelTests
        {
            [Test]
            public void Level_ShouldAddElementsRight()
            {
                var playerPosition = new Point(3, 3);
                var width = 10;
                var heigth = 10;
                var level = new Level(width, heigth, playerPosition, "Dirt");
                level.Add(new Item(1, 1, "ammo"));
                level.Add(new Item(1, 1, "ammo"));
                level.Add(new Demon(4, 4, 10));
                level.Add(new Wall(5, 5, "Dirt"));
                level.Add(new Wall(5, 5, "Dirt"));
                level.Add(new Wall(5, 5, "Dirt"));

                Assert.IsTrue(level.ItemList != null && level.ItemList.Count == 2);
                Assert.IsTrue(level.DemonList != null && level.DemonList.Count == 1);
                Assert.IsTrue(level.WallList != null && level.WallList.Count == 3);
            }
        }
    }
}