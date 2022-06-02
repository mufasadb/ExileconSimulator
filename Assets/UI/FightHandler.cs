using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightHandler : MonoBehaviour
{
    public bool isFighting;
    public GameObject playerAttack;
    public GameObject playerDefence;
    // public GameObject enemyAttack;
    // public GameObject enemyDefence;
    public GameObject FightUI;
    public GameObject SelectionUI;
    public CardSelection cardSelection;
    private float seperatingDistance = 55;
    private StaffMember currentFightTarget;
    #region Singleton

    public static FightHandler instance;
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
    public void Start()
    {
        cardSelection = Hand.instance.cardSelection;
    }
    public void doFight()
    {
        if (ResolveFight(Hand.instance.cardSelection.attack, Hand.instance.cardSelection.defence, currentFightTarget.attack, currentFightTarget.defence))
        {
            Debug.Log("PLAYER WON");
            List<CardDisplay> allSelectedCards = new List<CardDisplay>();
            foreach (var card in cardSelection.twoHandedWeapons) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.oneHandedWeapons) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.shields) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.amulets) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.rings) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.chests) { allSelectedCards.Add(card); }
            ClipMethodResolver.HandleClip(allSelectedCards, currentFightTarget.clipMethod, currentFightTarget.clipCount);


           //offer cards
           CreateRewardCard(1);
           CreateRewardCard(2);
        }
        else { Debug.LogError("Player Loses"); }

        //clip cards

        //reward cards
    }
    private void CreateRewardCard(int rewardNumber)
    {
        Card.CreateInstance(currentFightTarget.tier, "null");
        GameObject newCard = Instantiate(PrefabHolder.instance.CardPrefab, new Vector3(0, 0, 0), Quaternion.identity, GlobalVariables.instance.RewardContainer.transform);
        newCard.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        newCard.GetComponent<CardDisplay>().destination = GlobalVariables.instance.RewardContainer.transform.position + new Vector3(-500 + rewardNumber * 250, 0, 0);
    }
    public bool ResolveFight(Stats playerAttack, Stats playerDefence, Stats enemyAttack, Stats enemyDefence)
    {
        //player Attack vs Enemy Defence.
        int playerSpare = 0;
        int playerWild = playerAttack.wild;
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.fire, enemyDefence.fire);
        if (playerWild < 0) { announceResults("not enough fire Attack"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.cold, enemyDefence.cold);
        if (playerWild < 0) { announceResults("not enough cold Attack"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.lightning, enemyDefence.lightning);
        if (playerWild < 0) { announceResults("not enough lightning Attack"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.physical, enemyDefence.physical);
        if (playerWild < 0) { announceResults("not enough physical Attack"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.chaos, enemyDefence.chaos);
        if (playerWild < 0) { announceResults("not enough chaos Attack"); return false; }
        playerWild += playerSpare;
        playerWild -= enemyDefence.wild;
        if (playerWild < 0) { announceResults("not enough spare stats Attack"); return false; }

        //player Defence vs Enemy Attack
        playerSpare = 0;
        playerWild = playerDefence.wild;
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.fire, enemyAttack.fire);
        if (playerWild < 0) { announceResults("not enough fire Defence"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.cold, enemyAttack.cold);
        if (playerWild < 0) { announceResults("not enough cold Defence"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.lightning, enemyAttack.lightning);
        if (playerWild < 0) { announceResults("not enough lightning Defence"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.armour, enemyAttack.armour);
        if (playerWild < 0) { announceResults("not enough armour Defence"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.life, enemyAttack.life);
        if (playerWild < 0) { announceResults("not enough armour Life"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.chaos, enemyAttack.chaos);
        if (playerWild < 0) { announceResults("not enough chaos Defence"); return false; }
        playerWild += playerSpare;
        playerWild -= enemyAttack.wild;
        if (playerWild < 0) { announceResults("not enough spare stats Defence"); return false; }

        return true;
    }
    private (int playerWild, int playerSpare) ResolveSingleStat(int playerWild, int playerSpare, int playerVal, int enemyVal)
    {
        int remainingVal = playerVal - enemyVal;
        if (remainingVal < 0)
        {
            playerWild += remainingVal;
        }
        else if (remainingVal > 0)
        {
            playerSpare += remainingVal;
        }
        return (playerWild, playerSpare);
    }
    private void announceResults(string reason)
    {
        // comment out to clean up noise
        Debug.Log(reason);
    }
    public void reCalculateStats()
    {
        foreach (Transform child in playerAttack.transform) { Destroy(child.gameObject); }
        foreach (Transform child in playerDefence.transform) { Destroy(child.gameObject); }
        statDisplay(cardSelection.attack, true, playerAttack.transform);
        statDisplay(cardSelection.defence, true, playerDefence.transform);

    }
    public void InitiateFight(GameObject TargetEnemy, StaffMember staffMember)
    {
        if (!isFighting)
        {
            currentFightTarget = staffMember;
            isFighting = true;
            FightUI.SetActive(true);
            SelectionUI.SetActive(true);
            statDisplay(cardSelection.attack, true, playerAttack.transform);
            statDisplay(cardSelection.defence, true, playerDefence.transform);
        }
    }
    public void CancelFight()
    {
        isFighting = false;
        FightUI.SetActive(false);
        SelectionUI.SetActive(false);
        removeChildren();
        currentFightTarget = null;
    }
    public void removeChildren()
    {
        foreach (Transform child in playerAttack.transform) { Destroy(child.gameObject); }
        foreach (Transform child in playerDefence.transform) { Destroy(child.gameObject); }
        // foreach (Transform child in enemyAttack.transform) { Destroy(child.gameObject); }
        // foreach (Transform child in enemyDefence.transform) { Destroy(child.gameObject); }
    }
    void statDisplay(Stats stats, bool offence, Transform canvas)
    {
        // int pos = 0;
        // bool evenStats = false;
        List<string> statEleList = new List<string>();

        statEleList = CalculateAndAddStringsFromStatEle("Fire", stats.fire, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Cold", stats.cold, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Lightning", stats.lightning, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Physical", stats.physical, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Armour", stats.armour, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Life", stats.life, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Chaos", stats.chaos, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Wild", stats.wild, statEleList);

        for (int i = 0; i < statEleList.Count; i++)
        {
            PositionAndCallStat(statEleList[i], i, canvas, statEleList.Count % 2 == 0, statEleList.Count, true);
        }


    }
    private List<string> CalculateAndAddStringsFromStatEle(string baseEleName, int statVal, List<string> statEleList)
    {
        int workingStat = statVal % 3;
        int statTriple = (int)Mathf.Floor(statVal / 3);
        for (int i = 0; i < workingStat; i++)
        {
            statEleList.Add(baseEleName);
        }
        for (int i = 0; i < statTriple; i++)
        {
            statEleList.Add(baseEleName + "Triple");
        }
        return statEleList;
    }
    private int PositionAndCallStat(string statEle, int currentPosition, Transform canvas, bool evenStats, int totalStats, bool offence)
    {
        int workingStatCount = totalStats;
        int workingCurrentPosition = currentPosition;
        int y = 0;
        if (workingStatCount > 5)
        {
            evenStats = false;
            if (workingStatCount % 2 == 0) { evenStats = true; }
            y = 20;
            workingStatCount = totalStats / 2;
            if (workingCurrentPosition > workingStatCount)
            {
                workingCurrentPosition = workingCurrentPosition - workingStatCount;
                y = -20;
            }
            workingStatCount = workingStatCount + 1;
        }
        float x = (-(workingStatCount / 2) + workingCurrentPosition) * seperatingDistance;
        if (evenStats)
        {
            x = (-(workingStatCount / 2) + 0.5f + workingCurrentPosition) * seperatingDistance;
        }

        GameObject statIcon = AddStat(statEle);
        statIcon.transform.SetParent(canvas, false);
        statIcon.transform.localPosition = new Vector3(x, y, 0);
        // statIcon.transform.localScale = new Vector3(1f, 1f, 0);
        currentPosition = currentPosition + 1;
        return currentPosition;
    }
    public GameObject AddStat(string statEle)
    {
        GameObject statIcon = new GameObject("statIcon");
        RectTransform rt = statIcon.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.localScale = new Vector2(1.25f, 1.25f);
        Image image = statIcon.AddComponent<Image>();
        image.sprite = CardImageHolder.instance.getStatImage(statEle);
        rt.sizeDelta = new Vector2(image.sprite.bounds.size.x * 100, image.sprite.bounds.size.y * 100);
        return statIcon;
    }
}
