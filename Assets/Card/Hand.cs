using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public int handScrollIndex = 0;
    [SerializeField] public GameObject handContainer;
    public float cooldDownHover;
    public CardSelection cardSelection;
    [SerializeField] private GameObject rewardContainer;
    #region Singleton

    public static Hand instance;
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
    float timePressed = 0;
    float pressCooldown = 0;
    public List<GameObject> hand = new List<GameObject>();


    //remove for test
    float testCooldown = 0;
    float xOffset;
    float yOffset;
    public int seed;
    [SerializeField] float scale;
    float timesPressed;
    // Start is called before the first frame update
    void Start()
    {
        // handContainer = GameObject.FindGameObjectWithTag("Hand");
        cardSelection = handContainer.GetComponent<CardSelection>();
        rewardContainer = GlobalVariables.instance.RewardContainer;
        DoHandGen();



        //remove for test
        Random.InitState(seed);
        xOffset = Random.value;
        yOffset = Random.value;
    }
    public void AddCardToHand(GameObject cardObject)
    {
        CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
        hand.Add(cardDisplay.gameObject);
        cardDisplay.parentContainer = handContainer;
        cardObject.transform.SetParent(handContainer.transform);
        SortHand();
        UpdateCardDisplay();
        RewardSelection rewardSelection = cardObject.GetComponent<RewardSelection>();
        Destroy(rewardSelection);

    }
    public void CardsIntoDeck()
    {
        for (int i = 0; i < handContainer.transform.childCount; i++)
        // for (int i = 0; i < handContainer.Count; i++)
        {
            GameObject card = handContainer.transform.GetChild(i).gameObject;
            CardDisplay cardDisplay = card.GetComponent<CardDisplay>();
            // cardDisplay.transform.position = new Vector3(1920 - 100, 1080 - 100);
            cardDisplay.destination = new Vector3(1920 - 150, 1080 - 100);
            // cardDisplay.HideBack();
            cardDisplay.gameObject.GetComponent<Animator>().SetBool("facingForward", false);
        }
    }
    // public void CardOutOfDeck()
    // {
    //     for (int i = 0; i < handContainer.transform.childCount; i++)
    //     // for (int i = 0; i < handContainer.Count; i++)
    //     {
    //         GameObject card = handContainer.transform.GetChild(i).gameObject;
    //         CardDisplay cardDisplay = card.GetComponent<CardDisplay>();
    //         cardDisplay.
    //     }
    // }

    private void DoHandGen()
    {
        GameObject cardPrefab = PrefabHolder.instance.CardPrefab;
        for (int i = 0; i < 30; i++)
        {
            Card newCard = Card.CreateInstance(Random.Range(2, 3), i.ToString());
            Vector3 position = new Vector3(1920 - 150, 1080 - 50);
            var card = cardPrefab.GetComponent<CardDisplay>();
            card.parentContainer = handContainer;
            card.card = newCard;
            hand.Add(Instantiate(PrefabHolder.instance.CardPrefab, position, Quaternion.identity, handContainer.transform));

        }
        CardsIntoDeck();
        SortHand();
        UpdateCardDisplay();
    }
    public void UpdateScrollIndex(bool moveLeft)
    {
        if (moveLeft)
        {
            if (handScrollIndex > -hand.Count / 2)
            {
                handScrollIndex -= 1;
            }
        }
        else
        {
            if (handScrollIndex < hand.Count / 2)
            {
                handScrollIndex += 1;
            }
        }
        UpdateCardDisplay();
    }
    public void UpdateCardDisplay()
    {
        for (int i = 0; i < handContainer.transform.childCount; i++)
        {
            GameObject card = handContainer.transform.GetChild(i).gameObject;
            CardDisplay cardDisplay = card.GetComponent<CardDisplay>();
            cardDisplay.repositionInHand(i + handScrollIndex, handContainer.transform.childCount, handContainer.transform.position);
            cardDisplay.updatePositionScaleCaches(i);
        }
    }
    public void SortHand()
    {
        if (Settings.instance.sortBy == SortBy.Rarity) hand.Sort((c1, c2) => c1.GetComponent<CardDisplay>().card.rarity.CompareTo(c2.GetComponent<CardDisplay>().card.rarity));
        if (Settings.instance.sortBy == SortBy.Type) hand.Sort((c1, c2) => c1.GetComponent<CardDisplay>().card.type.CompareTo(c2.GetComponent<CardDisplay>().card.type));
        if (Settings.instance.sortBy == SortBy.Durability) hand.Sort((c1, c2) => c1.GetComponent<CardDisplay>().card.durability.CompareTo(c2.GetComponent<CardDisplay>().card.durability));
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.SetSiblingIndex(i);
        }
    }
    public void selectCard(CardDisplay card)
    {
        cardSelection.Select(card);
    }
    private void Update()
    {
        //updated in carddisplay, used to prevent weird flicker that happens with raycast in between frmames of card moving
        if (cooldDownHover > 0) { cooldDownHover -= Time.deltaTime; }
        if (Input.GetButton("Test"))
        {
            if (testCooldown <= 0)
            {

                // GetPerlin();
                testCooldown = 0.3f;
            }
            // for (int i = 0; i < handContainer.transform.childCount; i++)
            // {

            // }
        }
        if (Input.GetButton("Test2"))
        {
            if (testCooldown <= 0)
            {
                SortHand();
                UpdateCardDisplay();
                testCooldown = 0.3f;
            }
            // for (int i = 0; i < handContainer.transform.childCount; i++)
            // {
            //     GameObject card = handContainer.transform.GetChild(i).gameObject;
            //     CardDisplay cardDisplay = card.GetComponent<CardDisplay>();
            //     Debug.Log(cardDisplay.cardActionHandler.homeContainerID);
            // }
        }
        if (Input.GetButton("CardsRight")) { PressingDirectionLeft(true); }
        if (Input.GetButton("CardsLeft")) { PressingDirectionLeft(false); }
        if (pressCooldown > 0)
            pressCooldown -= Time.deltaTime;
        if (timePressed > 0)
            timePressed -= Time.deltaTime;
        if (testCooldown > 0)
            testCooldown -= Time.deltaTime;
    }

    private void PressingDirectionLeft(bool left)
    {
        if (pressCooldown <= 0)
        {
            UpdateScrollIndex(left);
            pressCooldown = 0.2f;
            if (timePressed > 1.6f)
            {
                UpdateScrollIndex(left);
                UpdateScrollIndex(left);
            }
        }
        timePressed += Time.deltaTime * 1.3f;

    }
}
