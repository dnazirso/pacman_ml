using System;
using System.Collections.Generic;
using System.Linq;
using utils_libs.Abstractions;
using utils_libs.Models;
using utils_libs.Tools;

namespace ghost_libs
{
    internal class GhostAI
    {
        private Ghost Self { get; }
        private IPlayer target { get; }
        private List<List<SelfCoords>> FromHere { get; set; }
        private List<List<char>> Grid { get; }

        internal GhostAI(Ghost Self, IPlayer target, List<List<char>> Grid)
        {
            this.Self = Self;
            this.target = target;
            this.Grid = Grid;

            ComputeDirectionForEachTiles();
        }

        public IDirection RandomDirection()
        {
            IDirection direction;

            Random rand = new Random();

            var n = rand.Next(1, 4);
            switch (n)
            {
                case 1: direction = DirectionType.Up.Direction; break;
                case 2: direction = DirectionType.Down.Direction; break;
                case 3: direction = DirectionType.Right.Direction; break;
                case 4: direction = DirectionType.Left.Direction; break;
                default: return DirectionType.StandStill.Direction;
            }

            return direction;
        }

        public IDirection GetDirectionSolution()
        {
            IDirection solution = FromHere[Self.Coord.X][Self.Coord.Y].ToTarget[target.Coord.X][target.Coord.Y];
            return solution;
        }
        private void ComputeDirectionForEachTiles()
        {
            var solutions = new List<List<SelfCoords>>();

            int left = 0;
            int top = 0;

            foreach (List<char> line in Grid)
            {
                List<SelfCoords> listSolutions = new List<SelfCoords>();

                foreach (var col in line)
                {
                    Position g = new Position { X = top, Y = left };
                    listSolutions.Add(new SelfCoords { ToTarget = ComputeAllTargetPostions(g) });
                    left++;
                }

                solutions.Add(listSolutions);
                top++;
                left = 0;
            }

            this.FromHere = solutions;
        }

        private List<List<IDirection>> ComputeAllTargetPostions(Position g)
        {
            var solutions = new List<List<IDirection>>();

            int left = 0;
            int top = 0;

            foreach (List<char> line in Grid)
            {

                List<IDirection> listDirections = new List<IDirection>();

                foreach (var col in line)
                {
                    Position p = new Position { X = top, Y = left };
                    listDirections.Add(ComputeDirection(g, p));
                    left++;
                }

                solutions.Add(listDirections);
                top++;
                left = 0;
            }

            return solutions;
        }

        /// <summary>
        /// Search a solution regarding target and self positions
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private IDirection ComputeDirection(Position g, Position p)
        {
            if (IsComputablePath(Grid[p.X][p.Y]) == false || IsComputablePath(Grid[g.X][g.Y]) == false)
            {
                return DirectionType.StandStill.Direction;
            }

            List<DirectionSolution> possibilities = CheckPossibilities(g, p);
            List<DirectionSolution> orderdSolutions = possibilities.OrderBy(possibility => possibility.distance).ToList();

            return orderdSolutions[orderdSolutions.Count / 2].Solution;
        }

        /// <summary>
        /// Check if a path is computable comparing shapes
        /// </summary>
        /// <param name="Shape"></param>
        /// <returns></returns>
        private bool IsComputablePath(char Shape)
        {
            switch (Shape)
            {
                case '╔':
                case '╗':
                case '╝':
                case '╚':
                case '#':
                case '║':
                case '═':
                case '-': return false;

                default: return true;
            }
        }

        /// <summary>
        /// Sort possible Solutions
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private List<DirectionSolution> CheckPossibilities(Position g, Position p)
        {
            List<DirectionSolution> directions = new List<DirectionSolution>();

            if ((g.X + 1 < Grid.Count) && IsComputablePath(Grid[g.X + 1][g.Y]))
            {
                DirectionSolution solution = new DirectionSolution { distance = ComputeDistance(g.X + 1, g.Y, p.X, p.Y), Solution = DirectionType.Up.Direction };
                directions.Add(solution);
            }

            if ((g.Y + 1 < Grid[g.X].Count) && IsComputablePath(Grid[g.X][g.Y + 1]))
            {
                DirectionSolution solution = new DirectionSolution { distance = ComputeDistance(g.X, g.Y + 1, p.X, p.Y), Solution = DirectionType.Right.Direction };
                directions.Add(solution);
            }

            if (g.X != 0 && IsComputablePath(Grid[g.X - 1][g.Y]))
            {
                DirectionSolution solution = new DirectionSolution { distance = ComputeDistance(g.X - 1, g.Y, p.X, p.Y), Solution = DirectionType.Down.Direction };
                directions.Add(solution);
            }

            if (g.Y != 0 && IsComputablePath(Grid[g.X][g.Y - 1]))
            {
                DirectionSolution solution = new DirectionSolution { distance = ComputeDistance(g.X, g.Y - 1, p.X, p.Y), Solution = DirectionType.Left.Direction };
                directions.Add(solution);
            }

            return directions;
        }

        private int ComputeDistance(int xa, int ya, int xb, int yb)
        {
            int distance = (xb - xa) * (xb - xa) + (yb - ya) * (yb - ya);
            return distance;
        }
    }
}
