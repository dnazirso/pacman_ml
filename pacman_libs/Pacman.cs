using System.Collections.Generic;
using utils_libs.Abstractions;

namespace pacman_libs
{
    public class Pacman : PlayerBase, IPacman
    {
        public int DotsEaten { get; set; }

        public Pacman()
        {
            Initialize();
        }

        public Pacman(List<List<IBlock>> Maze)
        {
            this.Maze = Maze;
            Initialize();
        }
    }
}
