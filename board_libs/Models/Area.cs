using utils_libs.Abstractions;
using utils_libs.Models;

namespace board_libs.Models
{
    public class Area : IBlock
    {
        public Position Min { get; }
        public Position Max { get; }
        public Position Coord { get; }
        public int Size { get; }
        public bool IsBlocking { get; }
        public char Shape { get; private set; }

        private Area(int xmin, int xmax, int ymin, int ymax, Position coord, int size, bool isblocking, char shape)
        {
            Min = new Position { X = xmin, Y = ymin };
            Max = new Position { X = xmax, Y = ymax };
            Coord = coord;
            Size = size;
            IsBlocking = isblocking;
            Shape = shape;
        }

        public static Area CreateNew(Position upperLeft, Position coord, int size, bool isBlocking, char shape)
        {
            int Xmin = upperLeft.X;
            int Ymin = upperLeft.Y;
            int Xmax = upperLeft.X + size;
            int Ymax = upperLeft.Y + size;

            return new Area(Xmin, Xmax, Ymin, Ymax, coord, size, isBlocking, shape);
        }

        public bool WillCollide(IPlayer p)
        {
            IPlayer newPlayer = new Player
            {
                Direction = p.Direction,
                Position = new Position
                {
                    X = p.Position.X,
                    Y = p.Position.Y
                }
            };
            newPlayer.Move();
            return CouldCollide(newPlayer);
        }

        private bool CouldCollide(IPlayer p)
        {
            if (IsBlocking == false)
            {
                return false;
            }
            return (p.Position.X < Max.X + 10
             && p.Position.X > Min.X - 10
             && p.Position.Y < Max.Y + 10
             && p.Position.Y > Min.Y - 10);
        }

        public bool Collide(IPlayer p) =>
               (p.Position.X < Max.X
             && p.Position.X > Min.X
             && p.Position.Y < Max.Y
             && p.Position.Y > Min.Y);

        public bool Overlap(IPlayer p) =>
               (p.Position.X < Max.X + 5
             && p.Position.X > Min.X - 5
             && p.Position.Y < Max.Y + 5
             && p.Position.Y > Min.Y - 5);

        public bool EreaseDot(IPacman p)
        {
            if (Shape.Equals('·') && Collide(p))
            {
                Shape = ' ';
                p.DotsEaten++;
            }
            if (Shape.Equals('•') && Collide(p))
            {
                Shape = ' ';
                p.DotsEaten++;
            }
            return Collide(p);
        }

        public Position GetCoord() => Coord;
    }
}
