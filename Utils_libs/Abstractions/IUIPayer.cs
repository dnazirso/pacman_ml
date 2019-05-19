namespace utils_libs.Abstractions
{
    public interface IUIPayer
    {
        IPosition LastPosition { get; set; }
        IPlayer Player { get; }
        void UpdatePosition();
    }
}
