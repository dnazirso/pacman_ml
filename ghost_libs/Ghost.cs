using pacman_libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghost_libs
{
    public class Ghost : IPlayer
    {
        public IDirection Direction { get; set; }
        public Position Position { get; set; }
        public Ghost()
        {
            this.Direction = new Left();
            this.Position = new Position();
        }
        public void Move() => Position = Direction.Move(Position);
        public void SetDirection(IDirection direction) => this.Direction = direction;
        public void SetPosition(int x, int y) => Position = new Position() { X = x, Y = y };
    }
}
