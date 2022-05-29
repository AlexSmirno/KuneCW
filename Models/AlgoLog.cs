using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kune.Models
{
    public class AlgoLog
    {
        public int iteraction { get; set; }
        public int cutV { get; set; }
        public string visited { get; set; }
        public int nextV { get; set; }
        public string chain { get; set; }

        public override string ToString()
        {
            return iteraction + " | " + cutV + " | " + visited + " | " + nextV + " | " + chain;
        }
    }
}
