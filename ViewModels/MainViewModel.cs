using Kune.Service.FilesWriteRead;
using Kune.Service.FilesDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kune.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel()
        {
            CurrentViewModel = new MainPageViewModel(new DefaultDialogService(), new FileService());
        }
    }
}
