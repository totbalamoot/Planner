using PlannerLib;

namespace PlannerWpf.ViewModels
{
    public class EditTaskViewModel : BaseViewModel
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

        private Task _editedTask;
        public Task EditedTask
        {
            get => _editedTask;
            set
            {
                _editedTask = value;
                OnPropertyChanged(nameof(EditedTask));
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

        private RelayCommand _editTaskCommand;
        public RelayCommand EditTaskCommand
        {
            get
            {
                return _editTaskCommand ??
                  (_editTaskCommand = new RelayCommand(obj =>
                  {
                      if (EditedTask.Name != string.Empty)
                      {
                          Account.EditTask(EditedTask, SelectedProject);
                          Close();
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
                      Account.RemoveTask(EditedTask);
                      Close();
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
                      OpenSetDateView();
                  }));
            }
        }

        public EditTaskViewModel(Account account, Project selectedProject, Task task)
        {
            Account = account;
            SelectedProject = selectedProject;
            EditedTask = (Task)task.Clone();
        }

        private void OpenSetDateView()
        {
            SetDateViewModel = new SetDateViewModel(EditedTask);
            SetDateViewModel.Open(MainViewModel, this);
        }
    }
}
