namespace pacman_libs
{
    public class StandStill : IDirection
    {
        public Position Move(Position position) => position;
    }
}