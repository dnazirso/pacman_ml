using System.Linq;
using System.Windows.Input;
using utils_libs.Abstractions;
using utils_libs.Directions;

namespace utils_libs.Tools
{
    /// <summary>
    /// Static class responsible of key-direction mapping and revers
    /// </summary>
    public class DirectionType : EnumerationBase
    {
        #region Enumerations
        public static readonly DirectionType StandStill = new DirectionType(Key.None, new StandStill(), 0);
        public static readonly DirectionType Up = new DirectionType(Key.Up, new Up(), 270);
        public static readonly DirectionType Down = new DirectionType(Key.Down, new Down(), 90);
        public static readonly DirectionType Left = new DirectionType(Key.Left, new Left(), 180);
        public static readonly DirectionType Right = new DirectionType(Key.Right, new Right(), 0);
        #endregion
        /// <summary>
        /// constructor that prepare enumarations
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Direction"></param>
        /// <param name="Angle"></param>
        public DirectionType(Key Key, IDirection Direction, int Angle) : base(Key, Direction, Angle) { }

        /// <summary>
        /// Checks if the given key is included to the enumerated ones
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ExistsWhitin(Key key) => GetAll<DirectionType>()
            .ToList()
            .Exists(x => x.Key.Equals(key));

        /// <summary>
        /// Checks if the given direction is included to the enumerated ones
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static bool ExistsWhitin(IDirection direction) => GetAll<DirectionType>()
            .ToList()
            .Exists(x => x.Direction.Equals(direction));

        /// <summary>
        /// Convert a Key into a Direction
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IDirection ToDirection(Key key) => GetAll<DirectionType>()
            .Where(k => k.Key.Equals(key))
            .FirstOrDefault().Direction;

        /// <summary>
        /// Convert a Direction into a Key
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Key ToKey(IDirection direction) => GetAll<DirectionType>()
            .Where(k => k.Direction.GetType().Equals(direction.GetType()))
            .FirstOrDefault().Key;

        /// <summary>
        /// Convert a Direction into an int represanting an angle
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static int ToAngle(IDirection direction) => GetAll<DirectionType>()
            .Where(k => k.Direction.GetType().Equals(direction.GetType()))
            .FirstOrDefault().Angle;
    }
}
