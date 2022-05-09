using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Kune.Service.FilesDialogs
{
    class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }

            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;

            }
            return false;
        }

        public async Task ShowMessage(string message)
        {
            await Task.FromResult(MessageBox.Show(message));
        }
    }
}
