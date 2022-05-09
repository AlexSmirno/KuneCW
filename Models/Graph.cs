using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kune.Models
{
    class Graph
    {
        public int size { get; set; }

        public Dictionary<int, List<int>> ribsList { get; set; }
    }
}
