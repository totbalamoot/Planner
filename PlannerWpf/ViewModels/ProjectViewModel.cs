using PlannerLib;

namespace PlannerWpf.ViewModels
{
    public class ProjectViewModel : BaseViewModel
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

        private Project _project;
        public Project Project
        {
            get => _project;
            set
            {
                _project = value;
                OnPropertyChanged(nameof(Project));
            }
        }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
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

        public ProjectViewModel(Account account, Project project)
        {
            Account = account;
            Project = project;
        }

        private void OpenCreateTaskView()
        {
            CreateTaskViewModel = new CreateTaskViewModel(Account, Project);
            CreateTaskViewModel.Open(MainViewModel, this);
        }

        private void OpenEditTaskView(Task task)
        {
            EditTaskViewModel = new EditTaskViewModel(Account, Project, task);
            EditTaskViewModel.Open(MainViewModel, this);
        }
    }
}
