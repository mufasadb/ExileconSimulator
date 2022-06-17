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
        GameEventManager.instance.CloseHand();

        //map tiers are 1-3, monsters will fight progressively stronger monsters
        // currentFightTier = map.card.mapTier;
        currentFightTier = 1;
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
        Debug.Log("Creating the next fight at tier " + currentFightTier);
        Monster monsterToFight = Monster.CreateInstance(currentFightTier);
        Debug.Log("against " + monsterToFight.name);
        FightHandler.instance.InitiateFight(monsterToFight);
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


            List<CardDisplay> allSelectedCards = new List<CardDisplay>();
            foreach (var card in Hand.instance.cardSelection.twoHandedWeapons) { allSelectedCards.Add(card); }
            foreach (var card in Hand.instance.cardSelection.oneHandedWeapons) { allSelectedCards.Add(card); }
            foreach (var card in Hand.instance.cardSelection.shields) { allSelectedCards.Add(card); }
            foreach (var card in Hand.instance.cardSelection.amulets) { allSelectedCards.Add(card); }
            foreach (var card in Hand.instance.cardSelection.rings) { allSelectedCards.Add(card); }
            foreach (var card in Hand.instance.cardSelection.chests) { allSelectedCards.Add(card); }

            //this just exits if you dont fight the monster. if we want to clip the cards it needs to be done in the fight handler (which doesnt know if we're in a map);
            foreach (var card in allSelectedCards)
            {
                card.DoUnselect();
            }

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

