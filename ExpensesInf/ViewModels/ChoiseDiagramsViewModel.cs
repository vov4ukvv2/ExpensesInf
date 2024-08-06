using ExpensesInf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExpensesInf.ViewModels
{

    class ChoiseDiagramsViewModel : INotifyPropertyChanged
    {
        #region Field

        private string _selectedChart;

        #endregion

        #region Property

        public ICommand OpenDiagramsCommand { get; }
        public ObservableCollection<string> ChartItemSource { get; set; }
        public string SelectedChart
        {
            get { return _selectedChart; }
            set
            {
                _selectedChart = value;
                OnPropertyChanged(nameof(SelectedChart));


            }
        }

        #endregion

        #region Methods
        public ChoiseDiagramsViewModel()
        {
            ChartItemSource = new ObservableCollection<String> {  "BarСhart", "PieСhart" };
            SelectedChart = ChartItemSource.First();
            OpenDiagramsCommand = new RelayCommand(ExecuteAction);
        }
        private void ExecuteAction()
        {

            switch (ChartItemSource.IndexOf(SelectedChart))
            {
            
                case 0:
                    BarChart barChart = new BarChart();
                    Application.Current.MainWindow.Close();
                    barChart.Show();
                    
                    break;
                case 1:
                    StatisticsView statisticsView = new StatisticsView();
                    Application.Current.MainWindow.Close();
                    statisticsView.Show();

                    break;
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
