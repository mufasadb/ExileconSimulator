using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    GameObject enterMapButton;
    GameObject mapSelectionContainer;
    CardDisplay map;
    public int fightsRemaining;
    public bool isInMap;
    private int currentFightTier;

    public void DoMap()
    {
        isInMap = true;
        
        //set fight tier to maps fight tier  - 1 TODO:
        currentFightTier = 0;

        NextFight();
    }
    public void NextFight()
    {
        currentFightTier ++;

        // GlobalVariables.instance
    }
    public void endMapInteraction()
    {
        if (isInMap)
        {
            Hand.instance.hand.Remove(map.card);
            Destroy(map.gameObject);
        }
        GameEventManager.instance.EndMapScreen();
        isInMap = false;
    }
}
