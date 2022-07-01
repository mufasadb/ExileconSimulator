using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActionHandler : MonoBehaviour
{
    private CardDisplay cardDisplay;
    public int homeContainerID = 0;
    void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
        if (cardDisplay == null) { Debug.LogError("couldn't find card display on card"); }
    }
    public void AttachToSelectionContainer(int instanceID)
    {
        homeContainerID = instanceID;
    }
    public void DetachSelectionContainer()
    {
        if (homeContainerID != 0)
        {

            SelectionSet.instance.DetachByID(homeContainerID);
            homeContainerID = 0;
        }
    }
    public void AutoSelect()
    {
        if (SelectionSet.instance != null)
        {
            SelectionContainer selCont = SelectionSet.instance.FindOpenContainerForType(cardDisplay.card.type);
            if (selCont.isUsed)
            {
                CardActionHandler cardToReplace = SelectionSet.instance.GetCardByContainer(selCont.GetInstanceID());
                cardToReplace.SelfUnselect();
            }
            selCont.TryToSelect(cardDisplay);
        }
    }
    private void SelfUnselect()
    {
        cardDisplay.DoUnselect();
    }
    public void GoToHomeContainer()
    {

        //this currently ignores durability requirements when replacing, but should only be called if in maps

        if (GlobalVariables.instance.selectionState != SelectionState.InMaps) { Debug.LogError("Directly Attached container without checking durability or type while not in maps"); }
        SelectionContainer selectionContainer = SelectionSet.instance.GetContainerByID(homeContainerID);
        selectionContainer.ActionSelection(cardDisplay);

    }

}
