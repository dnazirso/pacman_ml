using pacman_libs;

namespace Game_UI.Sprites
{
    public interface IBlock
    {
        bool HasCollide(IPlayer p);
        bool WillCollide(IPlayer p);
    }
}