using utils_libs.Abstractions;

namespace utils_libs.Models
{
    public struct Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
