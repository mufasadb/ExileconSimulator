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
    public SpriteHolder rare;
    public SpriteHolder magic;
    public SpriteHolder unique;
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
    public Sprite getBase()
    {
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
