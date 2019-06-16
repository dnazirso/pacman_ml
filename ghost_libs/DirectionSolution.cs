using System.Collections.Generic;
using utils_libs.Abstractions;

namespace ghost_libs
{
    public class DirectionSolution : IDirectionSolution
    {
        public IPosition Self { get; set; }
        public IPosition Target { get; set; }
        public IDirection Solution { get; set; }
        internal List<List<IDirectionSolution>> GridOfSolutions { get; set; }
    }
}
