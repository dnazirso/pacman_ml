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
        public int DotsLeft { get; private set; }

        /// <summary>
        /// Represents the maze structure
        /// </summary>
        public List<List<char>> Grid { get; private set; }

        /// <summary>
        /// Represents the maze structure
        /// </summary>
        public List<List<IBlock>> Maze { get; private set; }

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

            Maze = new List<List<IBlock>>();
            int top = 0;
            int left = 0;
            foreach (var line in Grid.Select((value, i) => new { value, i }))
            {
                var listOfArea = new List<IBlock>();
                left = 0;
                foreach (var c in line.value.Select((value, i) => new { value, i }))
                {
                    if (c.value.Equals('·')) DotsLeft++;
                    var block = Placeblock(new Position { X = top, Y = left }, new Position { X = line.i, Y = c.i }, 20, c.value);
                    listOfArea.Add(block);
                    left += 20;
                }
                Maze.Add(listOfArea);
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
        private Area Placeblock(IPosition position, IPosition coord, int size, char letter)
        {
            switch (letter)
            {
                case 'c': return Area.CreateNew(position, coord, size, false, letter);
                case 'b': return Area.CreateNew(position, coord, size, false, letter);
                case '·': return Area.CreateNew(position, coord, size, false, letter);
                case '•': return Area.CreateNew(position, coord, size, false, letter);
                case ' ': return Area.CreateNew(position, coord, size, false, letter);
                default: return Area.CreateNew(position, coord, size, true, letter);
            }
        }

        /// <summary>
        /// Manage key pressed events
        /// </summary>
        /// <param name="p"></param>
        /// <param name="key"></param>
        public void KeyPressedEvents(IPlayer p, Key key)
        {
            if (!DirectionType.ExistsWhitin(key)) return;
            p.TrySetDirection(DirectionType.ToDirection(key));
        }

        /// <summary>
        /// Retry to SetDirection if failed before and move
        /// </summary>
        /// <param name="p">the player</param>
        public void ComputeMoves(IPlayer p)
        {
            p.Move();
            DoesWarp(p);
        }

        /// <summary>
        /// When reaching an edge, teleport to the other side
        /// </summary>
        /// <param name="p">the player</param>
        private void DoesWarp(IPlayer p)
        {
            if (p.Position.Y > Limits.Y)
            {
                p.SetPosition(p.Position.X, 0);
            }
            if (p.Position.Y < 0)
            {
                p.SetPosition(p.Position.X, Limits.Y);
            }
            if (p.Position.X > Limits.X)
            {
                p.SetPosition(0, p.Position.Y);
            }
            if (p.Position.X < 0)
            {
                p.SetPosition(Limits.X, p.Position.Y);
            }
        }
    }
}
