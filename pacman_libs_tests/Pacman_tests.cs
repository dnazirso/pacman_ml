using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pacman_libs;
using utils_libs.Directions;

namespace pacman_libs_tests
{
    [TestClass]
    public class Pacman_tests
    {
        [TestMethod]
        public void Pacman_has_a_direction()
        {
            var pacman = new Player();
            Assert.IsNotNull(pacman.Direction);
        }

        [TestMethod]
        public void Pacman_has_a_position()
        {
            var pacman = new Player();
            Assert.IsNotNull(pacman.Position);
        }

        [TestMethod]
        public void Pacman_can_move_to_the_left()
        {
            var pacman = new Player();
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new Left());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.Y == inital_place.Y - 1);
        }

        [TestMethod]
        public void Pacman_can_move_to_the_right()
        {
            var pacman = new Player();
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new Right());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.Y == inital_place.Y + 1);
        }

        [TestMethod]
        public void Pacman_can_move_upward()
        {
            var pacman = new Player();
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new Up());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.X == inital_place.X - 1);
        }

        [TestMethod]
        public void Pacman_can_move_downward()
        {
            var pacman = new Player();
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new Down());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.X == inital_place.X + 1);
        }

        [TestMethod]
        public void Pacman_can_stand_still()
        {
            var pacman = new Player();
            var inital_place = new Position { X = pacman.Position.X, Y = pacman.Position.Y };
            pacman.SetDirection(new StandStill());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.AreEqual(reached_place, inital_place);
        }
    }
}
