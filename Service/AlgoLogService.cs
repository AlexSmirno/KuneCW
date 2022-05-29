using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kune.Models;

namespace Kune.Service
{
    public class AlgoLogService
    {
        private string path = $"{Environment.CurrentDirectory}\\log.txt";
        private List<AlgoLog> _log;
        public AlgoLogService(List<AlgoLog> log)
        {
            _log = log;
        }

        public async void Logging()
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)))
            {

                await writer.WriteLineAsync($"Новая запись от " + DateTime.Now.ToString("yy/MM/dd/hh/mm/ss") );
                for (int i = 0; i < _log.Count; i++)
                {
                    //File.AppendText(i + ") " + _log[i].ToString());
                    await writer.WriteLineAsync(i + " | " + _log[i].ToString());
                }
            }
        }
    }
}
