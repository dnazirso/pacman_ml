using System;
using System.Collections.Generic;
using utils_libs.Abstractions;
using utils_libs.Tools;

namespace ghost_libs
{
    internal class GhostAI
    {
        private int TickCounterRandom;
        private IDirection Direction { get; }
        private Ghost Self { get; }
        private IPlayer target { get; }
        private List<List<char>> grid { get; }
        public List<List<IBlock>> Maze { get; }

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
            TickCounterRandom++;
            IDirection direction = DirectionType.StandStill.Direction;

            if (TickCounterRandom > 30)
            {
                TickCounterRandom = 0;
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
            }

            if (!direction.Equals(Direction) && !direction.Equals(DirectionType.StandStill.Direction)) return direction;

            return Direction;
        }

        public IDirection ComputePath()
        {
            var attempt = FindPath(DirectionType.StandStill.Direction, Self);

            return attempt.parent != null
                ? attempt.parent.Direction
                : attempt.Direction;
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
