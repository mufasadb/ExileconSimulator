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
    [Header("Global Settings")]
    [Range(0.1f, 5f)]
    public float timeMulti = 1;
    // Start is called before the first frame update
    [Header("Tracked Variables")]
    public float timer = 960;
    public bool currentlyDragging = false;
    public bool preventMoving = false;
    public int rewardPendingCount = 0;
    public int clipPendingCount = 0;
    public float cardsMovingCooldown = 0;
    public bool cardAnimation = false;
    public int lastFightID;
    public SelectionState selectionState = SelectionState.InitialDeal;
    public float gameSpeed = 0.5f;

    [Header("Containers")]

    public GameObject SelectionContainer;
    public Transform cameraTrans;
    public GameObject RewardContainer;
    public GameObject CraftingContainer;
    public ErrorHandler errorHandler;
    public GameObject handContainer;
    public MapSelection mapSelection;
    public MapHandler mapHandler;
    public GameObject clipTop;
    public GameObject clipBottom;


    // Update is called once per frame
    void Update()
    {
        if (cardsMovingCooldown > 0)
        {
            cardsMovingCooldown -= Time.deltaTime;
        }
        timer -= Time.deltaTime * timeMulti * gameSpeed;

    }
}
public enum SelectionState { Fight, EnteringMaps, InMaps, InitialDeal }
