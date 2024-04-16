using PlannerLib;

namespace PlannerWpf.ViewModels
{
    public class CreateProjectViewModel : BaseViewModel
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

        private string _projectName { get; set; }
        public string ProjectName
        {
            get => _projectName;
            set
            {
                _projectName = value;
                OnPropertyChanged(nameof(ProjectName));
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
        private RelayCommand _createProjectCommand;
        public RelayCommand CreateProjectCommand
        {
            get
            {
                return _createProjectCommand ??
                  (_createProjectCommand = new RelayCommand(obj =>
                  {
                      if (ProjectName != string.Empty)
                      {
                          Account.CreateProject(ProjectName);
                          Close();
                      }
                  }));
            }
        }

        public CreateProjectViewModel(Account account)
        {
            Account = account;
            ProjectName = string.Empty;
        }
    }
}
