using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kune.Models;
using Kune.Service.Alhoritm;

namespace Kune.Service.ConvertData
{
    class GetListService : IGetData
    {
        public async Task<int[]?> ConvertData(string input)  // TODO: Валидация (ввод не связной вершины)
        {
            Graph graph = new Graph();
            graph.ribsList = new Dictionary<int, List<int>>();

            string[] buff = input.Replace("\r\n", " ").Split(" ");

            for (int i = 0; i < buff.Length - 1; i += 2)
            {
                int first;
                if (int.TryParse(buff[i], out first) == false || first <= 0)
                {
                    return null;
                }

                int second;
                if (int.TryParse(buff[i + 1], out second) == false || second <= 0)
                {
                    return null;
                }

                first--;
                second--;
                if (!graph.ribsList.ContainsKey(first))
                {
                    graph.ribsList[first] = new List<int>();
                }
                if (!graph.ribsList.ContainsKey(second))
                {
                    graph.ribsList[second] = new List<int>();
                }

                graph.ribsList[first].Add(second);
                graph.ribsList[second].Add(first);

                if (first > second && first > graph.size)
                {
                    graph.size = first;
                }
                else if (second > first && second > graph.size)
                {
                    graph.size = second;
                }
            }

            for (int i = 0; i < graph.size + 1; i++)
            {
                if (!graph.ribsList.ContainsKey(i))
                {
                    graph.ribsList[i] = new List<int>();
                }
            }
            graph.size++;

            ApplyAlg applyAlg = new ApplyAlg(graph);

            return await applyAlg.Main();
        }
    }
}
