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
        if (baseEleName == "Wild")
        {
            if (statVal > 0) statEleList.Add(baseEleName + statVal);
        }
        else
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
        }
        return statEleList;
    }
    private int PositionAndCallStat(string statEle, int currentPosition, Transform canvas, bool evenStats, int totalStats, bool offence)
    {
        int workingStatCount = totalStats;
        int workingCurrentPosition = currentPosition;
        float y = 0;
        if (workingStatCount > 5)
        {
            evenStats = false;
            if (workingStatCount % 2 == 0) { evenStats = true; }
            y = 0.3f;
            workingStatCount = totalStats / 2;
            if (workingCurrentPosition > workingStatCount)
            {
                workingCurrentPosition = workingCurrentPosition - workingStatCount;
                y = -0.3f;
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
        currentPosition = currentPosition + 1;
        return currentPosition;
    }
    public GameObject AddStat(string statEle)
    {
        GameObject statIcon = new GameObject("statIcon");
        RectTransform rt = statIcon.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.localScale = new Vector2(2f, 2f);
        SpriteRenderer spriteRenderer = statIcon.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = CardImageHolder.instance.getStatImage(statEle);
        return statIcon;
    }

}
