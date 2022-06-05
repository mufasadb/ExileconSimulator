using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedJewellery : MonoBehaviour
{
    public CardDisplay cardDisplay;
    public Transform selectionContainer;
    public Vector3 basePosition;
    public GameObject asDefenceButton;
    public GameObject asAttackButton;


    // Start is called before the first frame update

    public void Start()
    {
        basePosition = selectionContainer.position;
    }

    // Update is called once per frame
    public void UpdatePosition()
    {
        if (cardDisplay != null)
        {
            if (cardDisplay.asWeapon)
            {
                selectionContainer.position = basePosition + new Vector3(-120, 0, 0);
                asDefenceButton.SetActive(true);
                asAttackButton.SetActive(false);
            }
            else
            {
                selectionContainer.position = basePosition + new Vector3(120, 0, 0);
                asAttackButton.SetActive(true);
                asDefenceButton.SetActive(false);
            }
            // AsDefence();
            cardDisplay.destination = selectionContainer.position;
        }
    }

    public void AddCard(CardDisplay _cardDisplay) { cardDisplay = _cardDisplay; }
    public void RmoveCard()
    {
        cardDisplay = null;

    }
    public void AsWeapon()
    {
        if (cardDisplay != null)
        {

            cardDisplay.AsWeaponTrue();
            UpdatePosition();
            Hand.instance.cardSelection.statCalc();
            FightHandler.instance.reCalculateStats();
        }
    }
    public void AsDefence()
    {
        if (cardDisplay != null)
        {

            cardDisplay.AsDefenceTrue();
            UpdatePosition();
            Hand.instance.cardSelection.statCalc();
            FightHandler.instance.reCalculateStats();
        }
    }

}