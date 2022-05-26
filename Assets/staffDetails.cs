using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staffDetails : MonoBehaviour
{
    public StaffMember staffData;
    public Transform canvas;
    private float seperatingDistance = 3;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("staffData" + staffData.attack);

        // staffData.attack.StatDisplay(canvas.transform);
        statDisplay(staffData.attack, true);
        statDisplay(staffData.defence, false);
        Debug.Log("attack phys is " + staffData.attack.physical + ". attack armour is " + staffData.attack.armour);
        Debug.Log("defense phys is " + staffData.defence.physical + ". defense armour is " + staffData.defence.armour);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void statDisplay(Stats stats, bool offence)
    {
        int pos = 0;
        bool evenStats = false;
        if (stats.statCount % 2 == 0) { evenStats = true; }

        // Debug.Log(fire);

        for (int i = 0; i < stats.fire; i++)
        {
            pos = PositionAndCallStat("Fire", pos, canvas, evenStats, stats, offence);
        }
        for (int i = 0; i < stats.cold; i++)
        {
            pos = PositionAndCallStat("Cold", pos, canvas, evenStats, stats, offence);

        }
        for (int i = 0; i < stats.physical; i++)
        {
            pos = PositionAndCallStat("Physical", pos, canvas, evenStats, stats, offence);

        }
        for (int i = 0; i < stats.life; i++)
        {
            pos = PositionAndCallStat("Life", pos, canvas, evenStats, stats, offence);

        }
        for (int i = 0; i < stats.lightning; i++)
        {
            pos = PositionAndCallStat("Lightning", pos, canvas, evenStats, stats, offence);

        }
        for (int i = 0; i < stats.chaos; i++)
        {
            pos = PositionAndCallStat("Chaos", pos, canvas, evenStats, stats, offence);

        }
        for (int i = 0; i < stats.armour; i++)
        {
            pos = PositionAndCallStat("Armour", pos, canvas, evenStats, stats, offence);

        }
        for (int i = 0; i < stats.wild; i++)
        {
            pos = PositionAndCallStat("Wild", pos, canvas, evenStats, stats, offence);

        }


    }
    private int PositionAndCallStat(string statName, int currentPosition, Transform canvas, bool evenStats, Stats stats, bool offence)
    {
        int workingStatCount = stats.statCount;
        int workingCurrentPosition = currentPosition;
        int y = 19;
        if (offence) { y = 26;}
        if (workingStatCount > 5)
        {
            evenStats = false;
            if (workingStatCount % 2 == 0) { evenStats = true; }
            y += 3;
            workingStatCount = stats.statCount / 2;
            // workingStatCount = workingStatCount + 1;
            if (workingCurrentPosition > workingStatCount)
            {
                workingCurrentPosition = workingCurrentPosition - workingStatCount;
                y += -3;
            }
            workingStatCount = workingStatCount + 1;
        }
        float x = (-(workingStatCount / 2) + workingCurrentPosition) * seperatingDistance;
        if (evenStats)
        {
            x = (-(workingStatCount / 2) + 0.5f + workingCurrentPosition) * seperatingDistance;
        }
        // x = x - (statCount / 2 * seperatingDistance);


        GameObject statIcon = AddStat(StatEle.Parse<StatEle>(statName));
        statIcon.transform.SetParent(canvas, false);
        statIcon.transform.localPosition = new Vector3(x, y, 0);
        statIcon.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        currentPosition = currentPosition + 1;
        return currentPosition;
    }
    public GameObject AddStat(StatEle statEle)
    {
        GameObject statIcon = new GameObject("statIcon");
        RectTransform rt = statIcon.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.localScale = new Vector2(1.0f, 1.0f);
        Image image = statIcon.AddComponent<Image>();
        image.sprite = CardImageHolder.instance.getStat(statEle);
        rt.sizeDelta = new Vector2(image.sprite.bounds.size.x * 100, image.sprite.bounds.size.y * 100);
        return statIcon;
    }
}
