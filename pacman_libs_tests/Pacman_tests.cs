using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pacman_libs;

namespace pacman_libs_tests
{
    [TestClass]
    public class Pacman_tests
    {
        [TestMethod]
        public void Pacman_has_a_direction()
        {
            var pacman = new Pacman();
            Assert.IsNotNull(pacman.Direction);
        }

        [TestMethod]
        public void Pacman_has_a_position()
        {
            var pacman = new Pacman();
            Assert.IsNotNull(pacman.Position);
        }

        [TestMethod]
        public void Pacman_can_move_to_the_left()
        {
            var pacman = new Pacman();
            var inital_place = pacman.Position;
            pacman.SetDirection(new Left());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.Y == inital_place.Y + 1);
        }

        [TestMethod]
        public void Pacman_can_move_to_the_right()
        {
            var pacman = new Pacman();
            var inital_place = pacman.Position;
            pacman.SetDirection(new Right());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.Y == inital_place.Y - 1);
        }

        [TestMethod]
        public void Pacman_can_move_upward()
        {
            var pacman = new Pacman();
            var inital_place = pacman.Position;
            pacman.SetDirection(new Up());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.X == inital_place.X + 1);
        }

        [TestMethod]
        public void Pacman_can_move_downward()
        {
            var pacman = new Pacman();
            var inital_place = pacman.Position;
            pacman.SetDirection(new Down());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.IsTrue(reached_place.X == inital_place.X - 1);
        }

        [TestMethod]
        public void Pacman_can_stand_still()
        {
            var pacman = new Pacman();
            var inital_place = pacman.Position;
            pacman.SetDirection(new StandStill());
            pacman.Move();
            var reached_place = pacman.Position;
            Assert.AreEqual(reached_place, inital_place);
        }
    }
}
