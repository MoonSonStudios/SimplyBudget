using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BudgetCatagory : MonoBehaviour
{

    public TMP_InputField catagoryNameInput;
    public TMP_Text catagoryName2;

    public TMP_InputField catagoryPlannedExpense;
    public TMP_InputField catagoryActualExpense;

    public Image catagoryActualExpenseImage;

    public Color highExpenseColor;
    public Color lowExpenseColor;

    public double plannedExpense;
    public double actualExpense;

    public BudgetController controller;

    // Update is called once per frame
    void Update()
    {
        if(catagoryActualExpense.text != "" && catagoryPlannedExpense.text != "")
        {
            plannedExpense = System.Convert.ToDouble(catagoryPlannedExpense.text);
            actualExpense = System.Convert.ToDouble(catagoryActualExpense.text);
            

            if (actualExpense > plannedExpense)
            {
                catagoryActualExpenseImage.color = highExpenseColor;
            }
            else
            {
                catagoryActualExpenseImage.color = lowExpenseColor;
            }

        }

        if(catagoryNameInput.text != "")
        {
            if(catagoryName2.text != catagoryNameInput.text)
            {
                catagoryName2.text = catagoryNameInput.text;

            }
        }
    }

    public void RemoveCatagory()
    {
        controller.RemoveCatagory(gameObject);
    }
}
