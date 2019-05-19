using utils_libs.Abstractions;
using utils_libs.Directions;

namespace ghost_libs
{
    public class Ghost : IPlayer
    {
        public IDirection Direction { get; set; }
        public IPosition Position { get; set; }
        public IDirection WantedDirection { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int TickCounter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int DotsEaten { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public Ghost()
        {
            this.Direction = new Right();
            this.Position = new Position();
        }
        public void Move() => Position = Direction.Move(Position);
        public void SetDirection(IDirection direction) => this.Direction = direction;
        public void SetPosition(int x, int y) => Position = new Position() { X = x, Y = y };
        public void SetWantedDirection(IDirection wantedDirection)
        {
            throw new System.NotImplementedException();
        }

        public void UnsetWantedDirection()
        {
            throw new System.NotImplementedException();
        }
    }
}
