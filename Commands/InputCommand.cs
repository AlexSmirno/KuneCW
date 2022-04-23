using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kune.Service;

namespace Kune.Commands
{
    class InputCommand : CommandBase
    {
        string path;
        int mode;
        public override void Execute(object parameter)
        {
            GetDataService getData = new GetDataService();
        }

        public InputCommand(string path, int mode)
        {
            this.path = path;
            this.mode = mode;
        }
    }
}
