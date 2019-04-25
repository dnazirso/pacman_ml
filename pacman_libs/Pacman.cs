
namespace pacman_libs
{
    public class Pacman
    {
        public IDirection Direction { get; set; }
        public Position Position { get; set; }
        public Pacman()
        {
            this.Direction = new Left();
            this.Position = new Position();
        }
        public void Move() => Position = Direction.Move(Position);
        public void SetDirection(IDirection direction) => this.Direction = direction;
    }
}
