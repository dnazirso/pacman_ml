using utils_libs.Abstractions;
using utils_libs.Models;

namespace ghost_libs
{
    public interface IDirectionSolution
    {
        Position Self { get; set; }
        IDirection Solution { get; set; }
        Position Target { get; set; }
    }
}