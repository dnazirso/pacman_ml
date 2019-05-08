using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace board_libs
{
    public class Board
    {
        /// <summary>
        /// Dots in the maze
        /// </summary>
        public int DotsLeft { get; set; }

        /// <summary>
        /// Represents the maze structure
        /// </summary>
        public List<List<char>> Grid { get; private set; }

        /// <summary>
        /// Constructor that check if the given path exists
        /// </summary>
        /// <param name="pathToFile"></param>
        public Board(string pathToFile)
        {
            if (File.Exists(pathToFile))
            {
                CreateBoard(pathToFile);
            }
        }

        /// <summary>
        /// Read a file in order to retreive a maze structure
        /// </summary>
        /// <param name="pathToFile"></param>
        private void CreateBoard(string pathToFile)
        {
            Grid = File.ReadLines(pathToFile).Select(l => l.ToCharArray().ToList()).ToList();
        }
    }
}
