using Kune.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kune.Service
{
    class GetDataService
    {
        public async Task<int> GetData(string Input, int mode)
        {
            int result = 1;
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

            switch (mode)
            {
                case 1:
                    graph = await FromMatrix(Input);
                    break;
                case 2:
                    graph = await FromList(Input);
                    break;
            }

            return await Task.FromResult(result);
        }

        public async Task<int> GetData(Stream Input, int mode)
        {
            int result = 1;
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            string input = "";

            switch (mode)
            {
                case 1:
                    graph = await FromMatrix(input);
                    break;
                case 2:
                    graph = await FromList(input);
                    break;
            }
            return await Task.FromResult(result);
        }

        private async Task<Dictionary<int, List<int>>> FromMatrix(string input)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

            string[] buff = input.Split("\r\n");
            for (int i = 0; i < buff.Length; i++)
            {
                graph[i + 1] = new List<int>();
                string[] innnerbuff = buff[i].Split(" ");
                for (int j = 0; j < innnerbuff.Length; j++)
                {
                    int vertex = int.Parse(innnerbuff[j]);   // TODO: Валидация
                    if (vertex > 0)
                    {
                        graph[i + 1].Add(vertex);
                    }
                }
            }
            return await Task.FromResult(graph);
        }

        private async Task<Dictionary<int, List<int>>> FromList(string input)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

            string[] buff = input.Replace("\r\n", " ").Split(" ");

            int number = int.Parse(buff[0]);
            for (int i = 1; i < number + 1; i++)
            {
                graph[i] = new List<int>();
            }
            for (int i = 1; i < buff.Length - 1; i+=2)
            {
                int first = int.Parse(buff[i]);  // TODO: Валидация
                int second = int.Parse(buff[i + 1]);  // TODO: Валидация

                graph[first].Add(second);
            }
            return await Task.FromResult(graph);
        }
    }
}
