using utils_libs.Models;

namespace utils_libs.Abstractions
{
    public interface IUIPlayer
    {
        Position LastPosition { get; set; }
        IPlayer Player { get; }
        void UpdatePosition();
    }
}
