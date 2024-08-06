using ExpensesInf.Models;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExpensesInf.ViewModels
{
    class StatisticsViewModel : INotifyPropertyChanged
    {
        #region Field

        private readonly ExpenseService _expenseService;
        private ObservableCollection<string> _checkList;
        private string _otherCountText;
        private string _foodCountText;
        private string _entertainmentCountText;
        private string _transportCountText;
        private string _utilitiesCountText;

        #endregion

        #region Property

        public ICommand OtherClickFilterCommand { get; }
        public ICommand EntertainmentClickFilterCommand { get; }
        public ICommand TransportClickFilterCommand { get; }
        public ICommand UtilitiesClickFilterCommand { get; }
        public ICommand FoodClickFilterCommand { get; }
        public PlotModel ExpensePlotModel { get; set; }
        public ObservableCollection<PieSlice> ExpensePieSeries { get; set; }
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

            get { return _otherCountText; }
            set
            {
                _otherCountText = value;
                OnPropertyChanged(OtherCountText);
            }
        }
        public string TransportCountText
        {

            get { return _transportCountText; }
            set
            {
                _transportCountText = value;
                OnPropertyChanged(TransportCountText);
            }
        }
        public string EntertainmentCountText
        {

            get { return _entertainmentCountText; }
            set
            {
                _entertainmentCountText = value;
                OnPropertyChanged(EntertainmentCountText);
            }
        }

        public string UtilitiesCountText
        {

            get { return _utilitiesCountText; }
            set
            {
                _utilitiesCountText = value;
                OnPropertyChanged(UtilitiesCountText);
            }
        }
        public string FoodCountText
        {

            get { return _foodCountText; }
            set
            {
                _foodCountText = value;
                OnPropertyChanged(FoodCountText);
            }
        }
       
        #endregion

        #region Methods
        public StatisticsViewModel()
        {

            UtilitiesClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Utilities));
            FoodClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Food));
            EntertainmentClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Entertainment));
            OtherClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Other));
            TransportClickFilterCommand = new RelayCommand(() => FillCheckList(ExpenseType.Transport));
            _expenseService = new ExpenseService();

            InitializeData();
        }
        private  void InitializeData()
        {
            var checks =  _expenseService.GetAllChecks();

            // Обробити дані після отримання
            var groupedData = checks.GroupBy(c => c.Type)
                                    .Select(g => new ExpenseSummary
                                    {
                                        Type = g.Key,
                                        TotalAmount = g.Sum(c => c.Amount)
                                    }).ToList();
            ExpensePieSeries = new ObservableCollection<PieSlice>();
            foreach (var item in groupedData)
            {
                ExpensePieSeries.Add(new PieSlice(item.Type.ToString(), (double)item.TotalAmount));
            }
            ExpensePlotModel = new PlotModel { };
            var pieSeries = new PieSeries();
            foreach (var slice in ExpensePieSeries)
            {
                pieSeries.Slices.Add(slice);
            }
            ExpensePlotModel.Series.Add(pieSeries);

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
       
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}