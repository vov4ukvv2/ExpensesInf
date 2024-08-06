using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesInf.Models
{
    class ExpenseSummary
    {
        public ExpenseType Type { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
