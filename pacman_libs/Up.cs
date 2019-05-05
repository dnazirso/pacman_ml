namespace pacman_libs
{
    public class Up : IDirection
    {
        public Position Move(Position position)
        {
            position.X--;
            return position;
        }
    }
}
