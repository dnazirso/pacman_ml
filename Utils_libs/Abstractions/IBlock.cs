namespace utils_libs.Abstractions
{
    public interface IBlock
    {
        bool Collide(IPlayer p);
        bool Overlap(IPlayer p);
        bool WillCollide(IPlayer p);
        IPosition GetCoord();
    }
}