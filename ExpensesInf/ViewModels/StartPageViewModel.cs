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
