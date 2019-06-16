using utils_libs.Abstractions;

namespace ghost_libs
{
    public interface IDirectionSolution
    {
        IPosition Self { get; set; }
        IDirection Solution { get; set; }
        IPosition Target { get; set; }
    }
}