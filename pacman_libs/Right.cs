namespace pacman_libs
{
    public class Right : IDirection
    {
        public Position Move(Position position)
        {
            position.Y++;
            return position;
        }
    }
}
