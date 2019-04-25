using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_libs
{
    public class Down : IDirection
    {
        public Position Move(Position position)
        {
            position.X--;
            return position;
        }
    }
}
