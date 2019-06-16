using System.Collections.Generic;

namespace utils_libs.Abstractions
{
    public interface IPlayer
    {
        IDirection Direction { get; set; }
        IDirection WantedDirection { get; set; }
        IPosition Position { get; set; }
        IPosition Coord { get; set; }
        int TickCounter { get; set; }
        void Move();
        bool WillCollide(IDirection direction);
        void SetDirection(IDirection direction);
        void SetWantedDirection(IDirection wantedDirection);
        void TrySetDirection(IDirection direction);
        void RetrySetDirectionAndMove(IDirection direction);
        void UnsetWantedDirection();
        void SetPosition(int x, int y);
    }
}