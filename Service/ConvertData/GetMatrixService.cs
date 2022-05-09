using Kune.Models;
using Kune.Service.Alhoritm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

                string[] innnerbuff = buff[i].Split(" ");

                for (int j = i; j < innnerbuff.Length; j++)
                {
                    int vertex = int.Parse(innnerbuff[j]);  // TODO: Валидация
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
