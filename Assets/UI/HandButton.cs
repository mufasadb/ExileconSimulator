using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandButton : MonoBehaviour
{
    private Hand hand;
    private void Start()
    {
        hand = Hand.instance;
    }
    public void ButtonPressLeft() { hand.UpdateScrollIndex(true); }
    public void ButtonPressRight() { hand.UpdateScrollIndex(false); }

}
