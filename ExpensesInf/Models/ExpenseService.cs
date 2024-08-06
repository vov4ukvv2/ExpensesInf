using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExpensesInf.Models;
using System.Xml;
using Newtonsoft.Json;

public class ExpenseService
{
    private const string FilePath = "expenses.json";
    private List<Check> _checks;

    public ExpenseService() => LoadData();

    private void LoadData()
    {
        if (File.Exists(FilePath))
        {
            var jsonData = File.ReadAllText(FilePath);
            _checks = JsonConvert.DeserializeObject<List<Check>>(jsonData) ?? new List<Check>();
        }
        else
        {
            _checks = new List<Check>();
        }
    }

    private  void SaveData()
    {
        var jsonData = JsonConvert.SerializeObject(_checks, Newtonsoft.Json.Formatting.Indented);
         File.WriteAllTextAsync(FilePath, jsonData);
    }

    public  void AddCheck(Check check)
    {
        _checks.Add(check);
         SaveData();
    }
    public  void EditCheck(int oldCheck, Check newCheck)
    {
       
        if (oldCheck != -1)
        {
            _checks[oldCheck] = newCheck;
             SaveData();
        }
    }
    public  void DeleteCheck(Check check)
    {
        _checks.Remove(check);
         SaveData();
    }

    public List<Check> GetAllChecks()
    {
        return _checks;
    }

    public decimal GetTotalExpenses()
    {
        return _checks.Sum(c => c.Amount);
    }

    public decimal GetTotalExpensesByType(ExpenseType type)
    {
        return _checks.Where(c => c.Type == type).Sum(c => c.Amount);
    }

    public int GetTotalChecksCount()
    {
        return _checks.Count;
    }
    public   List<Check> GetChecksByType(ExpenseType type)
    {
        return _checks.Where(c => c.Type == type).ToList();
    }
}