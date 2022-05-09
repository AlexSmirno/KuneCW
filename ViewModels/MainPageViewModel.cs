using Kune.Commands;
using Kune.Models;
using Kune.Service;
using Kune.Service.ConvertData;
using Kune.Service.FilesDialogs;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Input;

namespace Kune.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        IDialogService dialogService;
        FileService fileService;

        private string inputGraph;
        public string InputGraph
        {
            get
            {
                return inputGraph;
            }
            set 
            {
                if (!string.Equals(inputGraph, value))
                {
                    inputGraph = value;
                    OnPropertyChanged(nameof(InputGraph));
                }
            } 
        }

        private bool firstCheckbox = false;
        public bool FirstCheckbox
        {
            get
            {
                return firstCheckbox;
            }
            set
            {
                firstCheckbox = value;
                OnPropertyChanged(nameof(FirstCheckbox));
                checkboxArray[0] = firstCheckbox;
            }
        }

        private bool secondCheckbox = false;
        public bool SecondCheckbox
        {
            get
            {
                return secondCheckbox;
            }
            set
            {
                secondCheckbox = value;
                OnPropertyChanged(nameof(SecondCheckbox));
                checkboxArray[1] = secondCheckbox;
            }
        }

        private string briefResult;
        public string BriefResult {
            get
            {
                return briefResult;
            }
            set
            {
                briefResult = "Максимальное паросочетание " + value;
                OnPropertyChanged(nameof(BriefResult));
            }
        }

        private int[] fullResult;
        public int[] FullResult
        {
            get
            {
                return fullResult;
            }
            set
            {
                fullResult = value;
                OnPropertyChanged(nameof(FullResult));
            }
        }
        private ICommand startAlgorithmCommand;
        public ICommand StartAlgorithmCommand
        {
            get
            {
                if (startAlgorithmCommand == null)
                { startAlgorithmCommand = new RelayCommand<object>(MyCommand_Execute); }
                return startAlgorithmCommand;
            }
        }

        private async void MyCommand_Execute(object parameter)
        {
            IGetData getData = new GetListService();

            if (FirstCheckbox) { getData = new GetMatrixService(); }
            if (SecondCheckbox) { getData = new GetListService(); }

            FullResult = await getData.ConvertData(InputGraph);
            BriefResult = FullResult.Where(item => item != -1).ToArray().Length.ToString();
        }

        private ICommand inputCommand;
        public ICommand InputCommand
        {
            get
            {
                if (inputCommand == null)
                { inputCommand = new RelayCommand<object>(InputCommand_Execute); }
                return inputCommand;
            }
        }

        private async void InputCommand_Execute(object parameter)
        {
            try
            {
                if (dialogService.OpenFileDialog() == true)
                {
                    string input = await fileService.OpenAsync(dialogService.FilePath);

                    IGetData getData = new GetListService();
                    if (FirstCheckbox) { getData = new GetMatrixService(); }
                    if (SecondCheckbox) { getData = new GetListService(); }
                    FullResult = await getData.ConvertData(input);

                    FullResult = FullResult.Where(item => item != -1).ToArray();
                    BriefResult = FullResult.Where(item => item != -1).ToArray().Length.ToString();

                    await dialogService.ShowMessage("Файл открыт!");
                }
            }
            catch (Exception ex)
            {
                await dialogService.ShowMessage(ex.Message);
            }
        }

        private ICommand outputCommand;
        public ICommand OutputCommand
        {
            get
            {
                if (outputCommand == null)
                { outputCommand = new RelayCommand<object>(OutputCommand_Execute); }
                return outputCommand;
            }
        }

        private async void OutputCommand_Execute(object parameter)
        {
            try
            {
                if (dialogService.SaveFileDialog() == true)
                {
                    await fileService.Save(dialogService.FilePath, FullResult);

                    await dialogService.ShowMessage("Файл сохранен!");
                }
            }
            catch (Exception ex)
            {
                await dialogService.ShowMessage(ex.Message);
            }
        }

        private bool[] checkboxArray = new bool[2];

        public MainPageViewModel(IDialogService dialogService, FileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }

        public int SelectedMode(bool[] checkboxArray)
        {
            return Array.IndexOf(checkboxArray, true) + 1; 
        }
    }
}
