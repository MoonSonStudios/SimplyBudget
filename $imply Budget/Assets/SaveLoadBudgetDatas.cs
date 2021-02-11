using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadBudgetDatas
{
    public static void SaveBudget(BudgetData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + data.month + data.year + ".sbudget";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();


    }

    public static BudgetData LoadBudget(string filename)
    {
        string path = Application.persistentDataPath + "/" + filename + ".sbudget";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            BudgetData newData = formatter.Deserialize(stream) as BudgetData;
            stream.Close();

            return newData;
        }
        else
        {
            //Debug.LogError("File " + path + "does not exist and cannot be loaded");
            return null;
        }

    }
}
