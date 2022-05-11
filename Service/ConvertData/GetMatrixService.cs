using Kune.Models;
using Kune.Service.Alhoritm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kune.Service.ConvertData
{
    class GetMatrixService : IGetData
    {
        public async Task<int[]> ConvertData(string input)
        {
            Graph graph = new Graph();
            graph.ribsList = new Dictionary<int, List<int>>();

            string[] buff = input.Split("\r\n");
            for (int i = 0; i < buff.Length; i++)
            {
                graph.ribsList[i] = new List<int>();
            }

            for (int i = 0; i < buff.Length; i++)
            {
                string[] innnerbuff = buff[i].Split(" ");

                for (int j = 0; j < innnerbuff.Length; j++)
                {
                    int vertex;
                    if (int.TryParse(innnerbuff[j], out vertex) == false)
                    {
                        return null;
                    }
                    if (vertex > 0)
                    {
                        graph.ribsList[i].Add(j);
                    }
                }
            }
            graph.size = buff.Length;
            ApplyAlg applyAlg = new ApplyAlg(graph);

            return await applyAlg.Main();
        }

    }
}
