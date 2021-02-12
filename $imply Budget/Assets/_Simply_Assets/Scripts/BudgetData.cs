using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BudgetData
{
    public int month;
    public int year;
    public double income;
    public List<CatagoryData> catagories;
    public string notes;

}
