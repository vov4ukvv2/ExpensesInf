using ExpensesInf.Models;
using ExpensesInf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExpensesInf.ViewModels
{

    class ViewChecksViewModel : INotifyPropertyChanged
    {
        private  ExpenseService _expenseService;
        private Check _selectedCheck;

        private ObservableCollection<Check> _checks;


        public ObservableCollection<Check> Checks
        {
            get => _checks;
            set
            {
                _checks = value;
                OnPropertyChanged();
            }
        }

        public Check SelectedCheck
        {
            get => _selectedCheck;
            set
            {
                _selectedCheck = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddCheckCommand { get; }
        public ICommand EditCheckCommand { get; }
        public ICommand DeleteCheckCommand { get; }
        public ViewChecksViewModel()
        {
            _expenseService = new ExpenseService();
            Checks = new ObservableCollection<Check>(_expenseService.GetAllChecks());

            AddCheckCommand = new RelayCommand(AddCheck);
            EditCheckCommand = new RelayCommand(async () => await EditCheck(), () => SelectedCheck != null);
            DeleteCheckCommand = new RelayCommand( DeleteCheck);
        }
        private void AddCheck()
        {
            AddCheck addCheck = new AddCheck();
            addCheck.ShowDialog();
            _expenseService = new ExpenseService();
            Checks = new ObservableCollection<Check>(_expenseService.GetAllChecks());


        }
        private async Task EditCheck()
        {
            if (SelectedCheck != null)
            {
                var SelectedCheckIndex = Checks.IndexOf(SelectedCheck);
                AddCheck editCheck = new AddCheck(SelectedCheckIndex);
                editCheck.ShowDialog();
                _expenseService = new ExpenseService();
                Checks = new ObservableCollection<Check>(_expenseService.GetAllChecks());
            }
        }
        private void DeleteCheck()
        {
            if (SelectedCheck != null)
            {
                 _expenseService.DeleteCheck(SelectedCheck);
                Checks.Remove(SelectedCheck);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
