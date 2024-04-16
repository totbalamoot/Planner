using PlannerLib;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PlannerWpf.ViewModels
{
    public class DayPlanViewModel : BaseViewModel
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

        private Task _selectedTask;
        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                SelectedProject = Account.Projects.First(p => p.Id == _selectedTask.ProjectId);
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                TasksOnDay = Account.GetTasksOnDay(_selectedDate);
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private ObservableCollection<Task> _tasksOnDay;
        public ObservableCollection<Task> TasksOnDay
        {
            get => _tasksOnDay;
            set
            {
                _tasksOnDay = value;
                OnPropertyChanged(nameof(TasksOnDay));
            }
        }

        private CreateTaskViewModel _createTaskViewModel;
        public CreateTaskViewModel CreateTaskViewModel
        {
            get => _createTaskViewModel;
            set
            {
                _createTaskViewModel = value;
                OnPropertyChanged(nameof(CreateTaskViewModel));
            }
        }

        private EditTaskViewModel _editTaskViewModel;
        public EditTaskViewModel EditTaskViewModel
        {
            get => _editTaskViewModel;
            set
            {
                _editTaskViewModel = value;
                OnPropertyChanged(nameof(EditTaskViewModel));
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

        private RelayCommand _openCreateTaskCommand;
        public RelayCommand OpenCreateTaskCommand
        {
            get
            {
                return _openCreateTaskCommand ??
                  (_openCreateTaskCommand = new RelayCommand(obj =>
                  {
                      OpenCreateTaskView();
                  }));
            }
        }

        private RelayCommand _openEditTaskCommand;
        public RelayCommand OpenEditTaskCommand
        {
            get
            {
                return _openEditTaskCommand ??
                  (_openEditTaskCommand = new RelayCommand(obj =>
                  {
                      if (SelectedTask != null)
                      {
                          OpenEditTaskView(SelectedTask);
                      }
                  }));
            }
        }

        private RelayCommand _removeTaskCommand;
        public RelayCommand RemoveTaskCommand
        {
            get
            {
                return _removeTaskCommand ??
                  (_removeTaskCommand = new RelayCommand(obj =>
                  {
                      if (SelectedTask != null)
                      {
                          Account.RemoveTask(SelectedTask);
                      }
                  }));
            }
        }

        public DayPlanViewModel(Account account)
        {
            Account = account;
            SelectedDate = DateTime.Today;
        }

        private void OpenCreateTaskView()
        {
            CreateTaskViewModel = new CreateTaskViewModel(Account, SelectedProject);
            CreateTaskViewModel.Open(MainViewModel, this);
        }

        private void OpenEditTaskView(Task task)
        {
            EditTaskViewModel = new EditTaskViewModel(Account, SelectedProject, task);
            EditTaskViewModel.Open(MainViewModel, this);
        }
    }
}
