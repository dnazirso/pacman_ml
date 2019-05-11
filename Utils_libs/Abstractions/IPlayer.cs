namespace utils_libs.Abstractions
{
    public interface IPlayer
    {
        IDirection Direction { get; set; }
        IPosition Position { get; set; }

        void Move();
        void SetDirection(IDirection direction);
        void SetPosition(int x, int y);
    }
}