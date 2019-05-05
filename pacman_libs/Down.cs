namespace pacman_libs
{
    public class Down : IDirection
    {
        public Position Move(Position position)
        {
            position.X++;
            return position;
        }
    }
}
