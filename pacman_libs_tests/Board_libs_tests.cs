using System;
using board_libs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pacman_libs_tests
{
    [TestClass]
    public class Board_libs_tests
    {
        [TestMethod]
        public void board_is_loaded_from_file()
        {
            var board = new Board("");
            Assert.IsTrue(board.DotsLeft > 8);
        }

        [TestMethod]
        public void board_has_a_start_point_for_Pacman()
        {
            var board = new Board("");
            var start = board.Grid.Exists(l => l.Exists(c => c == 0));
            Assert.IsTrue(start);
        }
    }
}
