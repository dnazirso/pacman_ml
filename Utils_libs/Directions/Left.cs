using utils_libs.Abstractions;

namespace utils_libs.Directions
{
    public class Left : IDirection
    {
        public IPosition Move(IPosition position)
        {
            position.Y--;
            return position;
        }
    }
}