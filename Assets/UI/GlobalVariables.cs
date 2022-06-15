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
            Debug.LogWarning("More than one instance of GlobalVariables");
            return;
        }
        instance = this;
    }
    #endregion
    // Start is called before the first frame update
    public bool currentlyDragging = false;
    public GameObject SelectionContainer;
    public Transform cameraTrans;
    public GameObject RewardContainer;
    public GameObject CraftingContainer;
    public ErrorHandler errorHandler;
    public bool preventMoving = false;
    public bool rewardPending = false;
    public GameObject handContainer;
    public MapSelection mapSelection;

    // Update is called once per frame
    void Update()
    {

    }
}
