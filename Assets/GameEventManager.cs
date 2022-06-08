using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    #region Singleton

    public static GameEventManager instance;
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

    [SerializeField] GameObject handUI;
    [SerializeField] GameObject fightUI;
    [SerializeField] GameObject rewardUI;
    [SerializeField] GameObject selectionUI;
    [SerializeField] GameObject craftUI;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;
    DisplayStaffStats staffStats;
    public bool handOpen = false;

    public void OpenHand()
    {
        OpenUIItem(handUI);
        OpenUIItem(leftButton);
        OpenUIItem(rightButton);
        handOpen = true;
    }
    public void CloseHand()
    {
        CloseUIItem(handUI);
        CloseUIItem(leftButton);
        CloseUIItem(rightButton);
        handOpen = false;
    }
    public void BeginFightScreen(DisplayStaffStats _staffStats)
    {
        OpenUIItem(fightUI);
        OpenUIItem(selectionUI);
        OpenHand();
        GlobalVariables.instance.preventMoving = true;
        if (_staffStats != null)
        {
            staffStats = _staffStats;
            staffStats.showNLock();
        }
    }
    public void EndFightScreen()
    {
        CloseUIItem(fightUI);
        CloseUIItem(selectionUI);
        CloseHand();
        GlobalVariables.instance.preventMoving = false;
        if (staffStats)
        {
            staffStats.hideNUnlock();
        }
        staffStats = null;
    }
    public void BeginRewardScreen()
    {
        OpenUIItem(fightUI);
        OpenHand();
        OpenUIItem(rewardUI);
        GlobalVariables.instance.preventMoving = true;
    }
    public void EndRewardScreen()
    {
        CloseUIItem(fightUI);
        CloseUIItem(selectionUI);
        CloseHand();
        CloseUIItem(craftUI);
        GlobalVariables.instance.preventMoving = false;
    }
    public void BeginCraftScreen()
    {
        OpenHand();
        OpenUIItem(craftUI);
        GlobalVariables.instance.preventMoving = true;
    }
    public void EndCraftScreen()
    {
        CloseUIItem(craftUI);
        GlobalVariables.instance.preventMoving = false;
    }
    public void CloseAllUI()
    {
        CloseUIItem(fightUI);
        CloseUIItem(selectionUI);
        CloseHand();
        CloseUIItem(craftUI);
        CloseUIItem(rewardUI);
        if (staffStats != null) { staffStats.hideNUnlock(); }
        Debug.Log(GlobalVariables.instance.preventMoving);
        GlobalVariables.instance.preventMoving = false;
        Debug.Log(GlobalVariables.instance.preventMoving);
        FightHandler.instance.CancelFight();
    }

    private void OpenUIItem(GameObject UIItem)
    {
        if (!UIItem.activeSelf) { UIItem.SetActive(true); }
    }
    private void CloseUIItem(GameObject UIItem)
    {
        if (UIItem.activeSelf) { UIItem.SetActive(false); };
    }

}
