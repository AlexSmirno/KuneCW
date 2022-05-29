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
            if (input == null)
            {
                throw new NullReferenceException("Возможно, вы не ввели граф");
            }
            Graph graph = new Graph();
            graph.ribsList = new Dictionary<int, List<int>>();

            string[] buff = input.Replace("\r\n", " ").Split(" ");

            if (buff.Length % 2 != 0)
            {
                throw new FormatException("Неверный формат для списка смежности");
            }

            for (int i = 0; i < buff.Length - 1; i += 2)
            {
                int first;
                if (int.TryParse(buff[i], out first) == false || first <= 0)
                {
                    throw new FormatException("Неверный формат для списка смежности");
                }

                int second;
                if (int.TryParse(buff[i + 1], out second) == false || second <= 0)
                {
                    throw new FormatException("Неверный формат для списка смежности");
                }

                first--;
                second--;
                if (!graph.ribsList.ContainsKey(first))
                {
                    graph.ribsList[first] = new List<int>();
                }

                if (graph.ribsList.ContainsKey(second))
                {
                    throw new FormatException("Возможно, одна из вершин находится в нескольких долях");
                }

                foreach (KeyValuePair<int, List<int>> riblist in graph.ribsList)
                {
                    if (riblist.Value.Contains(first))
                    {
                        throw new FormatException("Возможно, одна из вершин находится в нескольких долях");
                    }
                }

                graph.ribsList[first].Add(second);
                
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
