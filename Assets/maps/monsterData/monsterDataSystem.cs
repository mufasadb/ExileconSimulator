using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

public class monsterDataSystem : MonoBehaviour
{
    #region Singleton
    public static monsterDataSystem instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Staffdatasysyem");
            return;
        }
        instance = this;
        LoadMonsterData();

    }
    #endregion
    public monsterDataSet monsterDataSet;
    [SerializeField] private TextAsset jsonFile;
    public void LoadMonsterData()
    {
        monsterDataSet = JsonUtility.FromJson<monsterDataSet>(jsonFile.text);
    }
}
