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
        void Move(List<List<IBlock>> blocks);
        bool WillCollide(List<List<IBlock>> blocks, IDirection direction);
        void SetDirection(List<List<IBlock>> Maze, IDirection direction);
        void SetWantedDirection(IDirection wantedDirection);
        void RetrySetDirectionAndMove(List<List<IBlock>> Maze, IDirection direction);
        void UnsetWantedDirection();
        void SetPosition(int x, int y);
    }
}