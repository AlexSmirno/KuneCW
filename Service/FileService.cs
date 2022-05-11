using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kune.Service
{
    class FileService
    {
        public async Task<string> OpenAsync(string filename)
        {
            string input = "";
            using (StreamReader reader = new StreamReader(filename))
            {
                input += await reader.ReadToEndAsync();
            }
            return await Task.FromResult(input);
        }

        public async Task Save (string filename, int[] result)
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] >= 0)
                    {
                        await writer.WriteLineAsync((i + 1)+ " " + (result[i] + 1));
                    }
                }
            }
        }
    }
}
