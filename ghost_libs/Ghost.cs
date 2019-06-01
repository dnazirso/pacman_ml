using System;
using System.Collections.Generic;
using utils_libs.Abstractions;
using utils_libs.Directions;
using utils_libs.Tools;

namespace ghost_libs
{
    public class Ghost : IPlayer
    {
        private int TickCounterRandom;

        private IPlayer target { get; set; }
        private List<List<char>> grid { get; set; }
        public IDirection Direction { get; set; }
        public IPosition Position { get; set; }
        public IDirection WantedDirection { get; set; }
        public int TickCounter { get; set; }
        public IPosition Coord { get; set; }

        public Ghost()
        {
            Initialize();
        }
        public Ghost(IDirection Direction, IPosition Position)
        {
            this.Direction = Direction;
            this.Position = Position;
        }
        private Ghost(IPosition Coord, IDirection Direction)
        {
            this.Direction = Direction;
            this.Coord = Coord;
        }
        private void Initialize()
        {
            this.Direction = new Right();
            this.Position = new Position();
            this.WantedDirection = DirectionType.StandStill.Direction;
        }
        public Ghost(IPlayer target, List<List<char>> grid)
        {
            Initialize();
            this.target = target;
            this.grid = grid;
        }
        public void SetDirection(IDirection direction) => this.Direction = direction;
        public void SetPosition(int x, int y) => Position = new Position() { X = x, Y = y };
        public void SetWantedDirection(IDirection wantedDirection) => WantedDirection = wantedDirection;
        public void UnsetWantedDirection() => WantedDirection = DirectionType.StandStill.Direction;
        public void Move() => Position = Direction.Move(Position);
        public void MoveCoord(IDirection direction) => Coord = direction.Move(Position);
        public void Move(List<List<IBlock>> Maze)
        {
            SetDirection(ComputePath());
            if (WillCollide(Maze, Direction)) return;
            Move();
            RetrySetDirectionAndMove(Maze, WantedDirection);
        }
        public void SetDirection(List<List<IBlock>> Maze, IDirection direction)
        {
            if (!WillCollide(Maze, direction))
            {
                SetDirection(direction);
                UnsetWantedDirection();
                TickCounter = 0;
            }
            else
            {
                SetWantedDirection(direction);
            }
        }
        public void RetrySetDirectionAndMove(List<List<IBlock>> Maze, IDirection direction)
        {
            if (TickCounter < 20 && !WantedDirection.Equals(DirectionType.StandStill.Direction))
            {
                TickCounter++;
                SetDirection(Maze, WantedDirection);
            }
        }
        public bool WillCollide(List<List<IBlock>> Maze, IDirection direction)
        {
            IPlayer testPlayer = new Ghost
            {
                Direction = direction,
                Position = new Position
                {
                    X = Position.X,
                    Y = Position.Y
                }
            };

            return Maze.Exists(line => line.Exists(col =>
            {
                Coord = col.GetCoord();
                return col.WillCollide(testPlayer);
            }
            ));
        }
        private IDirection ComputePath()
        {
            var attempts = FindPath(DirectionType.StandStill.Direction, new List<Ghost>());
            if (attempts.Count == 0) return RandomDirection();
            return attempts[attempts.Count - 1].Direction;
        }

        private IDirection RandomDirection()
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

        private List<Ghost> FindPath(IDirection direction, List<Ghost> store)
        {
            var ghost = new Ghost(new Position { X = Coord.X, Y = Coord.Y }, direction);
            while (!direction.Equals(DirectionType.StandStill.Direction)
                && (grid[ghost.Coord.X][ghost.Coord.Y].Equals('·')
                 || grid[ghost.Coord.X][ghost.Coord.Y].Equals(' ')))
            {
                ghost.MoveCoord(direction);
            }
            if (!direction.Equals(DirectionType.StandStill.Direction)) store.Add(ghost);
            var ghosts = new List<Ghost>();
            ghosts.AddRange(store);

            if (Coord.Equals(target.Coord) || store.Count > 3)
            {
                return store;
            }

            List<List<Ghost>> attempt = new List<List<Ghost>>
            {
                FindPath(DirectionType.Up.Direction, ghosts),
                FindPath(DirectionType.Down.Direction, ghosts),
                FindPath(DirectionType.Left.Direction, ghosts),
                FindPath(DirectionType.Right.Direction, ghosts)
            };

            attempt.Sort((a, b) =>
            {
                var ax = a[a.Count - 1].Coord.X - target.Coord.X;
                var ay = a[a.Count - 1].Coord.Y - target.Coord.Y;
                var bx = b[b.Count - 1].Coord.X - target.Coord.X;
                var by = b[b.Count - 1].Coord.Y - target.Coord.Y;
                if (bx + by > ax + ay) return 1;
                if (bx + by < ax + ay) return -1;
                return 0;
            });

            return FindPath(DirectionType.Right.Direction, attempt[attempt.Count - 1]);
        }
    }
}
