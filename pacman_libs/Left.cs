namespace pacman_libs
{
    public class Left : IDirection
    {
        public Position Move(Position position)
        {
            position.Y++;
            return position;
        }
    }
}