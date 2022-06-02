using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class staffDetails : MonoBehaviour
{
    public StaffMember staffData;
    public Transform canvas;
    public Transform attackCanvas;
    public Transform defenceCanvas;
    public TextMeshPro nameText;
    private float seperatingDistance = 0.75f;
    // Start is called before the first frame update
    void Start()
    {

        // staffData.attack.StatDisplay(canvas.transform);
        statDisplay(staffData.attack, true, attackCanvas);
        statDisplay(staffData.defence, false, defenceCanvas);
        nameText.text = staffData.name;
    }

    // Update is called once per frame
    void Update()
    {

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
        // rt.pivot = new Vector2(0.5f, 0.5f);
        rt.localScale = new Vector2(2f, 2f);
        SpriteRenderer spriteRenderer = statIcon.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = CardImageHolder.instance.getStatImage(statEle);
        // rt.sizeDelta = new Vector2(spriteRenderer.sprite.bounds.size.x * 100, spriteRenderer.sprite.bounds.size.y * 100);
        return statIcon;
    }
    // void statDisplay(Stats stats, bool offence)
    // {
    //     int pos = 0;
    //     bool evenStats = false;
    //     if (stats.statCount % 2 == 0) { evenStats = true; }

    //     // Debug.Log(fire);

    //     for (int i = 0; i < stats.fire; i++)
    //     {
    //         pos = PositionAndCallStat("Fire", pos, canvas, evenStats, stats, offence);
    //     }
    //     for (int i = 0; i < stats.cold; i++)
    //     {
    //         pos = PositionAndCallStat("Cold", pos, canvas, evenStats, stats, offence);

    //     }
    //     for (int i = 0; i < stats.physical; i++)
    //     {
    //         pos = PositionAndCallStat("Physical", pos, canvas, evenStats, stats, offence);

    //     }
    //     for (int i = 0; i < stats.life; i++)
    //     {
    //         pos = PositionAndCallStat("Life", pos, canvas, evenStats, stats, offence);

    //     }
    //     for (int i = 0; i < stats.lightning; i++)
    //     {
    //         pos = PositionAndCallStat("Lightning", pos, canvas, evenStats, stats, offence);

    //     }
    //     for (int i = 0; i < stats.chaos; i++)
    //     {
    //         pos = PositionAndCallStat("Chaos", pos, canvas, evenStats, stats, offence);

    //     }
    //     for (int i = 0; i < stats.armour; i++)
    //     {
    //         pos = PositionAndCallStat("Armour", pos, canvas, evenStats, stats, offence);

    //     }
    //     for (int i = 0; i < stats.wild; i++)
    //     {
    //         pos = PositionAndCallStat("Wild", pos, canvas, evenStats, stats, offence);

    //     }


    // }
    // private int PositionAndCallStat(string statName, int currentPosition, Transform canvas, bool evenStats, Stats stats, bool offence)
    // {
    //     int workingStatCount = stats.statCount;
    //     int workingCurrentPosition = currentPosition;
    //     int y = 19;
    //     if (offence) { y = 26; }
    //     if (workingStatCount > 5)
    //     {
    //         evenStats = false;
    //         if (workingStatCount % 2 == 0) { evenStats = true; }
    //         y += 3;
    //         workingStatCount = stats.statCount / 2;
    //         // workingStatCount = workingStatCount + 1;
    //         if (workingCurrentPosition > workingStatCount)
    //         {
    //             workingCurrentPosition = workingCurrentPosition - workingStatCount;
    //             y += -3;
    //         }
    //         workingStatCount = workingStatCount + 1;
    //     }
    //     float x = (-(workingStatCount / 2) + workingCurrentPosition) * seperatingDistance;
    //     if (evenStats)
    //     {
    //         x = (-(workingStatCount / 2) + 0.5f + workingCurrentPosition) * seperatingDistance;
    //     }
    //     // x = x - (statCount / 2 * seperatingDistance);


    //     GameObject statIcon = AddStat(StatEle.Parse<StatEle>(statName));
    //     statIcon.transform.SetParent(canvas, false);
    //     statIcon.transform.localPosition = new Vector3(x, y, 0);
    //     statIcon.transform.localScale = new Vector3(0.1f, 0.1f, 0);
    //     currentPosition = currentPosition + 1;
    //     return currentPosition;
    // }
    // public GameObject AddStat(StatEle statEle)
    // {
    //     GameObject statIcon = new GameObject("statIcon");
    //     RectTransform rt = statIcon.AddComponent<RectTransform>();
    //     rt.anchorMin = new Vector2(0, 0);
    //     rt.anchorMax = new Vector2(0, 0);
    //     rt.pivot = new Vector2(0.5f, 0.5f);
    //     rt.localScale = new Vector2(1.0f, 1.0f);
    //     Image image = statIcon.AddComponent<Image>();
    //     image.sprite = CardImageHolder.instance.getStat(statEle);
    //     rt.sizeDelta = new Vector2(image.sprite.bounds.size.x * 100, image.sprite.bounds.size.y * 100);
    //     return statIcon;
    // }
}
