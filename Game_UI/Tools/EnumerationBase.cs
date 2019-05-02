using pacman_libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace Game_UI.Tools
{
    /// <summary>
    /// Enumaration abstraction class instead of Enums
    /// </summary>
    public abstract class EnumerationBase : IComparable
    {
        public IDirection Direction { get; private set; }

        public Key Key { get; private set; }

        public int Angle { get; private set; }

        protected EnumerationBase(Key Key, IDirection Direction, int Angle)
        {
            this.Key = Key;
            this.Direction = Direction;
            this.Angle = Angle;
        }

        public override string ToString() => nameof(Direction);

        public static IEnumerable<T> GetAll<T>() where T : EnumerationBase
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EnumerationBase otherValue))
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Key.Equals(otherValue.Key);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => base.GetHashCode();

        public int CompareTo(object obj) => Key.CompareTo(((EnumerationBase)obj).Key);
    }
}
