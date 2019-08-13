using Microsoft.VisualStudio.TestTools.UnitTesting;
using utils_libs.Directions;
using utils_libs.Models;

namespace pacman_libs_tests
{
    [TestClass]
    public class Pacman_tests : Pacman_tests_base
    {
        [TestMethod]
        public void Pacman_has_a_direction()
        {
            Assert.IsNotNull(pacman.Direction);
        }

        [TestMethod]
        public void Pacman_has_a_position()
        {
            Assert.IsNotNull(pacman.Position);
        }

        [TestMethod]
        public void Pacman_can_move_leftward()
        {
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new Left());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.Y == inital_place.Y - 1);
        }

        [TestMethod]
        public void Pacman_can_move_rightward()
        {
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new Right());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.Y == inital_place.Y + 1);
        }

        [TestMethod]
        public void Pacman_can_move_upward()
        {
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new Up());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.X == inital_place.X - 1);
        }

        [TestMethod]
        public void Pacman_can_move_downward()
        {
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new Down());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.X == inital_place.X + 1);
        }

        [TestMethod]
        public void Pacman_can_stand_still()
        {
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new StandStill());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.AreEqual(reached_place, inital_place);
        }
    }
}
