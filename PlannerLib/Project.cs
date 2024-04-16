using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PlannerLib
{
    public class Project : INotifyPropertyChanged, ICloneable
    {
        public static int s_idCounter;

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private ObservableCollection<Task> _tasks;
        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        private int _accountId;
        public int AccountId
        {
            get => _accountId;
            set
            {
                _accountId = value;
                OnPropertyChanged(nameof(AccountId));
            }
        }

        private Project() { }

        public Project(string name, int accountId)
        {
            Id = s_idCounter++;
            Name = name;
            Tasks = new ObservableCollection<Task>();
            AccountId = accountId;
        }

        public void CreateTask(string name)
        {
            Tasks.Add(new Task(name, Id));
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public void RemoveTask(Task task)
        {
            if (Tasks.Count != 0)
            {
                Task removedTask = Tasks.First(t => t.Id == task.Id);
                if (removedTask != null)
                {
                    Tasks.Remove(removedTask);
                }
            }
        }

        public void CopyTo(Project project)
        {
            project.Id = Id;
            project.Name = Name;
            project.Tasks = Tasks;
            project.AccountId = AccountId;
        }

        public object Clone()
        {
            Project project = new Project();
            CopyTo(project);
            return project;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
