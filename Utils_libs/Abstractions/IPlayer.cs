using System.Collections.Generic;
using utils_libs.Models;

namespace utils_libs.Abstractions
{
    public interface IPlayer
    {
        IDirection Direction { get; set; }
        IDirection WantedDirection { get; set; }
        Position Position { get; set; }
        Position Coord { get; set; }
        List<List<IBlock>> Maze { get; set; }
        void Move();
        bool WillCollide(IDirection direction);
        void SetDirection(IDirection direction);
        void SetWantedDirection(IDirection wantedDirection);
        void TrySetDirection(IDirection direction);
        void RetrySetDirection(IDirection direction);
        void UnsetWantedDirection();
        void SetPosition(int x, int y);
    }
}