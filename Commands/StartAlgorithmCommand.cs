using Kune.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kune.Commands
{
    class StartAlgorithmCommand : CommandBase
    {
        private string input;
        private int mode;
        private string briefResult;

        public override void Execute(object parameter)
        {
            GetDataService getData = new GetDataService();
            briefResult += getData.GetData(input, mode);
        }

        public StartAlgorithmCommand(object parameter = null)
        {

        }
    }
}
