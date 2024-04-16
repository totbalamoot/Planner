using PlannerLib;

namespace PlannerWpf.ViewModels
{
    public class EditProjectViewModel : BaseViewModel
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

        private Project _editedProject;
        public Project EditedProject
        {
            get => _editedProject;
            set
            {
                _editedProject = value;
                OnPropertyChanged(nameof(EditedProject));
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
        private RelayCommand _editProjectCommand;
        public RelayCommand EditProjectCommand
        {
            get
            {
                return _editProjectCommand ??
                  (_editProjectCommand = new RelayCommand(obj =>
                  {
                      if (EditedProject.Name != string.Empty)
                      {
                          Account.EditProject(EditedProject);
                          Close();
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

                      Account.RemoveProject(EditedProject);
                      Close();
                  }));
            }
        }

        public EditProjectViewModel(Account account, Project project)
        {
            Account = account;
            EditedProject = (Project)project.Clone();
        }
    }
}
