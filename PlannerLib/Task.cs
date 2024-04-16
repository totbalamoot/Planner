using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PlannerLib
{
    public class Task : INotifyPropertyChanged, ICloneable
    {
        public static int s_idCounter;

        private int _id { get; set; }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private DateTime _finishDate { get; set; }
        public DateTime FinishDate
        {
            get => _finishDate;
            set
            {
                _finishDate = value;
                OnPropertyChanged(nameof(FinishDate));
            }
        }

        private int _projectId { get; set; }
        public int ProjectId
        {
            get => _projectId;
            set
            {
                _projectId = value;
                OnPropertyChanged(nameof(ProjectId));
            }
        }

        public Task()
        {
            Id = -1;
        }

        public Task(string name, int projectId)
        {
            Id = s_idCounter++;
            Name = name;
            ProjectId = projectId;
        }

        public void CopyTo(Task task)
        {
            task.Id = Id;
            task.Name = Name;
            task.FinishDate = FinishDate;
            task.ProjectId = ProjectId;
        }

        public object Clone()
        {
            Task task = new Task();
            CopyTo(task);
            return task;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
