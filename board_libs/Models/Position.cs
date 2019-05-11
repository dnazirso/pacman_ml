using utils_libs.Abstractions;

namespace board_libs.Models
{
    public struct Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}