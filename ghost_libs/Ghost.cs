using System.Collections.Generic;
using utils_libs.Abstractions;
using utils_libs.Models;

namespace ghost_libs
{
    public class Ghost : PlayerBase
    {
        /// <summary>
        /// Represent the ghost interligence
        /// </summary>
        private GhostAI ghostAI { get; set; }

        /// <summary>
        /// dummy ghost for path testing
        /// </summary>
        public Ghost()
        {
            Initialize();
        }

        /// <summary>
        /// Ghost instancited within its sprite
        /// </summary>
        /// <param name="target"></param>
        /// <param name="Grid"></param>
        public Ghost(IPlayer target, Position Coord, List<List<char>> Grid, List<List<IBlock>> Maze)
        {
            Initialize();
            this.Coord = Coord;
            this.Maze = Maze;
            ghostAI = new GhostAI(this, target, Grid);
        }

        public override void Move()
        {
            SetDirection(ghostAI.GetDirectionSolution());
            if (WillCollide(Direction)) return;
            Position = Direction.Move(Position);
            RetrySetDirection(WantedDirection);
        }
    }
}
