using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest
{
    public bool isComplete;
    public string defeatEnemy;
    public string craftWith;
    public int tier;
    public GameObject tickBox;
    public void TickTickBox()
    {
        tickBox.SetActive(true);
    }
}
