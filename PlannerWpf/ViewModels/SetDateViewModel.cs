using PlannerLib;
using System;
using System.Collections.Generic;

namespace PlannerWpf.ViewModels
{
    public class SetDateViewModel : BaseViewModel
    {
        private Task _task;
        public Task Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged(nameof(Task));
            }
        }

        private DateTime _senderDateTime;
        public DateTime SenderDateTime
        {
            get => _senderDateTime;
            set
            {
                _senderDateTime = value;
                OnPropertyChanged(nameof(SenderDateTime));
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

        private int _selectedHour;
        public int SelectedHour
        {
            get => _selectedHour;
            set
            {
                _selectedHour = value;
                OnPropertyChanged(nameof(SelectedHour));
            }
        }

        private int _selectedMinute;
        public int SelectedMinute
        {
            get => _selectedMinute;
            set
            {
                _selectedMinute = value;
                OnPropertyChanged(nameof(SelectedMinute));
            }
        }

        private IEnumerable<int> _hoursList;
        public IEnumerable<int> HoursList
        {
            get => _hoursList;
            set
            {
                _hoursList = value;
                OnPropertyChanged(nameof(HoursList));
            }
        }

        private IEnumerable<int> _minutesList;
        public IEnumerable<int> MinutesList
        {
            get => _minutesList;
            set
            {
                _minutesList = value;
                OnPropertyChanged(nameof(MinutesList));
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

        private RelayCommand _setDateTimeCommand;
        public RelayCommand SetDateTimeCommand
        {
            get
            {
                return _setDateTimeCommand ??
                  (_setDateTimeCommand = new RelayCommand(obj =>
                  {
                      DateTime newDateTime = new DateTime(
                          SelectedDate.Year,
                          SelectedDate.Month, 
                          SelectedDate.Day, 
                          SelectedHour, 
                          SelectedMinute,
                          0);
                      Task.FinishDate = newDateTime;

                      Close();
                  }));
            }
        }

        public SetDateViewModel(Task task)
        {
            Task = task;
            SelectedDate = Task.FinishDate;
            SelectedHour = task.FinishDate.Hour;
            SelectedMinute = task.FinishDate.Minute;

            HoursList = GenerateTo(24);
            MinutesList = GenerateTo(60);
        }

        private IEnumerable<int> GenerateTo(int finalNum)
        {
            for (int i = 0; i < finalNum; i++)
            {
                yield return i;
            }
        }
    }
}
