using PlannerLib;

namespace PlannerWpf.ViewModels
{
    public class AllProjectsViewModel : BaseViewModel
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

        private ProjectViewModel _projectViewModel;
        public ProjectViewModel ProjectViewModel
        {
            get => _projectViewModel;
            set
            {
                _projectViewModel = value;
                OnPropertyChanged(nameof(ProjectViewModel));
            }
        }

        private CreateProjectViewModel _createProjectViewModel;
        public CreateProjectViewModel CreateProjectViewModel
        {
            get => _createProjectViewModel;
            set
            {
                _createProjectViewModel = value;
                OnPropertyChanged(nameof(CreateProjectViewModel));
            }
        }

        private EditProjectViewModel _editProjectViewModel;
        public EditProjectViewModel EditProjectViewModel
        {
            get => _editProjectViewModel;
            set
            {
                _editProjectViewModel = value;
                OnPropertyChanged(nameof(EditProjectViewModel));
            }
        }

        private RelayCommand _openProjectCommand;
        public RelayCommand OpenProjectCommand
        {
            get
            {
                return _openProjectCommand ??
                  (_openProjectCommand = new RelayCommand(obj =>
                  {
                      if (SelectedProject != null)
                      {
                          OpenProjectView(SelectedProject);
                      }
                  }));
            }
        }

        private RelayCommand _openCreateProjectCommand;
        public RelayCommand OpenCreateProjectCommand
        {
            get
            {
                return _openCreateProjectCommand ??
                  (_openCreateProjectCommand = new RelayCommand(obj =>
                  {
                      OpenCreateProjectView();
                  }));
            }
        }

        private RelayCommand _openEditProjectCommand;
        public RelayCommand OpenEditProjectCommand
        {
            get
            {
                return _openEditProjectCommand ??
                  (_openEditProjectCommand = new RelayCommand(obj =>
                  {
                      if (SelectedProject != null)
                      {
                          OpenEditProjectView(SelectedProject);
                      }
                  }));
            }
        }

        private RelayCommand _removeProjectCommand;
        public RelayCommand RemoveProjectCommand
        {
            get
            {
                return _removeProjectCommand ??
                  (_removeProjectCommand = new RelayCommand(obj =>
                  {
                      if (SelectedProject != null)
                      {
                          Account.RemoveProject(SelectedProject);
                      }
                  }));
            }
        }

        public AllProjectsViewModel(Account account)
        {
            Account = account;
        }

        private void OpenProjectView(Project project)
        {
            ProjectViewModel = new ProjectViewModel(Account, project);
            ProjectViewModel.Open(MainViewModel, this);
        }

        private void OpenCreateProjectView()
        {
            CreateProjectViewModel = new CreateProjectViewModel(Account);
            CreateProjectViewModel.Open(MainViewModel, this);
        }

        private void OpenEditProjectView(Project project)
        {
            EditProjectViewModel = new EditProjectViewModel(Account, project);
            EditProjectViewModel.Open(MainViewModel, this);
        }
    }
}
