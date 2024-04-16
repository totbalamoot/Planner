using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PlannerWpf.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private MainViewModel _mainViewModel;
        public MainViewModel MainViewModel
        {
            get => _mainViewModel;
            set
            {
                _mainViewModel = value;
                OnPropertyChanged(nameof(MainViewModel));
            }
        }

        private BaseViewModel _backViewModel;
        public BaseViewModel BackViewModel
        {
            get => _backViewModel;
            set
            {
                _backViewModel = value;
                OnPropertyChanged(nameof(BackViewModel));
            }
        }

        public virtual void Open(MainViewModel mainViewModel, BaseViewModel backViewModel)
        {
            MainViewModel = mainViewModel;
            MainViewModel.SelectedViewModel = this;
            BackViewModel = backViewModel;
        }

        public virtual void Close()
        {
            MainViewModel.SelectedViewModel = BackViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
