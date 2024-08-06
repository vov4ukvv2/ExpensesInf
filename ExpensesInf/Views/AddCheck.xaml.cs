using ExpensesInf.Models;
using ExpensesInf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExpensesInf.Views
{
    /// <summary>
    /// Interaction logic for AddCheck.xaml
    /// </summary>
    public partial class AddCheck : Window
    {
        public AddCheck()
        {
            InitializeComponent();
            DataContext = new AddCheckViewModel();
            Application.Current.MainWindow = this;
        } 
        public AddCheck(int check)
        {
            InitializeComponent();
            DataContext = new AddCheckViewModel(check);
            Application.Current.MainWindow = this;
        }
        
    }
}
