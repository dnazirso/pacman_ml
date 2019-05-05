namespace pacman_libs
{
    public class Player : IPlayer
    {
        public IDirection Direction { get; set; }
        public Position Position { get; set; }
        public Player()
        {
            this.Direction = new Left();
            this.Position = new Position();
        }
        public void Move() => Position = Direction.Move(Position);
        public void SetDirection(IDirection direction) => this.Direction = direction;
        public void SetPosition(int x, int y) => Position = new Position() { X = x, Y = y };
    }
}
