namespace utils_libs.Abstractions
{
    public interface IUIPlayer
    {
        IPosition LastPosition { get; set; }
        IPlayer Player { get; }
        void UpdatePosition();
    }
}
