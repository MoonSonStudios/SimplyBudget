using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadController : MonoBehaviour
{
    List<CatagoryData> catagoryDatas = new List<CatagoryData>();
    BudgetController budgetController;

    public void Start()
    {
        budgetController = GetComponent<BudgetController>();
    }

    void Update()
    {
        LoadDataForCalender();
    }

    public BudgetData CreateBudgetData()
    {
        BudgetCatagory[] catagoriesToConvert = GetCatagories();

        BudgetData budgetData = new BudgetData();

        if (catagoriesToConvert != null)
        {
            foreach (BudgetCatagory catagory in catagoriesToConvert)
            {
                CatagoryData catagoryData = new CatagoryData(catagory);
                if (catagoryData != null)
                {
                    catagoryDatas.Add(catagoryData);
                }
                else
                {
                    Debug.LogError("No partdata found");
                }
            }
        }
        else
        {
            Debug.LogError("Parts to conver is null");
        }
        //catagoryDatas.Add(new CatagoryData());
        if (catagoryDatas != null)
        {
            budgetData.catagories = catagoryDatas;
        }
        
        
        budgetData.income = System.Convert.ToDouble(budgetController.IncomeInputText.text);
        budgetData.month = budgetController.month;
        budgetData.year = budgetController.year;
        budgetData.notes = budgetController.NotesInputText.text;

        return budgetData;
    }

    public BudgetCatagory[] GetCatagories()
    {
        return FindObjectsOfType<BudgetCatagory>();
    }

    public void SaveBudget()
    {
        BudgetData data = CreateBudgetData();
       
        SaveLoadBudgetDatas.SaveBudget(data);
    }

    private void LoadDataForCalender()
    {
        //Get all files at path
        //foreach file in folder get the month and year, if year matches selected year color the correct months based on files of that year
        List<Button> monthButtons = budgetController.monthButtons;

        foreach(Button button in monthButtons)
        {
            string loadfileString = null;
            string monthString = (monthButtons.IndexOf(button) + 1).ToString();

            loadfileString = (monthString + budgetController.year.ToString());

           
            BudgetData data = SaveLoadBudgetDatas.LoadBudget(loadfileString);
            if(data != null)
            {
                double actualExpences = 0;

                foreach(CatagoryData catData in data.catagories)
                {
                    actualExpences += catData.actualExpenses;
                }

                if(actualExpences <= data.income)
                {
                    button.image.color = budgetController.lowCalculationColor;
                }
                if(actualExpences > data.income)
                {
                    button.image.color = budgetController.highCalculationColor;
                }
            }
            else
            {
                button.image.color = budgetController.defaultColor;
            }
            
        }
    }

    public void LoadBudget(int month)
    {
        //clear existing data
        foreach (BudgetCatagory catagory in budgetController.budgetCatagories)
        {
            Destroy(catagory.transform.gameObject);
        }
        budgetController.budgetCatagories.Clear();

        budgetController.IncomeInputText.text = "";
        budgetController.NotesInputText.text = "";

        budgetController.actualExpenseCalculationText.text = "";
        budgetController.actualExpenseCalculationText.color = budgetController.defaultTextColor;

        budgetController.savingsCalculationText.text = "";
        budgetController.savingsCalculationText.color = budgetController.defaultTextColor;

        budgetController.plannedExpenseCalculationText.text = "";
        budgetController.plannedExpenseCalculationText.color = budgetController.defaultTextColor;




        string loadfileString = null;
        string monthString = month.ToString();
        loadfileString = (monthString + budgetController.year.ToString());
        
        BudgetData data = SaveLoadBudgetDatas.LoadBudget(loadfileString);

        if(data != null)
        {
            ClearBudget();

            budgetController.month = data.month;
            budgetController.year = data.year;
            budgetController.IncomeInputText.text = System.Convert.ToString(data.income);

     

            //load notes
            budgetController.NotesInputText.text = data.notes;

            //create catagories from data
            foreach(CatagoryData catagoryData in data.catagories)
            {
                GameObject catagoryOBJ = budgetController.AddCatagory();
                BudgetCatagory catagory = catagoryOBJ.GetComponent<BudgetCatagory>();

                catagory.catagoryNameInput.text = catagoryData.catagoryName;

                catagory.catagoryPlannedExpense.text = catagoryData.plannedExpense.ToString();
                catagory.plannedExpense = catagoryData.plannedExpense;

                catagory.catagoryActualExpense.text = catagoryData.actualExpenses.ToString();
                catagory.actualExpense = catagoryData.actualExpenses;
   
            }
        }
        else
        {
            ClearBudget();

        }

    }

    private void ClearBudget()
    {
        //clear existing data
        foreach (BudgetCatagory catagory in budgetController.budgetCatagories)
        {
            Destroy(catagory.transform.gameObject);
        }
        budgetController.budgetCatagories.Clear();

        budgetController.IncomeInputText.text = "";
        budgetController.NotesInputText.text = "";

        budgetController.actualExpenseCalculationText.text = "";
        budgetController.actualExpenseCalculationText.color = budgetController.defaultTextColor;

        budgetController.savingsCalculationText.text = "";
        budgetController.savingsCalculationText.color = budgetController.defaultTextColor;

        budgetController.plannedExpenseCalculationText.text = "";
        budgetController.plannedExpenseCalculationText.color = budgetController.defaultTextColor;


    }
}
