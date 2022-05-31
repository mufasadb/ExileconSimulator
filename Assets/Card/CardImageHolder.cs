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

    [Header("Bases")]
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
    [Header("Stats")]
    public SpriteHolder physical;

    public SpriteHolder armour;

    public SpriteHolder life;

    public SpriteHolder cold;

    public SpriteHolder lightning;

    public SpriteHolder fire;

    public SpriteHolder chaos;

    public SpriteHolder wild;
    [Header("trippled stats")]
    public SpriteHolder physicalTriple;
    public SpriteHolder armourTriple;

    public SpriteHolder lifeTriple;

    public SpriteHolder coldTriple;

    public SpriteHolder lightningTriple;

    public SpriteHolder fireTriple;

    public SpriteHolder chaosTriple;

    public SpriteHolder wildTriple;
    [Header("Type Icons")]
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

    public Sprite getItem(string name, Type type)
    {
        // if (name == "Gladiator Armour")
        // {
        // string path = "Items/Chest/Gladiator Armour.png";
        string path = "Items/" + type.ToString() + "/" + name + ".png";
        // Sprite sprite = LoadSprite("Assets/Card/images/Items/Chest/Gladiator Armour.png");
        Sprite sprite = LoadSprite(path);

        // Debug.Log(sprite);
        return sprite;

        // }
        // return mace.sprite;
    }

    private Sprite LoadSprite(string path)
    {
        string fullPath = "Assets/Card/images/" + path;
        if (string.IsNullOrEmpty(fullPath)) return null;
        if (System.IO.File.Exists(fullPath))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(fullPath);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }
    public Sprite getStatImage(string statEle)
    {
        switch (statEle)
        {
            case "Physical":
                return physical.sprite;
            case "Life":
                return life.sprite;
            case "Cold":
                return cold.sprite;
            case "Fire":
                return fire.sprite;
            case "Lightning":
                return lightning.sprite;
            case "Chaos":
                return chaos.sprite;
            case "Wild":
                return wild.sprite;
            case "Armour":
                return armour.sprite;
            case "PhysicalTriple":
                return physicalTriple.sprite;
            case "LifeTriple":
                return lifeTriple.sprite;
            case "ColdTriple":
                return coldTriple.sprite;
            case "FireTriple":
                return fireTriple.sprite;
            case "LightningTriple":
                return lightningTriple.sprite;
            case "ChaosTriple":
                return chaosTriple.sprite;
            case "WildTriple":
                return wildTriple.sprite;
            case "ArmourTriple":
                return armourTriple.sprite;
            default:
                return wild.sprite;
        }
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
            default:
                return wild.sprite;
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
