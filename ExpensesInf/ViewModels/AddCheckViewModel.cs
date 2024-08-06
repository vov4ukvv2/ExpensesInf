using ExpensesInf.Models;
using ExpensesInf.Views;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExpensesInf.ViewModels
{
    class AddCheckViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ExpenseType type;
        private readonly ExpenseService _expenseService;
        private string _descriptionText;
        private string _amountText;
        private string _selectedChart;
        private bool _isEditMode = false;
        private Check _oldCheck;

        #endregion

        #region Property
        private string _addEditCheck= "Add check";
        public string AddEditCheck
        {
            get { return _addEditCheck; }
            set
            {
                _addEditCheck = value;
                OnPropertyChanged(_addEditCheck);
            }
        }
        public ObservableCollection<string> ChartItemSource { get; set; }
        public ICommand AddNewCheckCommand { get; }
        public string DescriptionText
        {
            get { return _descriptionText; }
            set
            {
                _descriptionText = value;
                OnPropertyChanged(DescriptionText);
            }
        }
        public string AmountText
        {
            get { return _amountText; }
            set
            {
                _amountText = value;
                OnPropertyChanged(AmountText);
            }
        }
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

        #region Method
        public AddCheckViewModel()
        {
            AddNewCheckCommand = new RelayCommand(AddNewCheckMethod);
            _expenseService = new ExpenseService();
            ChartItemSource = new ObservableCollection<String> { "Food", "Transport", "Entertainment", "Utilities", "Other" };
            SelectedChart = ChartItemSource.First();
        }
        public int asdf;
        public AddCheckViewModel(int check)
        {
            AddNewCheckCommand = new RelayCommand(AddNewCheckMethod);
            _expenseService = new ExpenseService();
            ChartItemSource = new ObservableCollection<String> { "Food", "Transport", "Entertainment", "Utilities", "Other" };
            asdf = check;
            var asd = _expenseService.GetAllChecks();
            _oldCheck = asd[check];
            DescriptionText = _oldCheck.Description;
            AmountText = _oldCheck.Amount.ToString();
            SelectedChart = ChartItemSource[(int)_oldCheck.Type];
            _isEditMode = true;
            AddEditCheck = "Edit check";
        }


        public static bool CanConvertDecimal(string input)
        {
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
            {
                string[] parts = input.Split('.');

                if (parts.Length > 1)
                {
                    return parts[1].Length <= 2;
                }

                return true;
            }
            return false;
        }


        private  void AddNewCheckMethod()
        {
            if (!CanConvertDecimal(AmountText))
            {
                MessageBox.Show($"'{AmountText}' is not a valid decimal with at most 2 decimal places.", "Validation Result", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            decimal am;
            decimal.TryParse(AmountText, NumberStyles.Number, CultureInfo.InvariantCulture, out am);
            
            switch (ChartItemSource.IndexOf(SelectedChart))
            {
                case 0:
                    type = ExpenseType.Food;
                    break;
                case 1:
                    type = ExpenseType.Transport;
                    break;
                case 2:
                    type = ExpenseType.Entertainment;
                    break;
                case 3:
                    type = ExpenseType.Utilities;
                    break;
                case 4:
                    type = ExpenseType.Other;
                    break;

            }
            if (_isEditMode)
            {

                 _expenseService.EditCheck(asdf, new Check { Amount = am, Date = DateTime.Now, Description = DescriptionText, Type = type });
                Application.Current.MainWindow.Close();
            }
            else
            {
                 _expenseService.AddCheck(new Check { Amount = am, Date = DateTime.Now, Description = DescriptionText, Type = type });
                Application.Current.MainWindow.Close();
                //ViewChecks viewChecks = new ViewChecks();
                //viewChecks.Show();
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
