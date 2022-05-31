using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    #region Singleton

    public static GlobalVariables instance;
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
    // Start is called before the first frame update
    public bool currentlyDragging = false;


    // Update is called once per frame
    void Update()
    {

    }
}
