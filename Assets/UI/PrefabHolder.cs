using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour
{
        #region Singleton
    public static PrefabHolder instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PrefabHolder");
            return;
        }
        instance = this;
    }
    #endregion
    public GameObject CardPrefab;
    public GameObject StaffPrefab;
}
