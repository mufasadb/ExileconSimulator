using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

public class StaffDataSystem : MonoBehaviour
{
    #region Singleton
    public static StaffDataSystem instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Staffdatasysyem");
            return;
        }
        instance = this;
        LoadStaffData();

    }
    #endregion
    public StaffDataSet staffDataSet;
    public TextAsset jsonFile;
    public void Start()
    {
    }
    public void LoadStaffData()
    {
        // string path = "Assets/Card/cardData.json";
        // FileStream stream = new FileStream(path, FileMode.Create);
        // List<CardDataObject> cardDataSet = new List<CardDataObject>();
        // CardDataSet cardDataSet = new CardDataSet();
        // cardDataSet.AddCard();
        // // CardDataObject cardDataObject = new CardDataObject();
        // // cardDataSet.Add(cardDataObject);
        // // cardDataSet.Add(cardDataObject);
        // Debug.Log(cardDataSet.set.Count);
        // string json = JsonUtility.ToJson(cardDataSet);

        // File.WriteAllText(Application.dataPath + "/ItemObjects.json", json);
        staffDataSet = JsonUtility.FromJson<StaffDataSet>(jsonFile.text);
        Debug.Log(staffDataSet);
        Debug.Log(staffDataSet.set.Count);
        Debug.Log(staffDataSet.set[0]);
        // string json = JsonUtility.ToJson(cardDataSet.GetCardBaseData(2));
        // File.WriteAllText(path, json);
        // Debug.Log(json);


    }
}