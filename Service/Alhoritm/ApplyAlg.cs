using Kune.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kune.Service.Alhoritm
{
    class ApplyAlg
    {
        private Graph _graph;
        private bool[] visited;
        private int[] ancestor;

        public ApplyAlg(Graph graph)
        {
            _graph = graph;


            visited = new bool[_graph.size];
            ancestor = new int[_graph.size];

            for (int i = 0; i < _graph.size; i++)
            {
                ancestor[i] = -1;
            }
        }

        public async Task<int[]> Main()
        {
            /*for (int i = 0; i < _graph.ribsList.Count; i++)
            {
                if (_graph.ribsList.ContainsKey(i))
                {
                    for (int j = 0; j < _graph.ribsList[i].Count; j++)
                    {
                        visited = new bool[_graph.size];
                        await Kun(_graph.ribsList[i][j]);
                    }
                }
            }*/

            for (int i = 0; i < _graph.size; i++)
            {
                visited = new bool[_graph.size];
                await Kun(i);
            }

            return await Task.FromResult(ancestor);
        }

        private async Task<bool> Kun(int currentVertex)
        {
            if (visited[currentVertex] == false)
            {
                visited[currentVertex] = true;

                for (int i = 0; i < _graph.ribsList[currentVertex].Count; i++)
                {
                    int to = _graph.ribsList[currentVertex][i];
                    if (ancestor[to] == -1 || await Kun(ancestor[to]))
                    {
                        ancestor[to] = currentVertex;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
