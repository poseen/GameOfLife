using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public struct Life
    {
        public bool Alive { get; set; }

        public int NumberOfLivingNeighbours { get; set; }
    }
}
