using PlannerLib;
using PlannerWpf.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PlannerWpf
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Account _account;
        public Account Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged(nameof(Account));
            }
        }

        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        private AllProjectsViewModel _allProjectsViewModel;
        public AllProjectsViewModel AllProjectsViewModel
        {
            get => _allProjectsViewModel;
            set
            {
                _allProjectsViewModel = value;
                OnPropertyChanged(nameof(AllProjectsViewModel));
            }
        }

        private DayPlanViewModel _dayPlanViewModel;
        public DayPlanViewModel DayPlanViewModel
        {
            get => _dayPlanViewModel;
            set
            {
                _dayPlanViewModel = value;
                OnPropertyChanged(nameof(DayPlanViewModel));
            }
        }

        private RelayCommand _openAllProjectsCommand;
        public RelayCommand OpenAllProjectsCommand
        {
            get
            {
                return _openAllProjectsCommand ??
                  (_openAllProjectsCommand = new RelayCommand(obj =>
                  {
                      OpenAllProjectsView();
                  }));
            }
        }

        private RelayCommand _openDayPlanCommand;
        public RelayCommand OpenDayPlanCommand
        {
            get
            {
                return _openDayPlanCommand ??
                  (_openDayPlanCommand = new RelayCommand(obj =>
                  {
                      OpenDayPlanView();
                  }));
            }
        }

        public MainViewModel()
        {
            Account = new Account();
            Account.CreateProject("project1");
            Account.CreateProject("project2");
            Account.CreateTask("task1", Account.Projects[0]);
            Account.CreateTask("task2", Account.Projects[0]);

            OpenAllProjectsView();
        }

        private void OpenAllProjectsView()
        {
            AllProjectsViewModel = new AllProjectsViewModel(Account);
            SelectedViewModel = AllProjectsViewModel;
            AllProjectsViewModel.Open(this, AllProjectsViewModel);
        }

        private void OpenDayPlanView()
        {
            DayPlanViewModel = new DayPlanViewModel(Account);
            SelectedViewModel = DayPlanViewModel;
            DayPlanViewModel.Open(this, DayPlanViewModel);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
