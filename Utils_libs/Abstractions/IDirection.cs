using utils_libs.Models;

namespace utils_libs.Abstractions
{
    public interface IDirection
    {
        Position Move(Position position);
    }
}