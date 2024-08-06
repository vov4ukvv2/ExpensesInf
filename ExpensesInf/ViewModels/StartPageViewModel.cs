using ExpensesInf.Models;
using ExpensesInf.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExpensesInf.ViewModels
{
    class StartPageViewModel : INotifyPropertyChanged
    {
        #region Field
        #endregion

        #region Property

        public ICommand ViewStatisticsCommand { get; }
        public ICommand AddExpensesCommand { get; }
        public ICommand ViewChecksCommand { get; }

        #endregion

        #region Methods

        public StartPageViewModel()
        {
            ViewStatisticsCommand = new RelayCommand(ViewStatisticsMethod);
            AddExpensesCommand = new RelayCommand(AddExpensesMethod);
            ViewChecksCommand = new RelayCommand(ViewChecksMethod);

            FillData() ;//  I know this is bad code. I added this so that you don't have to fill in the data when you first start it
        }

        private  ExpenseService _expenseService;

        private void FillData()
        {
            _expenseService = new ExpenseService();
            _expenseService.AddCheck(new Check { Amount = 50, Date = DateTime.Now, Description = "Lunch", Type = ExpenseType.Food });
            _expenseService.AddCheck(new Check { Amount = 100, Date = DateTime.Now, Description = "Bus Ticket", Type = ExpenseType.Transport });
            _expenseService.AddCheck(new Check { Amount = 70, Date = DateTime.Now, Description = "Movie", Type = ExpenseType.Entertainment });
            _expenseService.AddCheck(new Check { Amount = 150, Date = DateTime.Now, Description = "Electricity Bill", Type = ExpenseType.Utilities });
            _expenseService.AddCheck(new Check { Amount = 200, Date = DateTime.Now, Description = "Groceries", Type = ExpenseType.Food });

        }
        private void ViewStatisticsMethod()
        {
            ChoiseDiagrams statisticsView = new ChoiseDiagrams();
            statisticsView.ShowDialog();

        }
        private void ViewChecksMethod()
        {
            ViewChecks viewChecks = new ViewChecks();
            viewChecks.ShowDialog();

        }
        private void AddExpensesMethod()
        {
            AddCheck addCheck = new AddCheck();
            addCheck.ShowDialog();

        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
