using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BudgetController : MonoBehaviour
{
    //calender variables

    [Header("Calender Variables")]
    public TMP_Text calenderBannerText;
    public Canvas calenderObject;
    public bool calenderOpen = true;
    
    [Space]

    public TMP_Text selectedYearText;
    public TMP_Text nextYearText;
    public TMP_Text yearAfterNextText;
    public TMP_Text previousYearText;
    public TMP_Text yearBeforePreviousText;

    [Space]

    public Button selectNextYear;
    public Button selectYearAfterNext;

    public Button selectPreviousYear;
    public Button selectYearBeforePrevious;

    public int year;

    [Range(1,12)]
    public int month;
    private string monthName;

    public List<Button> monthButtons;

    // budgetting variables

    [Header ("Budget Variables")]
    public GameObject catagoryHolder; //catagories are children of this object
    public GameObject catagoryPrefab; //blank catagory object

    public TMP_InputField IncomeInputText; //Income text entered by user
    public TMP_Text savingsCalculationText; //savings text calculated based on income minus actual expenses
    public TMP_InputField NotesInputText;

    public TMP_Text plannedExpenseCalculationText; //planned expenses text calculated by sum of planned expenses in all catagories
    public TMP_Text actualExpenseCalculationText; //actual expenses text calculated by the sum of sum of all actual expences in catagories

    public Color highCalculationColor; //color that displays numbers that are higher or lower than wanted
    public Color lowCalculationColor; //color that displays numbers that are wanted ro better than expected
    public Color noSavingsColor; //color that displays when savings are negative
    public Color defaultColor; //default color
    public Color defaultTextColor; //default color


    private string plannedExpense;
    private string actualExpense;

    public List<BudgetCatagory> budgetCatagories;//list of all created catagories

    private void Start()
    {
        OpenCalender();
        SetYearSelectorText();
        SetSelectedMonthName("Jan");
        SetCalenderBannerText();
    }
    private void Update()
    {
        double plannedSum = 0;
        double actualSum = 0;

        double income = 0;
        double savings = 0;

        if (budgetCatagories.Count > 0) //when there are catagorys created
        {
            income = System.Convert.ToDouble(IncomeInputText.text); //get income from textinput

            foreach (BudgetCatagory catagory in budgetCatagories) //for every catagory created get the planned and actual expense amounts and calculate total
            {
                plannedSum += System.Convert.ToDouble(catagory.plannedExpense);
                actualSum += System.Convert.ToDouble(catagory.actualExpense);
            }

            //convert calculations of expenses to text
            plannedExpenseCalculationText.text = System.Convert.ToString("$" + plannedSum); 
            actualExpenseCalculationText.text = System.Convert.ToString("$" + actualSum);

            //calculate savings and convert savings to text to be displayed
            savings = income - actualSum;
            savingsCalculationText.text = System.Convert.ToString("$" + savings);

        }

        if(actualSum > plannedSum)
        {
            actualExpenseCalculationText.color = highCalculationColor; //when actual expenses is higher than expected, color calculated amount orange
        }
        else
        {
            actualExpenseCalculationText.color = lowCalculationColor; //when actual expenses is equal or lower than expected, color calculated amount green
        }

        if(savings >= 0)
        {
            savingsCalculationText.color = lowCalculationColor; //when savings is not negative then color the amount green 
        }
        else
        {
            savingsCalculationText.color = noSavingsColor; //when savings is negative then color the amount red 
        }
    }

    public void SetCalenderBannerText()
    {
        calenderBannerText.text = monthName + " " + System.Convert.ToString(year);
    }

    public void SetSelectedMonth(int _month)
    {
        month = _month;
        SetCalenderBannerText();
    }
    public void SetSelectedMonthName(string _monthName)
    {
        monthName = _monthName;
        SetCalenderBannerText();
    }

    public void SetSelectedYear(int amountToChange)
    {
        year = year + amountToChange;
        SetYearSelectorText();
        SetCalenderBannerText();
    }

    public void SetYearSelectorText()
    {
        selectedYearText.text = System.Convert.ToString(year);
        nextYearText.text = System.Convert.ToString(year + 1);
        yearAfterNextText.text = System.Convert.ToString(year + 2);
        previousYearText.text = System.Convert.ToString(year - 1);
        yearBeforePreviousText.text = System.Convert.ToString(year - 2);

    }
    public void DebugTest()
    {
        Debug.Log("Test");
    }
    public void OpenCalender()
    {

        calenderOpen = !calenderOpen;
        calenderObject.enabled = calenderOpen;
    }

    public void AddNewCatagory()
    {
        GameObject catagory = Instantiate(catagoryPrefab, catagoryHolder.transform.GetChild(0).transform); //create a new blank catagory and position it
        catagory.GetComponent<BudgetCatagory>().controller = this;
        budgetCatagories.Add(catagory.GetComponent<BudgetCatagory>()); //add catagory to list to be able to get its contents easy

    }
    public GameObject AddCatagory()
    {
        GameObject catagory = Instantiate(catagoryPrefab, catagoryHolder.transform.GetChild(0).transform); //create a new blank catagory and position it
        catagory.GetComponent<BudgetCatagory>().controller = this;  
        budgetCatagories.Add(catagory.GetComponent<BudgetCatagory>()); //add catagory to list to be able to get its contents easy

        return catagory;
    } 
    
    public void RemoveCatagory(GameObject _catagory)
    {
        budgetCatagories.Remove(_catagory.GetComponent<BudgetCatagory>()); //remove catagory from list;
        Destroy(_catagory);//delete catagory clone
    }
}
