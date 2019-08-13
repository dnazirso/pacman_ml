using board_libs;
using board_libs.Models;
using ghost_libs;
using pacman_libs;
using System.Collections.Generic;
using System.IO;
using utils_libs.Abstractions;

namespace pacman_libs_tests
{
    public abstract class Pacman_tests_base
    {
        public Board board;
        public Pacman pacman;
        public Ghost blinky;
        public Pacman_tests_base()
        {
            board = new Board(Directory.GetCurrentDirectory() + "\\maze_test.txt");
            pacman = new Pacman(board.Maze);

            foreach (List<IBlock> line in board.Maze)
            {
                foreach (Area block in line)
                {
                    if (block.Shape.Equals('c'))
                    {
                        pacman.Coord = block.Coord;
                        pacman.SetPosition(block.Min.X + 10, block.Min.Y + 20);
                    }
                    if (block.Shape.Equals('b'))
                    {
                        blinky.Coord = block.Coord;
                        blinky.SetPosition(block.Min.X + 10, block.Min.Y + 20);
                    }
                }
            }
        }
    }
}
