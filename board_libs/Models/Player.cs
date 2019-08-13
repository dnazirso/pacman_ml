using utils_libs.Abstractions;
using utils_libs.Directions;
using utils_libs.Models;

namespace board_libs.Models
{
    public class Player : PlayerBase
    {
        public Player()
        {
            this.Direction = new Left();
            this.Position = new Position();
        }
        public override void Move() => Position = Direction.Move(Position);
    }
}
