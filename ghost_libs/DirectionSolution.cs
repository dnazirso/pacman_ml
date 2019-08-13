using System.Collections.Generic;
using utils_libs.Abstractions;
using utils_libs.Models;

namespace ghost_libs
{
    public class DirectionSolution : IDirectionSolution
    {
        public Position Self { get; set; }
        public Position Target { get; set; }
        public IDirection Solution { get; set; }
        internal List<List<IDirectionSolution>> GridOfSolutions { get; set; }
    }
}
