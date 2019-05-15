namespace utils_libs.Abstractions
{
    public interface IPlayer
    {
        IDirection Direction { get; set; }
        IDirection WantedDirection { get; set; }
        IPosition Position { get; set; }
        int DotsEaten { get; set; }
        int TickCounter { get; set; }

        void Move();
        void SetDirection(IDirection direction);
        void SetWantedDirection(IDirection wantedDirection);
        void UnsetWantedDirection();
        void SetPosition(int x, int y);
    }
}