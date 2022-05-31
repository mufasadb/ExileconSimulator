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
    public CardSelection cardSelection;
    private float seperatingDistance = 20;
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
    public void doFight()
    {
        Debug.Log("doing fight with");
    }
    public void reCalculateStats()
    {
        foreach (Transform child in playerAttack.transform) { Destroy(child.gameObject); }
        foreach (Transform child in playerDefence.transform) { Destroy(child.gameObject); }
        statDisplay(cardSelection.attack, true, playerAttack.transform);
        statDisplay(cardSelection.defence, true, playerDefence.transform);
    }
    public void InitiateFight(GameObject TargetEnemy)
    {
        if (!isFighting)
        {
            isFighting = true;
            cardSelection = Hand.instance.cardSelection;
            FightUI.SetActive(true);
            statDisplay(cardSelection.attack, true, playerAttack.transform);
            statDisplay(cardSelection.defence, true, playerDefence.transform);
        }
    }
    private void DrawDefence()
    {

    }
    public void CancelFight()
    {
        isFighting = false;
        FightUI.SetActive(false);
        removeChildren();
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
            // workingStatCount = workingStatCount + 1;
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
        rt.localScale = new Vector2(0.55f, 1.0f);
        Image image = statIcon.AddComponent<Image>();
        image.sprite = CardImageHolder.instance.getStatImage(statEle);
        rt.sizeDelta = new Vector2(image.sprite.bounds.size.x * 100, image.sprite.bounds.size.y * 100);
        return statIcon;
    }
}
