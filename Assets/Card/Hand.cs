using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Hand : MonoBehaviour
{
    public int handScrollIndex = -2;
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
            cardDisplay.destination = new Vector3(1920 - 150, 1080 - 100, 0);
            // cardDisplay.HideBack();
            if (handContainer.activeSelf)
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
        GameObject statCardPrefab = PrefabHolder.instance.ToolStatPrefab;
        GameObject howCardPrefab = PrefabHolder.instance.ToolQuickReferencePrefab;
        Vector3 position = new Vector3(1920 - 150, 1080 - 50, 0);
        for (int i = 0; i < 10; i++)
        {
            Card newCard = Card.CreateInstance(1, false);
            if (i == 0) { newCard = Card.CreateSpecificInstance("Wooden Hammer"); }
            if (i == 1) { newCard = Card.CreateSpecificInstance("Full Plate"); }
            var card = cardPrefab.GetComponent<CardDisplay>();
            card.parentContainer = handContainer;
            card.card = newCard;
            hand.Add(Instantiate(PrefabHolder.instance.CardPrefab, position, Quaternion.identity, handContainer.transform));
            hand[hand.Count - 1].name = card.card.name;

        }
        hand.Add(Instantiate(statCardPrefab, position, Quaternion.identity, handContainer.transform));
        hand[hand.Count - 1].name = "QuickReferenceStat";
        hand[hand.Count - 1].GetComponent<CardDisplay>().parentContainer = handContainer;
        hand.Add(Instantiate(howCardPrefab, position, Quaternion.identity, handContainer.transform));
        hand[hand.Count - 1].GetComponent<CardDisplay>().parentContainer = handContainer;
        hand[hand.Count - 1].name = "QuickReferenceHowTo";
        CardsIntoDeck();
        SortHand();
        UpdateCardDisplay();
        GlobalVariables.instance.selectionState = SelectionState.Fight;
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
        SortHand();
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
        if (Settings.instance.sortBy == SortBy.Rarity) hand = hand.OrderBy(x => x.GetComponent<CardDisplay>().card.rarity).ThenBy(x => x.GetComponent<CardDisplay>().card.durability).ThenBy(x => x.GetInstanceID()).ToList();
        if (Settings.instance.sortBy == SortBy.Type) hand = hand.OrderBy(x => x.GetComponent<CardDisplay>().card.type).ThenBy(x => x.GetComponent<CardDisplay>().card.durability).ThenBy(x => x.GetInstanceID()).ToList();
        if (Settings.instance.sortBy == SortBy.Name) hand = hand.OrderBy(x => x.GetComponent<CardDisplay>().card.name).ThenBy(x => x.GetComponent<CardDisplay>().card.durability).ThenBy(x => x.GetInstanceID()).ToList();
        if (Settings.instance.sortBy == SortBy.Durability) hand = hand.OrderBy(x => x.GetComponent<CardDisplay>().card.durability).ThenBy(x => x.GetComponent<CardDisplay>().card.type).ThenBy(x => x.GetInstanceID()).ToList();


        for (int i = 0; i < hand.Count; i++)
        {
            if (!hand[i].GetComponent<CardDisplay>().selected) hand[i].transform.SetSiblingIndex(i);
        }
        var quickRef = hand.Find(c => c.name == "QuickReferenceStat");
        quickRef.transform.SetAsFirstSibling();
        var howTo = hand.Find(c => c.name == "QuickReferenceHowTo");
        howTo.transform.SetAsFirstSibling();

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
