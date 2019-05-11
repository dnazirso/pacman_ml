using utils_libs.Abstractions;

namespace utils_libs.Directions
{
    public class Down : IDirection
    {
        public IPosition Move(IPosition position)
        {
            position.X++;
            return position;
        }
    }
}
