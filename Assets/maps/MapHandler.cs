using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    GameObject enterMapButton;
    GameObject mapSelectionContainer;
    public CardDisplay map;
    public int fightsRemaining = 3;
    public bool isInMap;
    private int currentFightTier;
    [SerializeField] private GameObject doMap;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject mapSelector;
    [SerializeField] private GameObject lockInButton;
    [SerializeField] private GameObject cantLockInButton;

    public bool yetToClose = false;
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
        if (GlobalVariables.instance.atFrontOfQueue == false)
        {
            GlobalVariables.instance.errorHandler.NewError("You'll have to be at the front of the queue to leave the maps");
            return;
        }
        isInMap = true;
        HideMapButtons();
        GameEventManager.instance.CloseHand();

        //map tiers are 1-3, monsters will fight progressively stronger monsters
        // currentFightTier = map.card.mapTier;
        currentFightTier = map.card.mapTier;
        fightsRemaining = 3;
        GlobalVariables.instance.selectionState = SelectionState.InMaps;
        GameEventManager.instance.SetFightCancelButtonToLeaveMap();
        NextFight();
    }
    public void EnterMap()
    {
        if (map != null)
        {
            GameEventManager.instance.MapWeaponSelection();
            CheckCanEnterMap();
        }
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
        if (fightsRemaining < 1) { endMapInteraction(); return; }
        currentFightTier++;
        Monster monsterToFight = Monster.CreateInstance(currentFightTier);

        if (map.card.rarity != Rarity.Normal)
        {

            if (map.card.mapMods.mod == Mods.Attack)
            {
                if (fightsRemaining > 3)
                {
                    monsterToFight.AddStats(map.card.mapMods.addedStats, true);
                }
            }
            else if (map.card.mapMods.mod == Mods.Defence)
            {
                if (fightsRemaining > 3)
                {
                    monsterToFight.AddStats(map.card.mapMods.addedStats, false);
                }
            }
            if (map.card.mapMods.mod == Mods.ExtraClip)
            {
                foreach (int mob in map.card.mapMods.monstersAffected)
                {
                    if (3 - fightsRemaining == mob)
                    {
                        monsterToFight.clipCount = 2;
                    }
                }
            }


        }


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
        if (fightsRemaining < 3)
        {
            int rewardCount = 1;
            if (map.card.rarity == Rarity.Magic) rewardCount = 2;
            if (map.card.rarity == Rarity.Rare) rewardCount = 3;
            yetToClose = true;
            GlobalVariables.instance.RewardContainer.GetComponent<RewardHandler>().DoReward(rewardCount + 1, map.card.mapTier + 2, rewardCount);
        }
        if (GlobalVariables.instance.selectionState == SelectionState.InMaps)
        {
            Hand.instance.hand.Remove(map.gameObject);
            Destroy(map.gameObject);
            map = null;
            GlobalVariables.instance.selectionState = SelectionState.Fight;
        }

        else { CloseOff(); }

    }
    public void CloseOff()
    {

        yetToClose = false;
        ResetMapState();
        GameEventManager.instance.EndMapScreen();
        GameEventManager.instance.CloseAllUI();
        GameEventManager.instance.SetFightCancelButtonToCancel();
        Hand.instance.cardSelection.UnSelectAllCards();
        currentFightTier = 0;
        fightsRemaining = 3;
        GameEventManager.instance.NormalTime();
        QueueMember_Player queueMember_Player = GlobalVariables.instance.player.GetComponent<QueueMember_Player>();
        if (queueMember_Player.qMan != null) queueMember_Player.qMan.Deregister(queueMember_Player);

    }
    public void ResetMapState()
    {
        doMap.SetActive(true);
        cancelButton.SetActive(true);
        lockInButton.SetActive(false);
        cantLockInButton.SetActive(false);
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

