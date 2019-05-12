using board_libs.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using utils_libs.Abstractions;
using utils_libs.Tools;

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
        /// Represents the maze structure
        /// </summary>
        public List<IBlock> Maze { get; private set; }

        /// <summary>
        /// Represents the farest corner position 
        /// in order to know X max and Y max of the 
        /// board admiting X min and Y min are 0,0
        /// </summary>
        public Position Limits { get; private set; }

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
            DotsLeft = Grid.Sum(l => l.Sum(c => c.Equals('·') || c.Equals('.') ? 1 : 0));

            Maze = new List<IBlock>();
            int top = 0;
            int left = 0;
            foreach (List<char> line in Grid)
            {
                left = 0;
                foreach (char c in line)
                {
                    Maze.Add(Placeblock(new Position { X = top, Y = left }, 20, c));
                    left += 20;
                }
                top += 20;
            }
            Limits = new Position { X = top, Y = left };
        }

        /// <summary>
        /// Place an Area on the Maze
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="letter"></param>
        /// <returns></returns>
        private IBlock Placeblock(IPosition position, int size, char letter)
        {
            switch (letter)
            {
                //case '╔': return Area.CreateNew(position, size, false, letter);
                //case '╗': return Area.CreateNew(position, size, false, letter);
                //case '╝': return Area.CreateNew(position, size, false, letter);
                //case '╚': return Area.CreateNew(position, size, false, letter);
                //case '#': return Area.CreateNew(position, size, false, letter);
                //case '║': return Area.CreateNew(position, size, false, letter);
                //case '═': return Area.CreateNew(position, size, false, letter);
                case 'c': return Area.CreateNew(position, size, false, letter);
                case '·': return Area.CreateNew(position, size, false, letter);
                case ' ': return Area.CreateNew(position, size, false, letter);
                default: return Area.CreateNew(position, size, true, letter);
            }
        }

        /// <summary>
        /// Check if the next step is possible depending on the direction taken
        /// </summary>
        /// <param name="p">the player</param>
        /// <param name="key">the pressed key</param>
        /// <returns>a boolean</returns>
        public bool CheckLimits(IPlayer p, Key key)
        {
            if ((p.Position.X < 0 && key.Equals(DirectionType.Up.Key))
            || (p.Position.X > Limits.X && key.Equals(DirectionType.Down.Key))
            || (p.Position.Y < 0 && key.Equals(DirectionType.Left.Key))
            || (p.Position.Y > Limits.Y && key.Equals(DirectionType.Right.Key)))
            {
                return false;
            }
            return !Maze.Exists(x => x.WillCollide(p));
        }


        /// <summary>
        /// When reaching an edge, teleport to the other side
        /// </summary>
        /// <param name="p"></param>
        public void DoesWarp(IPlayer p)
        {
            if (p.Position.Y > Limits.Y)
            {
                p.SetPosition(p.Position.X, 0);
            }
            if (p.Position.Y < 0)
            {
                p.SetPosition(p.Position.X, Limits.Y);
            }
        }
    }
}
