using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatagoryData
{
    public string catagoryName;
    public double plannedExpense;
    public double actualExpenses;

    public CatagoryData(BudgetCatagory catagory)
    {
        catagoryName = catagory.catagoryNameInput.text;
        plannedExpense = catagory.plannedExpense;
        actualExpenses = catagory.actualExpense;
    }
}
