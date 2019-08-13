using System.Collections.Generic;
using utils_libs.Directions;
using utils_libs.Models;
using utils_libs.Tools;

namespace utils_libs.Abstractions
{
    public abstract class PlayerBase : IPlayer
    {
        public IDirection Direction { get; set; }
        public Position Position { get; set; }
        public IDirection WantedDirection { get; set; }
        public int TickCounter { get; set; }
        public Position Coord { get; set; }
        public List<List<IBlock>> Maze { get; set; }

        public PlayerBase()
        {
            Initialize();
        }

        public void Initialize()
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

        public virtual void Move()
        {
            if (WillCollide(Direction)) return;
            Position = Direction.Move(Position);
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
            IPlayer testPlayer = (IPlayer)this.MemberwiseClone();

            testPlayer.Direction = direction;
            testPlayer.Position = new Position
            {
                X = Position.X,
                Y = Position.Y
            };

            return Maze.Exists(line => line.Exists(col =>
            {
                var willcollide = col.WillCollide(testPlayer);
                var coord = col.GetCoord();
                if (col.Overlap(testPlayer))
                {
                    Coord = new Position { X = coord.X, Y = coord.Y };
                }
                return willcollide;
            }
            ));
        }
    }
}
