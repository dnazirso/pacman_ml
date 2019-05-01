using pacman_libs;
using System.Windows.Input;

namespace Game_UI.Tools
{
    /// <summary>
    /// Static class responsible of key-direction mapping and revers
    /// </summary>
    internal static class DirectionMapper
    {
        // TODO : Think about an abstract Enumation class

        /// <summary>
        /// Give back a direction from a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static IDirection ToDirection(Key key)
        {
            switch (key)
            {
                case Key.Left:
                    return new Right();
                case Key.Up:
                    return new Down();
                case Key.Right:
                    return new Left();
                case Key.Down:
                    return new Up();
                default:
                    return new StandStill();
            }
        }

        /// <summary>
        /// Give back a key from a gien direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        internal static Key ToKey(IDirection direction)
        {
            if (direction.GetType() == typeof(Right))
            {
                return Key.Left;
            }
            if (direction.GetType() == typeof(Down))
            {
                return Key.Up;
            }
            if (direction.GetType() == typeof(Left))
            {
                return Key.Right;
            }
            if (direction.GetType() == typeof(Up))
            {
                return Key.Down;
            }
            return Key.None;
        }
    }
}
