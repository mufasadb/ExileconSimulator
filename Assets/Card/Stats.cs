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
    public int armour = 0;
    public int life = 0;
    public int statCount = 0;
    public Stats(StatsType statsType, Type type)
    {
        if (statsType == StatsType.Implicits) { makeImplicit(type); }
        if (statsType == StatsType.Explicits) { makeExplicit(type); }
        if (statsType == StatsType.staffAttack)
        {
            makeStaffAttack();
        }
    }
    public void makeStaffAttack()
    {
        //make staff
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
            statCount += 2;
            armour = 2;
            statCount += 2;
            // do armour
        }
        else
        {
            wild = 2;
            statCount += 2;
            // do jewelery
        }
    }
    public void makeExplicit(Type type)
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
            statCount += 2;
            armour = 2;
            statCount += 2;
            // do armour
        }
        else
        {
            wild = 2;
            statCount += 2;
            // do jewelery
        }
    }
    public void StatDisplay(Transform parentContainer)
    {
        int pos = 0;
        int x = -33;
        x += pos * 33;

        for (int i = 0; i < fire; i++)
        {
            string statName = "Fire";
            GameObject statIcon = AddStat(StatEle.Parse<StatEle>(statName));
            statIcon.transform.SetParent(parentContainer, false);
            statIcon.transform.localPosition = new Vector3(x, 0, 0);
            statIcon.transform.localScale = new Vector3(0.6f, 0.6f, 0);
        }
        for(int i = 0; i < cold; i++){
            string statName = "Cold";
            GameObject statIcon = AddStat(StatEle.Parse<StatEle>(statName));
            statIcon.transform.SetParent(parentContainer, false);
            statIcon.transform.localPosition = new Vector3(x, 0, 0);
            statIcon.transform.localScale = new Vector3(0.6f, 0.6f, 0);

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
        rt.localScale = new Vector2(1.0f, 1.0f);
        Image image = statIcon.AddComponent<Image>();
        image.sprite = CardImageHolder.instance.getStat(StatEle.Fire);
        rt.sizeDelta = new Vector2(image.sprite.bounds.size.x * 100, image.sprite.bounds.size.y * 100);
        return statIcon;
    }
}
public enum StatsType { StaffDefence, staffAttack, Implicits, Explicits, Quality }
public enum StatEle { Physical, Life, Cold, Fire, Lightning, Chaos, Wild }
