namespace pacman_libs
{
    public interface IPlayer
    {
        IDirection Direction { get; set; }
        Position Position { get; set; }

        void Move();
        void SetDirection(IDirection direction);
        void SetPosition(int x, int y);
    }
}