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
        /// Manage key pressed events
        /// </summary>
        /// <param name="p"></param>
        /// <param name="key"></param>
        public void KeyPressedEvents(IPlayer p, Key key)
        {
            if (!DirectionType.ExistsWhitin(key)) return;
            SetDirection(p, DirectionType.ToDirection(key));
        }

        /// <summary>
        /// First keyboard event that lauch the game
        /// </summary>
        /// <param name="p"></param>
        /// <param name="key"></param>
        public void FirstEntry(IPlayer p, Key key)
        {
            if ((DirectionType.ToDirection(key).Equals(DirectionType.Left.Direction)
                || DirectionType.ToDirection(key).Equals(DirectionType.Right.Direction)))
            {
                SetDirection(p, DirectionType.ToDirection(key));
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
            foreach (List<char> line in Grid)
            {
                var listOfArea = new List<IBlock>();
                left = 0;
                foreach (char c in line)
                {
                    if (c.Equals('·')) DotsLeft++;
                    listOfArea.Add(Placeblock(new Position { X = top, Y = left }, 20, c));
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
        private Area Placeblock(IPosition position, int size, char letter)
        {
            switch (letter)
            {
                case 'c': return Area.CreateNew(position, size, false, letter);
                case '·': return Area.CreateNew(position, size, false, letter);
                case ' ': return Area.CreateNew(position, size, false, letter);
                default: return Area.CreateNew(position, size, true, letter);
            }
        }

        /// <summary>
        /// Set the direction to the player
        /// </summary>
        /// <param name="direction">the wanted </param>
        public void SetDirection(IPlayer p, IDirection direction)
        {
            p.SetDirection(Maze, direction);
        }

        /// <summary>
        /// Retry to SetDirection if failed before and move
        /// </summary>
        /// <param name="p"></param>
        public void ComputeMoves(IPlayer p)
        {
            CheckLimitsAndMove(p, DirectionType.ToKey(p.Direction));
        }

        /// <summary>
        /// Check if the next step is possible depending on the direction taken
        /// </summary>
        /// <param name="p">the player</param>
        /// <param name="key">the pressed key</param>
        /// <returns>a boolean</returns>
        public void CheckLimitsAndMove(IPlayer p, Key key)
        {
            if (
             !((p.Position.X < 0 && key.Equals(DirectionType.Up.Key))
            || (p.Position.X > Limits.X && key.Equals(DirectionType.Down.Key))
            || (p.Position.Y < 0 && key.Equals(DirectionType.Left.Key))
            || (p.Position.Y > Limits.Y && key.Equals(DirectionType.Right.Key)))
            )
            {
                p.Move(Maze);
                DoesWarp(p);
            }
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
