using utils_libs.Abstractions;
using utils_libs.Models;

namespace utils_libs.Directions
{
    public class Up : IDirection
    {
        public Position Move(Position position)
        {
            position.X--;
            return position;
        }
    }
}
