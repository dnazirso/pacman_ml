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
        public Ghost(IPlayer target, IPosition Coord, List<List<char>> grid)
        {
            Initialize();
            this.Coord = Coord;
            ghostAI = new GhostAI(this.Coord, this.Direction, target, grid);
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

        public void Move() => Position = Direction.Move(Position);

        public void MoveCoord(IDirection direction) => Coord = direction.Move(Position);

        public void Move(List<List<IBlock>> Maze)
        {
            SetDirection(ghostAI.ComputePath());
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
    }
}
