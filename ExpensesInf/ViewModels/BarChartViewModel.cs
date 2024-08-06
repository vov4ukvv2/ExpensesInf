using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ExpensesInf.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ExpensesInf.ViewModels
{
    internal class BarChartViewModel : INotifyPropertyChanged
    {
        private readonly ExpenseService _expenseService;
        private ObservableCollection<string> _checkList;
        private string _otherCountText;
        private string _foodCountText;
        private string _entertainmentCountText;
        private string _transportCountText;
        private string _utilitiesCountText;

        public ICommand OtherClickFilterCommand { get; }
        public ICommand EntertainmentClickFilterCommand { get; }
        public ICommand TransportClickFilterCommand { get; }
        public ICommand UtilitiesClickFilterCommand { get; }
        public ICommand FoodClickFilterCommand { get; }
        public PlotModel ExpensePlotModel { get; set; }

        public ObservableCollection<string> CheckList
        {
            get => _checkList;
            set
            {
                _checkList = value;
                OnPropertyChanged(nameof(CheckList));
            }
        }
        public string OtherCountText
        {
            get => _otherCountText;
            set
            {
                _otherCountText = value;
                OnPropertyChanged(nameof(OtherCountText));
            }
        }
        public string TransportCountText
        {
            get => _transportCountText;
            set
            {
                _transportCountText = value;
                OnPropertyChanged(nameof(TransportCountText));
            }
        }
        public string EntertainmentCountText
        {
            get => _entertainmentCountText;
            set
            {
                _entertainmentCountText = value;
                OnPropertyChanged(nameof(EntertainmentCountText));
            }
        }
        public string UtilitiesCountText
        {
            get => _utilitiesCountText;
            set
            {
                _utilitiesCountText = value;
                OnPropertyChanged(nameof(UtilitiesCountText));
            }
        }
        public string FoodCountText
        {
            get => _foodCountText;
            set
            {
                _foodCountText = value;
                OnPropertyChanged(nameof(FoodCountText));
            }
        }

        public BarChartViewModel()
        {
            UtilitiesClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Utilities));
            FoodClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Food));
            EntertainmentClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Entertainment));
            OtherClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Other));
            TransportClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Transport));
            _expenseService = new ExpenseService();

            InitializeData();
        }

        private void InitializeData()
        {
            var checks = _expenseService.GetAllChecks();

            var groupedData = checks.GroupBy(c => c.Type)
                                    .Select(g => new ExpenseSummary
                                    {
                                        Type = g.Key,
                                        TotalAmount = g.Sum(c => c.Amount)
                                    }).ToList();

            ExpensePlotModel = new PlotModel { Title = "Expenses" };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };  // Now CategoryAxis on Y axis
            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "Amount" }; // LinearAxis on X axis

            var barSeries = new BarSeries { Title = "Expenses", FillColor = OxyColors.SkyBlue };

            foreach (var item in groupedData)
            {
                categoryAxis.Labels.Add(item.Type.ToString());
                barSeries.Items.Add(new BarItem { Value = (double)item.TotalAmount });
            }

            ExpensePlotModel.Axes.Add(categoryAxis);
            ExpensePlotModel.Axes.Add(valueAxis);
            ExpensePlotModel.Series.Add(barSeries);

            UtilitiesCountText = _expenseService.GetTotalExpensesByType(ExpenseType.Utilities).ToString();
            FoodCountText = _expenseService.GetTotalExpensesByType(ExpenseType.Food).ToString();
            TransportCountText = _expenseService.GetTotalExpensesByType(ExpenseType.Transport).ToString();
            EntertainmentCountText = _expenseService.GetTotalExpensesByType(ExpenseType.Entertainment).ToString();
            OtherCountText = _expenseService.GetTotalExpensesByType(ExpenseType.Other).ToString();
            CheckList = new ObservableCollection<string>(checks.Select(c => $"{c.Date}  {c.Description} {c.Type} ${c.Amount:0.00}"));
        }

        private void FillCheckList(ExpenseType expenseType)
        {
            var checks = _expenseService.GetChecksByType(expenseType);
            CheckList = new ObservableCollection<string>(checks.Select(c => $"{c.Date}  {c.Description} {c.Type} ${c.Amount:0.00}"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
