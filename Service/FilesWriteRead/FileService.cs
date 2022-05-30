using System;
using System.IO;
using System.Threading.Tasks;

namespace Kune.Service.FilesWriteRead
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

        public async Task SaveWithRecord (string filename, int[] result, int maxPar, double time)
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                await writer.WriteLineAsync("Отчет");
                await writer.WriteLineAsync("Время работы алгоритма примерно = " + time + " секунд");
                await writer.WriteLineAsync("Максимальное паросочетание: " + maxPar);
                await writer.WriteLineAsync("Вариант паросочетания:");

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] >= 0)
                    {
                        await writer.WriteLineAsync((i + 1) + " " + (result[i] + 1));
                    }
                }
            }
        }
    }
}
