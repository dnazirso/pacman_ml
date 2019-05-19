using utils_libs.Abstractions;
using utils_libs.Directions;
using utils_libs.Tools;

namespace pacman_libs
{
    public class Pacman : IPacman
    {
        public IDirection Direction { get; set; }
        public IPosition Position { get; set; }
        public IDirection WantedDirection { get; set; }
        public int TickCounter { get; set; }
        public int DotsEaten { get; set; }

        public Pacman()
        {
            this.Direction = new Right();
            this.Position = new Position();
        }
        public void Move() => Position = Direction.Move(Position);
        public void SetDirection(IDirection direction) => this.Direction = direction;
        public void SetPosition(int x, int y) => Position = new Position() { X = x, Y = y };
        public void SetWantedDirection(IDirection wantedDirection) => WantedDirection = wantedDirection;
        public void UnsetWantedDirection() => WantedDirection = DirectionType.StandStill.Direction;
    }
}
