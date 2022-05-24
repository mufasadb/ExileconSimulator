using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

public class CardDataSystem : MonoBehaviour
{
    #region Singleton
    public static CardDataSystem instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Hand");
            return;
        }
        instance = this;
    }
    #endregion
    public CardDataSet cardDataSet;
    public TextAsset jsonFile;
    public void Start()
    {
        LoadCardData();
    }
    public void LoadCardData()
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
        cardDataSet = JsonUtility.FromJson<CardDataSet>(jsonFile.text);
        // string json = JsonUtility.ToJson(cardDataSet.GetCardBaseData(2));
        // File.WriteAllText(path, json);
        // Debug.Log(json);


    }
}