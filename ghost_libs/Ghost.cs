using System.Collections.Generic;
using utils_libs.Abstractions;
using utils_libs.Directions;
using utils_libs.Tools;

namespace ghost_libs
{
    public class Ghost : IPlayer
    {
        internal IPlayer parent { get; set; }
        public IDirection Direction { get; set; }
        public IPosition Position { get; set; }
        public IDirection WantedDirection { get; set; }
        public int TickCounter { get; set; }
        public IPosition Coord { get; set; }
        private List<List<IBlock>> Maze { get; }
        private GhostAI ghostAI { get; set; }

        /// <summary>
        /// dummy ghost for path testing
        /// </summary>
        public Ghost()
        {
            Initialize();
        }

        /// <summary>
        /// AI dummy ghost for path computing
        /// </summary>
        /// <param name="Coord"></param>
        /// <param name="parent"></param>
        /// <param name="Direction"></param>
        internal Ghost(IPosition Coord, Ghost parent, IDirection Direction)
        {
            this.Direction = Direction;
            this.Coord = Coord;
            this.parent = parent;
        }

        /// <summary>
        /// Ghost instancited within its sprite
        /// </summary>
        /// <param name="target"></param>
        /// <param name="grid"></param>
        public Ghost(IPlayer target, IPosition Coord, List<List<char>> grid, List<List<IBlock>> maze)
        {
            Initialize();
            this.Coord = Coord;
            this.Maze = maze;
            ghostAI = new GhostAI(this, this.Direction, target, grid, maze);
            ghostAI.ComputeAllSolutions();
        }

        private void Initialize()
        {
            this.Direction = new Right();
            this.Position = new Position();
            this.WantedDirection = DirectionType.StandStill.Direction;
        }

        public void SetDirection(IDirection direction) => this.Direction = direction;

        public void SetPosition(int x, int y) => Position = new Position() { X = x, Y = y };

        public void SetWantedDirection(IDirection wantedDirection) => WantedDirection = wantedDirection;

        public void UnsetWantedDirection() => WantedDirection = DirectionType.StandStill.Direction;

        public void MoveCoord(IDirection direction) => direction.Move(Coord);

        public void Move()
        {
            SetDirection(ghostAI.ComputePath());
            if (WillCollide(Direction)) return;
            Direction.Move(Position);
            RetrySetDirectionAndMove(WantedDirection);
        }

        public void TrySetDirection(IDirection direction)
        {
            if (WillCollide(direction))
            {
                SetWantedDirection(direction);
            }
            else
            {
                SetDirection(direction);
                UnsetWantedDirection();
                TickCounter = 0;
            }
        }

        public void RetrySetDirectionAndMove(IDirection direction)
        {
            if (TickCounter < 20 && !WantedDirection.Equals(DirectionType.StandStill.Direction))
            {
                TickCounter++;
                TrySetDirection(WantedDirection);
            }
        }

        public bool WillCollide(IDirection direction)
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
                var willcollide = col.WillCollide(testPlayer);
                var coord = col.GetCoord();
                if (col.Overlap(testPlayer))
                {
                    Coord.X = coord.X;
                    Coord.Y = coord.Y;
                }
                return willcollide;
            }
            ));
        }
    }
}
