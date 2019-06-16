using System.Collections.Generic;
using utils_libs.Abstractions;
using utils_libs.Directions;
using utils_libs.Tools;

namespace pacman_libs
{
    public class Pacman : IPacman
    {
        private IPosition prevCoord { get; set; }
        private List<List<IBlock>> Maze { get; set; }
        public IDirection Direction { get; set; }
        public IPosition Position { get; set; }
        public IDirection WantedDirection { get; set; }
        public int TickCounter { get; set; }
        public int DotsEaten { get; set; }
        public IPosition Coord { get; set; }

        public Pacman()
        {
            Initialize();
        }

        public Pacman(IPosition coord)
        {
            this.Coord = new Position { X = coord.X, Y = coord.Y };
            prevCoord = new Position { X = coord.X, Y = coord.Y };
            Initialize();
        }

        public Pacman(List<List<IBlock>> maze)
        {
            this.Maze = maze;
            Initialize();
        }

        private void Initialize()
        {
            this.Direction = new Right();
            this.Position = new Position();
            this.prevCoord = new Position();
            this.WantedDirection = DirectionType.StandStill.Direction;
        }

        public void SetDirection(IDirection direction) => this.Direction = direction;

        public void SetPosition(int x, int y) => Position = new Position() { X = x, Y = y };

        public void SetWantedDirection(IDirection wantedDirection) => WantedDirection = wantedDirection;

        public void UnsetWantedDirection() => WantedDirection = DirectionType.StandStill.Direction;

        public void MoveCoord(IDirection direction) => direction.Move(Coord);

        public void Move()
        {
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
            IPlayer testPlayer = new Pacman
            {
                Direction = direction,
                Position = new Position
                {
                    X = Position.X,
                    Y = Position.Y
                }
            };

            prevCoord.Y = Coord.Y;
            prevCoord.X = Coord.X;
            return Maze.Exists(line => line.Exists(col =>
                 {
                     var willcollide = col.WillCollide(testPlayer);
                     var coord = col.GetCoord();
                     if (col.Overlap(testPlayer) && (coord.X != prevCoord.X || coord.Y != coord.Y))
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
