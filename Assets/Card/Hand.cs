using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    #region Singleton
    public static Hand instance;
    public int handScrollIndex = 0;
    private GameObject handContainer;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Hand");
            return;
        }
        instance = this;
    }
    #endregion
    public List<Card> hand = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        handContainer = GameObject.FindGameObjectWithTag("Hand");
        DoHandGen();
    }
    private void DoHandGen()
    {
        GameObject cardPrefab = PrefabHolder.instance.CardPrefab;

        for (int i = 0; i < 20; i++)
        {
            hand.Add(Card.CreateInstance(1, i.ToString()));
            Vector3 position = new Vector3(100, 500, 0);
            var card = cardPrefab.GetComponent<CardDisplay>();
            card.card = hand[i];
            // Debug.Log(PrefabHolder.instance.CardPrefab);
            Instantiate(PrefabHolder.instance.CardPrefab, position, Quaternion.identity, handContainer.transform);
        }
        SortHand();
        UpdateCardDisplay();
    }
    public void UpdateScrollIndex(bool moveLeft)
    {
        if (moveLeft)
        {
            handScrollIndex -= 1;
        }
        else
        {
            handScrollIndex += 1;
        }
        UpdateCardDisplay();
    }
    public void UpdateCardDisplay()
    {

        for (int i = 0; i < handContainer.transform.childCount; i++)
        {

            GameObject card = handContainer.transform.GetChild(i).gameObject;
            card.GetComponent<CardDisplay>().repositionInHand(i + handScrollIndex, handContainer.transform.childCount, handContainer.transform.position);
        }
    }
    public Card getCard()
    {
        if (hand.Count == 0)
        {
            Debug.LogWarning("hand length is 0");
        }
        return hand[0];
    }
    public void SortHand()
    {
        hand.Sort((c1, c2) => c1.rarity.CompareTo(c2.rarity));
        hand.Sort((c1, c2) => c1.type.CompareTo(c2.type));
        hand.Sort((c1, c2) => c1.durability.CompareTo(c2.durability));
    }

}
