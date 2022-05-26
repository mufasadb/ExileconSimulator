using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardImageHolder : MonoBehaviour
{
    #region Singleton
    public static CardImageHolder instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CardImageHolder");
            return;
        }
        instance = this;
    }
    #endregion
    public SpriteHolder normal;
    public SpriteHolder normal1Dur;
    public SpriteHolder normal2Dur;
    public SpriteHolder magic;
    public SpriteHolder magic1Dur;
    public SpriteHolder magic2Dur;
    public SpriteHolder rare;
    public SpriteHolder rare1Dur;
    public SpriteHolder rare2Dur;
    public SpriteHolder unique;
    public SpriteHolder unique1Dur;
    public SpriteHolder unique2Dur;
    public SpriteHolder currency;
    public SpriteHolder mace;
    public SpriteHolder physical;
    public SpriteHolder armour;
    public SpriteHolder life;
    public SpriteHolder cold;
    public SpriteHolder lightning;
    public SpriteHolder fire;
    public SpriteHolder chaos;
    public SpriteHolder wild;
    public SpriteHolder oneHandedTypeIcon;
    public SpriteHolder twoHandedTypeIcon;
    public SpriteHolder ringTypeIcon;
    public SpriteHolder amuletTypeIcon;
    public SpriteHolder shieldTypeIcon;
    public SpriteHolder chestTypeIcon;
    public Sprite getBase(Rarity rarity, int durability)
    {
        if (durability == 0)
        {
            switch (rarity)
            {
                case Rarity.Normal:
                    return normal2Dur.sprite;
                case Rarity.Magic:
                    return magic2Dur.sprite;
                case Rarity.Rare:
                    return rare2Dur.sprite;
                case Rarity.Unique:
                    return unique2Dur.sprite;
                case Rarity.Currency:
                    return currency.sprite;
            }
        }
        else if (durability == 1)
        {
            switch (rarity)
            {
                case Rarity.Normal:
                    return normal1Dur.sprite;
                case Rarity.Magic:
                    return magic1Dur.sprite;
                case Rarity.Rare:
                    return rare1Dur.sprite;
                case Rarity.Unique:
                    return unique1Dur.sprite;
                case Rarity.Currency:
                    return currency.sprite;
            }
        }
        else
        {
            switch (rarity)
            {
                case Rarity.Normal:
                    return normal.sprite;
                case Rarity.Magic:
                    return magic.sprite;
                case Rarity.Rare:
                    return rare.sprite;
                case Rarity.Unique:
                    return unique.sprite;
                case Rarity.Currency:
                    return currency.sprite;
            }
        }
        return normal.sprite;
    }
    public Sprite getItem()
    {
        return mace.sprite;
    }
    public Sprite getStat(StatEle statEle)
    {
        switch (statEle)
        {
            case StatEle.Physical:
                return physical.sprite;
            case StatEle.Life:
                return life.sprite;
            case StatEle.Cold:
                return cold.sprite;
            case StatEle.Fire:
                return fire.sprite;
            case StatEle.Lightning:
                return lightning.sprite;
            case StatEle.Chaos:
                return chaos.sprite;
            case StatEle.Wild:
                return wild.sprite;
            case StatEle.Armour:
                return armour.sprite;
            default: return wild.sprite;
        }
    }
    public Sprite getTypeIcon(Type type)
    {
        switch (type)
        {
            case Type.OneHandedWeapon:
                return oneHandedTypeIcon.sprite;
            case Type.TwoHandedWeapon:
                return twoHandedTypeIcon.sprite;
            case Type.Chest:
                return chestTypeIcon.sprite;
            case Type.Shield:
                return shieldTypeIcon.sprite;
            case Type.Ring:
                return ringTypeIcon.sprite;
            case Type.Amulet:
                return amuletTypeIcon.sprite;
            default:
                return amuletTypeIcon.sprite;
        }
    }

}
