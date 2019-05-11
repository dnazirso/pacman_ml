using utils_libs.Abstractions;

namespace utils_libs.Directions
{
    public class StandStill : IDirection
    {
        public IPosition Move(IPosition position) => position;
    }
}