using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

public class MonsterDataSystem : MonoBehaviour
{
    #region Singleton
    public static MonsterDataSystem instance;
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
    public MonsterDataSet monsterDataSet;
    [SerializeField] private TextAsset jsonFile;
    public void LoadMonsterData()
    {
        monsterDataSet = JsonUtility.FromJson<MonsterDataSet>(jsonFile.text);
    }
}
