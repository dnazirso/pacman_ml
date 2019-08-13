using System;
using System.Collections.Generic;
using utils_libs.Abstractions;
using utils_libs.Tools;

namespace ghost_libs
{
    internal class GhostAI
    {
        private IDirection Direction { get; }
        private Ghost Self { get; }
        private IPlayer target { get; }
        private List<List<char>> grid { get; }
        public List<List<IBlock>> Maze { get; }
        internal List<List<DirectionSolution>> Solutions { get; private set; }

        internal GhostAI(Ghost Self, IDirection Direction, IPlayer target, List<List<char>> grid, List<List<IBlock>> Maze)
        {
            this.Self = Self;
            this.Direction = Direction;
            this.target = target;
            this.grid = grid;
            this.Maze = Maze;
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

        internal void ComputeAllSolutions()
        {
            var solutions = new List<List<DirectionSolution>>();
            int left = 0, top = 0;
            foreach (var line in grid)
            {
                left = 0;
                var selfPosition = new Position { X = top, Y = left };
                var dirsolist = new List<DirectionSolution>();
                foreach (var col in line)
                {
                    var gidOfSolutions = new List<List<DirectionSolution>>();
                    dirsolist.Add(new DirectionSolution { GridOfSolutions = ComputeAllTargetPostions() });
                    left += 1;
                }
                solutions.Add(dirsolist);
                top += 1;
            }
            this.Solutions = solutions;
        }

        private List<List<IDirectionSolution>> ComputeAllTargetPostions()
        {
            var gidOfSolutions = new List<List<IDirectionSolution>>();
            int left = 0, top = 0;
            foreach (var line in grid)
            {
                left = 0;
                var targetPosition = new Position { X = top, Y = left };
                var dirsolist = new List<IDirectionSolution>();
                foreach (var col in line)
                {
                    dirsolist.Add(new DirectionSolution { Solution = RandomDirection() });
                    left += 1;
                }
                gidOfSolutions.Add(dirsolist);
                top += 1;
            }
            return gidOfSolutions;
        }

        public IDirection ComputePath()
        {
            var solution = Solutions[Self.Coord.X][Self.Coord.Y].GridOfSolutions[target.Coord.X][target.Coord.Y].Solution;
            if (!solution.Equals(Self.Direction))
            {
                return solution;
            }
            return Direction;
        }

        private Ghost FindPath(IDirection direction, Ghost store, int depth = 0)
        {
            var ghost = new Ghost(new Position { X = store.Coord.X, Y = store.Coord.Y }, store, direction);

            while (!direction.Equals(DirectionType.StandStill.Direction)
                && (grid[ghost.Coord.X][ghost.Coord.Y].Equals('·')
                 || grid[ghost.Coord.X][ghost.Coord.Y].Equals(' ')))
            {
                ghost.MoveCoord(direction);
            }

            if (!direction.Equals(DirectionType.StandStill.Direction)) store = ghost;

            if ((ghost.Coord.X.Equals(target.Coord.X) && ghost.Coord.Y.Equals(target.Coord.Y)) || depth > 3)
            {
                return store;
            }

            depth++;

            List<Ghost> reduced = ReduceGhosts(store, depth);

            reduced.Sort(CompareGhosts());

            return FindPath(DirectionType.Right.Direction, reduced[3], depth);
        }

        private List<Ghost> ReduceGhosts(Ghost store, int depth)
        {
            var up = FindPath(DirectionType.Up.Direction, store, depth);
            var down = FindPath(DirectionType.Down.Direction, store, depth);
            var left = FindPath(DirectionType.Left.Direction, store, depth);
            var right = FindPath(DirectionType.Right.Direction, store, depth);

            List<Ghost> reduced = new List<Ghost> {
                up,
                down,
                left,
                right,
            };
            return reduced;
        }

        private Comparison<Ghost> CompareGhosts()
        {
            return (a, b) =>
            {
                var ax = a.Coord.X - target.Coord.X;
                var ay = a.Coord.Y - target.Coord.Y;
                var bx = b.Coord.X - target.Coord.X;
                var by = b.Coord.Y - target.Coord.Y;
                if (bx * bx + by * by < ax * ax + ay * ax) return 1;
                if (bx * bx + by * by > ax * ax + ay * ax) return -1;
                return 0;
            };
        }
    }
}
