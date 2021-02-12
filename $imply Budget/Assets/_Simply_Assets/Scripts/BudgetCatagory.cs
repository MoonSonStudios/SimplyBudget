using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BudgetCatagory : MonoBehaviour
{
    //Displayed Catagory Names
    public TMP_InputField catagoryNameInput; // input by user for planned expences column
    public TMP_Text catagoryName2; // set when user inputs a name in planned expences column

    //$$ Amount input Fields
    public TMP_InputField catagoryPlannedExpense; // input for $$ amount in planned expences 
    public TMP_InputField catagoryActualExpense; // input for $$ ammount in actual expences
 
    //UI 
    public Image catagoryActualExpenseImage; // catagory image, use a white image, colors set via script depending on expence ammount
    public Color highExpenseColor; //color that catagoryActualExpenseImage uses when expense is Higher than intended
    public Color lowExpenseColor; //color that catagoryActualExpenseImage uses when expense is lower than or exactly what was intended

    //used for calculations, set via user input
    public double plannedExpense; 
    public double actualExpense; 

    //controller object 
    public BudgetController controller; // set by the controller itself when this object is instanced

    // Update is called once per frame
    void Update()
    {
        //Only change values when the catagory planned and actual expence inputs are not blank
        if(catagoryActualExpense.text != "" && catagoryPlannedExpense.text != "")
        {
            plannedExpense = System.Convert.ToDouble(catagoryPlannedExpense.text);
            actualExpense = System.Convert.ToDouble(catagoryActualExpense.text);

            //change ui color in actual expences column 
            if (actualExpense > plannedExpense)
            {
                catagoryActualExpenseImage.color = highExpenseColor;
            }
            else
            {
                catagoryActualExpenseImage.color = lowExpenseColor;
            }

        }

        //if catagory is named 
        if(catagoryNameInput.text != "")
        {
            //Set the actual expence collumn name if it is not the same as the planned expence collumn name 
            if (catagoryName2.text != catagoryNameInput.text) 
            {
                catagoryName2.text = catagoryNameInput.text;

            }
        }
    }

    public void RemoveCatagory()
    {
        //Used for deleting catagory when user chooses
        controller.RemoveCatagory(gameObject);
    }
}
