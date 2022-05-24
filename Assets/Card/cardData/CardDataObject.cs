using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardDataObject
{
    // public Stats implicits;
    public string name;
    public string implicits;
    public int tier;
    public bool isUnique;
    public Type type;
    // public CardDataObject(){
    //     // name = cardDataObject.name;
    //     // implicits = cardDataObject.implicits;
    //     // tier = cardDataObject.tier;
    //     // isUnique = cardDataObject.isUnique;
    //     name = "Shiny Sword";
    //     implicits = "Physical, Physical";
    //     tier = 1;
    //     isUnique = false;
    //     type = Type.Amulet;
    // }
}
