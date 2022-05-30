using Kune.Models;
using Kune.Service.FilesWriteRead;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<AlgoLog> log;
        private int iteraction = 0;

        public ApplyAlg(Graph graph)
        {
            log = new List<AlgoLog>();
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
            foreach (KeyValuePair<int, List<int>> riblistForOne in _graph.ribsList)
            {
                visited = new bool[_graph.size];
                iteraction++;
                await Kun(riblistForOne.Key);
            }

            AlgoLogService algoLogService = new AlgoLogService(log);
            algoLogService.Logging();

            return await Task.FromResult(ancestor);
        }

        private async Task<bool> Kun(int currentVertex)
        {
            log.Add(new AlgoLog() { iteraction = this.iteraction, cutV = currentVertex + 1}); // LOG

            if (visited[currentVertex] == false)
            {
                visited[currentVertex] = true;

                log[log.Count - 1].visited = "visited"; // LOG

                for (int i = 0; i < _graph.ribsList[currentVertex].Count; i++)
                {
                    int to = _graph.ribsList[currentVertex][i];
                    
                    log[log.Count - 1].nextV = to + 1; // LOG

                    if ((ancestor[to] == -1) || await Kun(ancestor[to]))
                    {
                        ancestor[to] = currentVertex;
                        if (log[log.Count - 1].chain == null) // LOG
                        {
                            log[log.Count - 1].chain = (to + 1) + " " + (currentVertex + 1); // LOG
                        }
                        else
                        {
                            log.Add(new AlgoLog() { cutV = currentVertex + 1 , 
                                iteraction = log[log.Count - 1].iteraction, 
                                nextV = to + 1, visited = log[log.Count - 1].visited} ); // LOG
                            log[log.Count - 1].chain = to + 1 + " " + (currentVertex + 1); // LOG
                        }
                        return true;
                    }

                    log.Add(new AlgoLog() { iteraction = this.iteraction, cutV = currentVertex + 1, visited = "visited" }); // LOG

                }
            }

            log[log.Count - 1].visited = "already visited"; // LOG

            return false;

        }

    }
}
