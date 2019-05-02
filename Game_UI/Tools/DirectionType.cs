using pacman_libs;
using System.Linq;
using System.Windows.Input;

namespace Game_UI.Tools
{
    /// <summary>
    /// Static class responsible of key-direction mapping and revers
    /// </summary>
    internal class DirectionType : EnumerationBase
    {
        #region Ready steady directions
        private static readonly IDirection u = new Up();
        private static readonly IDirection d = new Down();
        private static readonly IDirection l = new Left();
        private static readonly IDirection r = new Right();
        private static readonly IDirection s = new StandStill();
        #endregion
        #region Enumerations
        public static readonly DirectionType StandStill = new DirectionType(Key.None, s, 0);
        public static readonly DirectionType Up = new DirectionType(Key.Up, d, 270);
        public static readonly DirectionType Down = new DirectionType(Key.Down, u, 90);
        public static readonly DirectionType Left = new DirectionType(Key.Left, r, 180);
        public static readonly DirectionType Right = new DirectionType(Key.Right, l, 0);
        #endregion
        /// <summary>
        /// constructor that prepare enumarations
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Direction"></param>
        /// <param name="Angle"></param>
        internal DirectionType(Key Key, IDirection Direction, int Angle) : base(Key, Direction, Angle) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static bool ExistsWhitin(Key key) => GetAll<DirectionType>()
            .ToList()
            .Exists(x => x.Key.Equals(key));
        internal static bool ExistsWhitin(IDirection direction) => GetAll<DirectionType>()
            .ToList()
            .Exists(x => x.Direction.Equals(direction));

        /// <summary>
        /// Convert a Key into a Direction
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static IDirection ToDirection(Key key) => GetAll<DirectionType>()
            .Where(k => k.Key.Equals(key))
            .FirstOrDefault().Direction;

        /// <summary>
        /// Convert a Direction into a Key
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        internal static Key ToKey(IDirection direction) => GetAll<DirectionType>()
            .Where(k => k.Direction.GetType().Equals(direction.GetType()))
            .FirstOrDefault().Key;

        /// <summary>
        /// Convert a Direction into an int represanting an angle
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        internal static int ToAngle(IDirection direction) => GetAll<DirectionType>()
            .Where(k => k.Direction.GetType().Equals(direction.GetType()))
            .FirstOrDefault().Angle;
    }
}
