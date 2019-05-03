using Game_UI.Tools;
using pacman_libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_UI.Models
{
    public class Area
    {
        IPosition UpperLeft { get; }
        IPosition UpperRight { get; }
        IPosition BottomLeft { get; }
        IPosition BottomRight { get; }
        public bool IsForbidden { get; set; } = true;

        private Area(IPosition UpperLeft, IPosition UpperRight, IPosition BottomLeft, IPosition BotomRight)
        {
            this.UpperLeft = UpperLeft;
            this.UpperRight = UpperRight;
            this.BottomLeft = BottomLeft;
            this.BottomRight = BotomRight;
        }

        public static Area SetPositions(IPosition upperLeft, int width, int height)
        {
            Position upperRight = new Position { X = upperLeft.X + width, Y = upperLeft.Y };
            Position bottomLeft = new Position { X = upperLeft.X, Y = upperLeft.Y + height };
            Position bottomRight = new Position { X = upperLeft.X + width, Y = upperLeft.Y + height };

            return new Area(upperLeft, upperRight, bottomLeft, bottomRight);
        }

        public IPosition GetPosition(string corner)
        {
            if (corner == "UL") return UpperLeft;
            if (corner == "UR") return UpperRight;
            if (corner == "BL") return BottomLeft;
            if (corner == "BR") return BottomRight;
            return UpperLeft;
        }

        public bool HasCollide(IPlayer p)
        {
            if (HasCollideOnLeft(p) || HasCollideOnRight(p) || HasCollideOnBottom(p) || HasCollideOnTop(p))
                return true;
            return false;
        }

        bool HasCollideOnLeft(IPlayer p)
        {
            if (
            p.Direction.Equals(DirectionType.Right.Direction)
            && p.Position.X + 10 > UpperLeft.X
            && p.Position.Y + 10 < UpperLeft.Y
            && p.Position.X + 10 < BottomLeft.X
            && p.Position.Y + 10 < BottomLeft.Y
            )
            {
                return true;
            }
            return false;
        }

        bool HasCollideOnRight(IPlayer p)
        {
            if (
            p.Direction.Equals(DirectionType.Left.Direction)
            && p.Position.X - 10 > UpperRight.X
            && p.Position.Y - 10 > UpperRight.Y
            && p.Position.X - 10 < BottomRight.X
            && p.Position.Y - 10 > BottomRight.Y
            )
            {
                return true;
            }
            return false;
        }

        bool HasCollideOnBottom(IPlayer p)
        {
            if (
            p.Direction.Equals(DirectionType.Up.Direction)
            && p.Position.X - 10 < BottomLeft.X
            && p.Position.Y - 10 > BottomLeft.Y
            && p.Position.X - 10 < BottomRight.X
            && p.Position.Y - 10 < BottomRight.Y
            )
            {
                return true;
            }
            return false;

        }

        bool HasCollideOnTop(IPlayer p)
        {
            if (
            p.Direction.Equals(DirectionType.Down.Direction)
            && p.Position.X + 10 > UpperLeft.X
            && p.Position.Y + 10 > UpperLeft.Y
            && p.Position.X + 10 > UpperRight.X
            && p.Position.Y + 10 < UpperRight.Y
            )
            {
                return true;
            }
            return false;

        }
    }
}
