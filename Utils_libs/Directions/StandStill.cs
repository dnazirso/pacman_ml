using utils_libs.Abstractions;
using utils_libs.Models;

namespace utils_libs.Directions
{
    public class StandStill : IDirection
    {
        public Position Move(Position position) => position;
    }
}