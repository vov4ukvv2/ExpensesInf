using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesInf.Models
{
    public enum ExpenseType
    {
        Food = 0,
        Transport,
        Entertainment,
        Utilities,
        Other
    }

    public class Check
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ExpenseType Type { get; set; }
    }
}
