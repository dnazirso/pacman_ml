using System.Collections.Generic;
using utils_libs.Abstractions;

namespace ghost_libs
{
    public class Ghost : PlayerBase
    {
        internal IPlayer parent { get; set; }
        private GhostAI ghostAI { get; set; }

        /// <summary>
        /// dummy ghost for path testing
        /// </summary>
        public Ghost()
        {
            Initialize();
        }

        /// <summary>
        /// AI dummy ghost for path computing
        /// </summary>
        /// <param name="Coord"></param>
        /// <param name="parent"></param>
        /// <param name="Direction"></param>
        internal Ghost(IPosition Coord, Ghost parent, IDirection Direction)
        {
            this.Direction = Direction;
            this.Coord = Coord;
            this.parent = parent;
        }

        /// <summary>
        /// Ghost instancited within its sprite
        /// </summary>
        /// <param name="target"></param>
        /// <param name="grid"></param>
        public Ghost(IPlayer target, IPosition Coord, List<List<char>> grid, List<List<IBlock>> Maze)
        {
            Initialize();
            this.Coord = Coord;
            this.Maze = Maze;
            ghostAI = new GhostAI(this, this.Direction, target, grid, Maze);
            ghostAI.ComputeAllSolutions();
        }

        public override void Move()
        {
            SetDirection(ghostAI.ComputePath());
            if (WillCollide(Direction)) return;
            Direction.Move(Position);
            RetrySetDirectionAndMove(WantedDirection);
        }
    }
}
