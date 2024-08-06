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
    /// Interaction logic for ViewChecks.xaml
    /// </summary>
    public partial class ViewChecks : Window
    {
        public ViewChecks()
        {
            InitializeComponent();
            DataContext = new ViewChecksViewModel();
            Application.Current.MainWindow = this;

        }
    }
}
