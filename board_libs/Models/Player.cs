using System.Collections.Generic;
using utils_libs.Abstractions;
using utils_libs.Directions;
using utils_libs.Tools;

namespace board_libs.Models
{
    public class Player : IPlayer
    {
        public IDirection Direction { get; set; }
        public IPosition Position { get; set; }
        public IDirection WantedDirection { get; set; }
        public int TickCounter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int DotsEaten { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IPosition Coord { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Player()
        {
            this.Direction = new Left();
            this.Position = new Position();
        }
        public void Move() => Position = Direction.Move(Position);
        public void SetDirection(IDirection direction) => this.Direction = direction;
        public void SetPosition(int x, int y) => Position = new Position() { X = x, Y = y };
        public void SetWantedDirection(IDirection wantedDirection)
        {
            WantedDirection = wantedDirection;
        }

        public void UnsetWantedDirection()
        {
            WantedDirection = DirectionType.StandStill.Direction;
        }

        public bool WillCollide()
        {
            throw new System.NotImplementedException();
        }

        public void RetrySetDirectionAndMove(IDirection direction)
        {
            throw new System.NotImplementedException();
        }

        public bool WillCollide(IDirection direction)
        {
            throw new System.NotImplementedException();
        }

        public void TrySetDirection(IDirection direction)
        {
            throw new System.NotImplementedException();
        }
    }
}
