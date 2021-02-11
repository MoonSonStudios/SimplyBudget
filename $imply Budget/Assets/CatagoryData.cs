using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatagoryData
{
    public string catagoryName;
    public double expectedExpense;
    public double actualExpenses;

    public CatagoryData(BudgetCatagory catagory)
    {
        catagoryName = catagory.catagoryNameInput.text;
        expectedExpense = catagory.plannedExpense;
        actualExpenses = catagory.actualExpense;
    }
}
