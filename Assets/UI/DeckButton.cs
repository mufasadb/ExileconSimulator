using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckButton : MonoBehaviour
{
    public bool showHand = true;
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
        if (showHand) { ShowHand(false); }
        else { ShowHand(true); }
    }
    public void ShowHand(bool newVal)
    {
        showHand = newVal;
        leftButton.SetActive(showHand);
        rightButton.SetActive(showHand);
        handContainer.SetActive(showHand);
    }
}
