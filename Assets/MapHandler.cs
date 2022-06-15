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
    [SerializeField] private GameObject doMap;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject mapSelector;

    public void DoMap()
    {
        isInMap = true;
        HideMapButtons();
        //set fight tier to maps fight tier  - 1 TODO:
        currentFightTier = 0;

        NextFight();
    }
    public void NextFight()
    {
        currentFightTier++;
        FightHandler.instance.InitiateFight(MonsterDataSystem.instance.monsterDataSet.GetMonsterBaseData(currentFightTier));
        // GlobalVariables.instance
    }
    public void HideMapButtons()
    {
        doMap.SetActive(false);
        cancelButton.SetActive(false);
        GlobalVariables.instance.mapSelection.MoveContainer(false);

    }
    public void ShowMapButtons()
    {
        doMap.SetActive(true);
        cancelButton.SetActive(true);
        GlobalVariables.instance.mapSelection.MoveContainer(true);
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
        currentFightTier = 0;
        fightsRemaining = 0;
        ShowMapButtons();
    }
}
