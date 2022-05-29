using Kune.Commands;
using Kune.Service;
using Kune.Service.ConvertData;
using Kune.Service.FilesDialogs;
using System;
using System.Linq;
using System.Windows.Input;
using System.Diagnostics;

namespace Kune.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        IDialogService dialogService;
        FileService fileService;
        private Stopwatch Time = new Stopwatch();

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

        private bool secondCheckbox = true;
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

        private string algoTime = "Время работы алгоритма: ";
        public string AlgoTime
        {
            get
            {
                return algoTime;
            }
            set
            {
                algoTime = "Время работы алгоритма: " + value;
                OnPropertyChanged(nameof(AlgoTime));
            }
        }

        private bool isFullRecord;
        public bool IsFullRecord
        {
            get
            {
                return isFullRecord;
            }
            set
            {
                isFullRecord = value;
                OnPropertyChanged(nameof(IsFullRecord));
            }
        }

        private string briefResult = "Максимальное паросочетание: ";
        public string BriefResult {
            get
            {
                return briefResult;
            }
            set
            {
                briefResult = "Максимальное паросочетание: " + value;
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
                {
                    startAlgorithmCommand = new RelayCommand<object>(MyCommand_Execute);
                }
                return startAlgorithmCommand;
            }
        }

        private async void MyCommand_Execute(object parameter)
        {
            IGetData getData = new GetListService();
            try
            {
                if (FirstCheckbox) { getData = new GetMatrixService(); }
                if (SecondCheckbox) { getData = new GetListService(); }
                Time.Restart();
                FullResult = await getData.ConvertData(InputGraph);
                Time.Stop();
                AlgoTime = Time.Elapsed.TotalMilliseconds.ToString();
                FullResult = FullResult.ToArray();
                BriefResult = FullResult.Where(item => item != -1).ToArray().Length.ToString();
            }
            catch (Exception ex)
            {
                await dialogService.ShowMessage(ex.Message);
            }
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

                    Time.Restart();
                    AlgoTime = Time.Elapsed.TotalMilliseconds.ToString();
                    Time.Stop();
                    FullResult = await getData.ConvertData(input);
                    FullResult = FullResult.Where(item => item != -1).ToArray();
                    BriefResult = FullResult.Length.ToString();

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
                    if (IsFullRecord == true)
                    {
                        await fileService.SaveWithRecord(dialogService.FilePath, FullResult, int.Parse(BriefResult.Split()[2]), Time.Elapsed.TotalMilliseconds);
                    }
                    else
                    {
                        await fileService.Save(dialogService.FilePath, FullResult);
                    }

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
    }
}
