using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PlannerLib
{
    public class Account : INotifyPropertyChanged
    {
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

        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        public Account()
        {
            Tasks = new ObservableCollection<Task>();
            Projects = new ObservableCollection<Project>();
        }

        public void CreateTask(string name, Project project)
        {
            Task task = new Task(name, project.Id);
            Tasks.Add(task);
            project.AddTask(task);
        }

        public void CreateTask(Task task, Project project)
        {
            bool isContainsTask = Tasks.Any(t => t.Id == task.Id);
            bool isContainsProject = Projects.Any(p => p.Id == project.Id);

            if (!isContainsTask && isContainsProject)
            {
                Task newTask = new Task();
                task.CopyTo(newTask);
                newTask.Id = Task.s_idCounter++;
                newTask.ProjectId = project.Id;

                Tasks.Add(newTask);

                Project editedProject = Projects.First(p => p.Id == project.Id);
                editedProject.AddTask(newTask);
            }
        }

        public void EditTask(Task task)
        {
            Task editedTask = Tasks.First(t => t.Id == task.Id);
            if (editedTask != null)
            {
                task.CopyTo(editedTask);
            }
        }

        public void EditTask(Task task, Project project)
        {
            bool isContainsTask = Tasks.Any(t => t.Id == task.Id);
            bool isContainsProject = Projects.Any(p => p.Id == project.Id);

            if (isContainsTask && isContainsProject)
            {
                Project editedProject = Projects.First(p => p.Id == task.ProjectId);
                Task editedTask = editedProject.Tasks.First(t => t.Id == task.Id);

                Project newProject = Projects.First(p => p.Id == project.Id);

                task.CopyTo(editedTask);
                editedTask.ProjectId = newProject.Id;

                newProject.AddTask(editedTask);
                editedProject.RemoveTask(editedTask);

                Task oldTask = Tasks.First(t => t.Id == task.Id);
                editedTask.CopyTo(oldTask);

            }
        }

        public void RemoveTask(Task task)
        {
            Task removedTask = Tasks.First(t => t.Id == task.Id);
            if (removedTask != null)
            {
                Project project = Projects.First(p => p.Id == removedTask.ProjectId);
                project.Tasks.Remove(removedTask);
                Tasks.Remove(removedTask);
            }
        }

        public void CreateProject(string name)
        {
            Projects.Add(new Project(name, Id));
        }

        public void EditProject(Project project)
        {
            Project editedProject = Projects.First(p => p.Id == project.Id);
            if (editedProject != null)
            {
                project.CopyTo(editedProject);
            }
        }

        public void RemoveProject(Project project)
        {
            Project removedProject = Projects.First(p => p.Id == project.Id);
            if (removedProject != null)
            {
                foreach (var task in removedProject.Tasks)
                {
                    Tasks.Remove(task);
                }
                removedProject.Tasks.Clear();
                Projects.Remove(removedProject);
            }
        }

        public ObservableCollection<Task> GetTasksOnDay(DateTime date)
        {
            ObservableCollection<Task> newTasks = new ObservableCollection<Task>();
            foreach (var task in Tasks)
            {
                if (task.FinishDate.Date == date.Date)
                {
                    newTasks.Add(task);
                }
            }
            return newTasks; 
        }

        public ObservableCollection<Task> GetTasksOnToday()
        {
            return GetTasksOnDay(DateTime.Today);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
