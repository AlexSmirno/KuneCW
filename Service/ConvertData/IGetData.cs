using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kune.Service.ConvertData
{
    public interface IGetData
    {
        Task<int[]> ConvertData(string Input);
     }
}
