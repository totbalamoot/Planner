using PlannerLib;
using System;

namespace PlannerWpf.ViewModels
{
    public class CreateTaskViewModel : BaseViewModel
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

        private Project _selectedProject;
        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }

        private Task _createdTask { get; set; }
        public Task CreatedTask
        {
            get => _createdTask;
            set
            {
                _createdTask = value;
                OnPropertyChanged(nameof(CreatedTask));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private SetDateViewModel _setDateViewModel;
        public SetDateViewModel SetDateViewModel
        {
            get => _setDateViewModel;
            set
            {
                _setDateViewModel = value;
                OnPropertyChanged(nameof(SetDateViewModel));
            }
        }

        private RelayCommand _backCommand;
        public RelayCommand BackCommand
        {
            get
            {
                return _backCommand ??
                  (_backCommand = new RelayCommand(obj =>
                  {
                      Close();
                  }));
            }
        }

        private RelayCommand _createTaskCommand;
        public RelayCommand CreateTaskCommand
        {
            get
            {
                return _createTaskCommand ??
                  (_createTaskCommand = new RelayCommand(obj =>
                  {
                      if ((CreatedTask.Name != string.Empty) && (SelectedProject != null))
                      {
                          Account.CreateTask(CreatedTask, SelectedProject);
                          Close();
                      }
                  }));
            }
        }

        private RelayCommand _openSetDateCommand;
        public RelayCommand OpenSetDateCommand
        {
            get
            {
                return _openSetDateCommand ??
                  (_openSetDateCommand = new RelayCommand(obj =>
                  {
                      CreatedTask.FinishDate = SelectedDate;
                      OpenSetDateView();
                  }));
            }
        }

        public CreateTaskViewModel(Account account, Project selectedProject)
        {
            Account = account;
            SelectedProject = selectedProject;
            CreatedTask = new Task();
            CreatedTask.Name = string.Empty;
            CreatedTask.FinishDate = DateTime.Today;
            SelectedDate = CreatedTask.FinishDate;
        }

        private void OpenSetDateView()
        {
            SetDateViewModel = new SetDateViewModel(CreatedTask);
            SetDateViewModel.Open(MainViewModel, this);
        }
    }
}
