using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace utils_libs.Abstractions
{
    public interface IUIPLayer
    {
        IPosition LastPosition { get; set; }
        IPlayer Player { get; }

        void UpdatePosition();
    }
}
