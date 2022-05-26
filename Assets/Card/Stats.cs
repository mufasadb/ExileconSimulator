using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats
{
    // Start is called before the first frame update
    public int wild = 0;
    public int physical = 0;
    public int cold = 0;
    public int lightning = 0;
    public int fire = 0;
    public int chaos = 0;
    public int armour = 0;
    public int life = 0;
    public int statCount = 0;
    private int seperatingDistance = 40;
    public Stats()
    {
        // makeExplicit(Type.TwoHandedWeapon);

    }
    public Stats DeclareStats(int[] ints)
    {
        //int fire, int cold, int lightning, int physical, int life, int armour, int chaos, int wild
        this.fire = ints[0];
        this.cold = ints[1];
        this.lightning = ints[2];
        this.physical = ints[3];
        this.life = ints[4];
        this.armour = ints[5];
        this.chaos = ints[6];
        this.wild = ints[7];
        return this;
    }
    public void createStaffAttack(int tier)
    {
        armour = 3;
        countStats();
    }
    public void createStaffDefence(int tier)
    {
        physical = 3;
        cold = 2;
        lightning = 2;
        countStats();
        //make staff
    }
    public void countStats()
    {
        statCount = physical + armour + life + chaos + wild + cold + fire + lightning;
    }
    public void makeImplicit(Type type)
    {
        if (type == Type.OneHandedWeapon || type == Type.TwoHandedWeapon)
        {
            physical = 2;
            statCount += 2;
            // do weapon
        }
        else if (type == Type.Shield || type == Type.Chest)
        {
            life = 2;
            armour = 2;
            countStats();
            // do armour
        }
        else
        {
            wild = 2;
            countStats();
            // do jewelery
        }
    }
    public void makeExplicit(Type type)
    {
        if (type == Type.OneHandedWeapon || type == Type.TwoHandedWeapon)
        {
            physical = 2;
            cold = Mathf.RoundToInt(Random.Range(0, 3));
            fire = Mathf.RoundToInt(Random.Range(0, 3));
            lightning = Mathf.RoundToInt(Random.Range(0, 3));
            countStats();
            // do weapon
        }
        else if (type == Type.Shield || type == Type.Chest)
        {

            life = Mathf.RoundToInt(Random.Range(0, 3));
            armour = Mathf.RoundToInt(Random.Range(0, 3));
            countStats();
            // do armour
        }
        else
        {
            wild = 2;
            countStats(); 
            // do jewelery
        }
    }
    public void StatDisplay(Transform parentContainer)
    {
        int pos = 0;
        bool evenStats = false;
        if (statCount % 2 == 0) { evenStats = true; }

        // Debug.Log(fire);

        for (int i = 0; i < fire; i++)
        {
            pos = PositionAndCallStat("Fire", pos, parentContainer, evenStats);
        }
        for (int i = 0; i < cold; i++)
        {
            pos = PositionAndCallStat("Cold", pos, parentContainer, evenStats);

        }
        for (int i = 0; i < physical; i++)
        {
            pos = PositionAndCallStat("Physical", pos, parentContainer, evenStats);

        }
        for (int i = 0; i < life; i++)
        {
            pos = PositionAndCallStat("Life", pos, parentContainer, evenStats);

        }
        for (int i = 0; i < lightning; i++)
        {
            pos = PositionAndCallStat("Lightning", pos, parentContainer, evenStats);

        }
        for (int i = 0; i < chaos; i++)
        {
            pos = PositionAndCallStat("Chaos", pos, parentContainer, evenStats);

        }
        for (int i = 0; i < armour; i++)
        {
            pos = PositionAndCallStat("Armour", pos, parentContainer, evenStats);

        }
        for (int i = 0; i < wild; i++)
        {
            pos = PositionAndCallStat("Wild", pos, parentContainer, evenStats);

        }

        // Debug.Log(card.implicits);
    }
    public GameObject AddStat(StatEle statEle)
    {
        GameObject statIcon = new GameObject("statIcon");
        RectTransform rt = statIcon.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.localScale = new Vector2(1f, 1f);
        Image image = statIcon.AddComponent<Image>();
        image.sprite = CardImageHolder.instance.getStat(statEle);
        rt.sizeDelta = new Vector2(image.sprite.bounds.size.x * 100, image.sprite.bounds.size.y * 100);
        return statIcon;
    }
    private int PositionAndCallStat(string statName, int currentPosition, Transform parentContainer, bool evenStats)
    {
        int workingStatCount = statCount;
        int workingCurrentPosition = currentPosition;
        int y = 0;
        if (workingStatCount > 5)
        {
            evenStats = false;
            if (workingStatCount % 2 == 0) { evenStats = true; }
            y = 20;
            workingStatCount = statCount / 2;
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
        // x = x - (statCount / 2 * seperatingDistance);


        GameObject statIcon = AddStat(StatEle.Parse<StatEle>(statName));
        statIcon.transform.SetParent(parentContainer, false);
        statIcon.transform.localPosition = new Vector3(x, y, 0);
        statIcon.transform.localScale = new Vector3(1f, 1f, 0);
        currentPosition = currentPosition + 1;
        return currentPosition;
    }
}
public enum StatsType { StaffDefence, staffAttack, Implicits, Explicits, Quality }
public enum StatEle { Physical, Life, Cold, Fire, Lightning, Armour, Chaos, Wild }
