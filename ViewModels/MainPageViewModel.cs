using Kune.Commands;
using Kune.Models;
using Kune.Service;
using System;
using System.Windows;
using System.Windows.Input;

namespace Kune.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
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

        private string briefResult = "Максимальное паросочетание";
        public string BriefResult {
            get
            {
                return briefResult;
            }
            set
            {
                briefResult = value;
                OnPropertyChanged(nameof(BriefResult));
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
            GetDataService getData = new GetDataService();
            await getData.GetData(InputGraph, SelectedMode(checkboxArray));
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
            GetDataService getData = new GetDataService();

        }

        private bool[] checkboxArray = new bool[2];

        public MainPageViewModel()
        {

        }

        public int SelectedMode(bool[] checkboxArray)
        {
            return (Array.IndexOf(checkboxArray, true) + 1); 
        }
    }
}
