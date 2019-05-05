using pacman_libs;

namespace Game_UI.Models
{
    public class Area
    {
        int Xmin { get; set; }
        int Xmax { get; set; }
        int Ymin { get; set; }
        int Ymax { get; set; }
        bool IsBlocking { get; set; }

        private Area(int xmin, int xmax, int ymin, int ymax, bool isblocking)
        {
            Xmin = xmin;
            Xmax = xmax;
            Ymin = ymin;
            Ymax = ymax;
            IsBlocking = isblocking;
        }

        public static Area SetPositions(IPosition upperLeft, int width, int height, bool isBlocking = false)
        {
            int Xmin = upperLeft.X;
            int Xmax = upperLeft.X + width;
            int Ymin = upperLeft.Y;
            int Ymax = upperLeft.Y + height;

            return new Area(Xmin, Xmax, Ymin, Ymax, isBlocking);
        }

        public bool WillCollide(IPlayer p)
        {
            IPlayer newPlayer = new Player
            {
                Direction = p.Direction,
                Position = new pacman_libs.Position
                {
                    X = p.Position.X,
                    Y = p.Position.Y
                }
            };
            newPlayer.Move();
            return DoesCollide(newPlayer.Position);
        }

        public bool HasCollide(IPlayer p) => DoesCollide(p.Position);

        private bool DoesCollide(IPosition position)
        {
            if (IsBlocking == false)
            {
                return false;
            }
            return (position.X < Xmax + 10
             && position.X > Xmin - 10
             && position.Y < Ymax + 10
             && position.Y > Ymin - 10);
        }
    }
}
