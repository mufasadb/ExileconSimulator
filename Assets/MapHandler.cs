using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    GameObject enterMapButton;
    GameObject mapSelectionContainer;
    public CardDisplay map;
    public int fightsRemaining;
    public bool isInMap;
    private int currentFightTier;
    [SerializeField] private GameObject doMap;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject mapSelector;
    [SerializeField] private GameObject lockInButton;
    [SerializeField] private GameObject cantLockInButton;
    #region Singleton

    public static MapHandler instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Fight Handler");
            return;
        }
        instance = this;
    }
    #endregion
    public void DoMap()
    {
        isInMap = true;
        HideMapButtons();
        //set fight tier to maps fight tier  - 1 TODO:
        currentFightTier = map.card.mapTier;
        fightsRemaining = 3;
        GlobalVariables.instance.selectionState = SelectionState.InMaps;
        GameEventManager.instance.SetFightCancelButtonToLeaveMap();
        NextFight();
    }
    public void EnterMap()
    {
        GameEventManager.instance.MapWeaponSelection();
        CheckCanEnterMap();
    }
    public void CheckCanEnterMap()
    {
        bool hasWeapon = false;
        bool hasDefence = false;
        if (Hand.instance.cardSelection.oneHandedWeapons.Count > 0) { hasWeapon = true; }
        if (Hand.instance.cardSelection.twoHandedWeapons.Count > 0) { hasWeapon = true; }
        if (Hand.instance.cardSelection.shields.Count > 0) { hasDefence = true; }
        if (Hand.instance.cardSelection.chests.Count > 0) { hasDefence = true; }
        if (hasWeapon && hasDefence)
        {
            CanLockin();

        }
        else { CantLockin(); }
    }
    public void NextFight()
    {
        currentFightTier++;
        FightHandler.instance.InitiateFight(Monster.CreateInstance(currentFightTier));
        fightsRemaining--;
        // GlobalVariables.instance
    }

    public void HideMapButtons()
    {
        doMap.SetActive(false);
        cancelButton.SetActive(false);
        cantLockInButton.SetActive(false);
        lockInButton.SetActive(false);
        GlobalVariables.instance.mapSelection.MoveContainer(false);

    }
    public void ShowMapButtons()
    {
        doMap.SetActive(true);
        cancelButton.SetActive(true);
        lockInButton.SetActive(false);
        cantLockInButton.SetActive(false);
        GlobalVariables.instance.mapSelection.MoveContainer(true);
    }

    public void endMapInteraction()
    {
        if (isInMap)
        {
            Hand.instance.hand.Remove(map.card);
            Destroy(map.gameObject);
        }
        GameEventManager.instance.EndMapScreen();
        GameEventManager.instance.SetFightCancelButtonToCancel();
        isInMap = false;
        currentFightTier = 0;
        fightsRemaining = 0;
        ShowMapButtons();
    }
    public void CanLockin()
    {
        cantLockInButton.SetActive(false);
        lockInButton.SetActive(true);
    }

    public void CantLockin()
    {
        lockInButton.SetActive(false);
        cantLockInButton.SetActive(true);

    }

}

