using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckButton : MonoBehaviour
{
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject handContainer;
    // Start is called before the first frame update
    void Start()
    {
        ShowHand(false);
    }

    public void ToggleHand()
    {
        if (GlobalVariables.instance.rewardPendingCount == 0 && GlobalVariables.instance.clipPendingCount == 0)
        {

            if (GameEventManager.instance.handOpen)
            {
                ShowHand(false);
                // if (GlobalVariables.instance.SelectionContainer.activeInHierarchy == true) { GlobalVariables.instance.SelectionContainer.SetActive(false); }
            }
            else { ShowHand(true); }
        }
    }
    public void ShowHand(bool newVal)
    {
        if (newVal)
        {
            GameEventManager.instance.OpenHand();
        }
        else
        {
            GameEventManager.instance.CloseAllUI();
        }
        // leftButton.SetActive(showHand);
        // rightButton.SetActive(showHand);
        // handContainer.SetActive(showHand);
    }
}
