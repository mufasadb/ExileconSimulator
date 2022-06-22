using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    // Start is called before the first frame update
    public Image baseImage;
    public Image typeIcon;
    public Image itemImage;
    public TMPro.TMP_Text title;
    public GameObject craftingContainer;
    public GameObject implicitContainer;
    public GameObject craftingSticker;
    public GameObject qualitySticker;
    private List<Image> implicitList;
    public GameObject explicitContainer;
    [SerializeField] TMPro.TMP_Text descriptiveText;
    private int horizontalCardSpacing = 120; //positions spacing between cards
    private int verticalCardSpacing = -10; //positions spacing between cards
    private Canvas canvas;
    private float onHoverCooldown = 0;
    private Renderer rendererObj;
    public Vector3 destination;
    public int sortedOrderIndex;
    public bool selected;
    public GameObject parentContainer;
    private GameObject selectedContainer;
    private CardSelection cardselection;
    public bool isDragged = false;
    public float speed = 5;
    public bool asWeapon = false;
    private float soundCoolDown = 0;
    [SerializeField] GameObject back;
    public CardActionHandler cardActionHandler;

    private void Awake()
    {
        selectedContainer = GlobalVariables.instance.SelectionContainer;
        rendererObj = GetComponent<Renderer>();
        if (card.type != Type.Tool) typeIcon.sprite = CardImageHolder.instance.getTypeIcon(card.type);
        if (card.type == Type.TwoHandedWeapon || card.type == Type.OneHandedWeapon) { asWeapon = true; }
        MoveTypeIcon(card.type);
        itemImage.sprite = CardImageHolder.instance.getItem(card.name, card.type);

        if (card.name == "Quick Reference")
        {
            // baseImage.sprite = CardImageHolder.instance.getBase(Rarity.Currency, 2);
            itemImage.color = new Color(0, 0, 0, 0);
            typeIcon.color = new Color(0, 0, 0, 0);
        }
        else
        {
            descriptiveText.text = card.extraDescription;
            baseImage.sprite = CardImageHolder.instance.getBase(card.rarity, card.durability);
        }
        title.text = card.name;
        if (card.type != Type.Currency && card.type != Type.Map && card.type != Type.Tool)
        {
            card.implicits.StatDisplay(implicitContainer.transform);
            card.explicits.StatDisplay(explicitContainer.transform);
        }
        canvas = GetComponent<Canvas>();
        if (card.type == Type.Currency) descriptiveText.transform.localPosition = new Vector3(0, 85, 0);
    }
    public void DoClip(int clipCount)
    {
        int homeID = cardActionHandler.homeContainerID;
        DoUnselect();
        cardActionHandler.AttachToSelectionContainer(homeID);
        transform.SetParent(GlobalVariables.instance.RewardContainer.transform);
        gameObject.AddComponent<ClipAcceptance>();
        //animate and noise
        card.durability -= clipCount;
        destination = new Vector3(1920 / 2, 1080 / 2, 0);
        baseImage.sprite = CardImageHolder.instance.getBase(card.rarity, card.durability);

        StartCoroutine(DoLater.DoAfterXSeconds(1.8f, () => { AudioManager.instance.Play("cardClip"); }));
        StartCoroutine(DoLater.DoAfterXSeconds(1.1f, () => { Biggerise(); }));
        StartCoroutine(DoLater.DoAfterXSeconds(1.7f, () => { GlobalVariables.instance.clipTop.SetActive(true); }));
        StartCoroutine(DoLater.DoAfterXSeconds(1.7f, () => { GlobalVariables.instance.clipBottom.SetActive(true); }));
        StartCoroutine(DoLater.DoAfterXSeconds(3f, () => { Smallerise(); }));
    }
    public void AsWeaponTrue()
    {
        asWeapon = true;
        if (FightHandler.instance.isFighting) { FightHandler.instance.reCalculateStats(); }
    }
    public void CraftQuality(bool isWeapon)
    {
        qualitySticker.SetActive(true);
        if (isWeapon)
        {
            card.quality.physical = 1;
        }
        else
        {
            card.quality.armour = 1;
        }
        card.quality.StatDisplay(qualitySticker.transform);
        card.isCrafted = true;
    }
    public void AsDefenceTrue()
    {
        asWeapon = false;
        if (FightHandler.instance.isFighting) { FightHandler.instance.reCalculateStats(); }
    }
    private void MoveTypeIcon(Type type)
    {
        if (type == Type.TwoHandedWeapon)
        {
            typeIcon.rectTransform.localScale = new Vector3(1.1f, 2.2f, 1);
            typeIcon.rectTransform.localPosition = new Vector3(-106.8f, -33, 0);
        }
        if (type == Type.Shield)
        {
            typeIcon.rectTransform.localPosition = new Vector3(-107.2f, -50f, 0);
        }
        if (type == Type.Amulet)
        {
            typeIcon.rectTransform.localPosition = new Vector3(-107.2f, -111, 0);
        }
        if (type == Type.Ring)
        {
            typeIcon.rectTransform.localPosition = new Vector3(-107.2f, -141, 0);
        }
        if (type == Type.Chest)
        {
            typeIcon.rectTransform.localPosition = new Vector3(-107.2f, -79, 0);
        }
        if (type == Type.Currency)
        {
            typeIcon.color = new Color(0, 0, 0, 0);
        }
        if (type == Type.Map)
        {
            typeIcon.color = new Color(0, 0, 0, 0);
        }
    }
    public void updatePositionScaleCaches(int index)
    {
        sortedOrderIndex = index;
    }
    public void Smallerise()
    {
        if (selected)
        {
            baseImage.transform.localScale = new Vector3(1f, 1f, 1);
        }
        else
        {
            transform.SetSiblingIndex(sortedOrderIndex);
            baseImage.transform.localScale = new Vector3(1f, 1f, 1);
        }
        baseImage.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void Biggerise()
    {

        baseImage.transform.localScale = new Vector3(1.2f, 1.2f, 1);

        if (selected)
        {
            if (card.type == Type.Ring || card.type == Type.Amulet)
            {
                baseImage.transform.localPosition = new Vector3(200, 150, 0);
            }
            else if (card.type == Type.Ring || card.type == Type.Chest)
            {
                baseImage.transform.localPosition = new Vector3(200, 0, 0);
            }
            else if (card.type == Type.Map)
            {
                // baseImage.transform.localPosition = new Vector3()
            }
            else
            {
                baseImage.transform.localPosition = new Vector3(200, -150, 0);
            }
            if (card.type != Type.Map)
            {
                baseImage.transform.localScale = new Vector3(3f, 3f, 1);
            }
        }
        else
        {
            transform.SetAsLastSibling();
            baseImage.transform.localScale = new Vector3(2f, 2f, 1);
            baseImage.transform.localPosition = new Vector3(0, 150, 0);
        }

    }
    public void DoSelect(Vector3 position, GameObject newParent)
    {
        selected = true;
        transform.SetParent(newParent.transform);
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
        // transform.localPosition = position;
        destination = position;

        // transform.parent = selectedContainer.transform;
    }
    public void ShowBack()
    {
        back.SetActive(true);
    }
    public void HideBack()
    {
        back.SetActive(false);
    }
    public void DoUnselect()
    {
        selected = false;
        transform.SetParent(parentContainer.transform);
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        Hand.instance.cardSelection.UnSelect(this);
        Hand.instance.UpdateCardDisplay();
        AudioManager.instance.Play("cardSwipe");

        cardActionHandler.DetachSelectionContainer();
    }

    private void Update()
    {
        // if (!selected)
        // {
        if (onHoverCooldown > 0)
        {
            onHoverCooldown -= Time.deltaTime;
        }
        if (soundCoolDown > 0)
        {
            soundCoolDown -= Time.deltaTime;
        }
        if (transform.position != destination)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        }
        // }
    }

    public void repositionInHand(int myPositionIndex, int handCount, Vector3 handPosition)
    {
        float halfWay = handCount / 2;
        float position = myPositionIndex - halfWay;
        if (Mathf.Abs(position) > 5) { position = position * 2; }
        // float scale = 0.8f - Mathf.Abs(position) * 0.01f;
        float scale = 0.8f;
        transform.localScale = new Vector3(scale, scale, 1);
        float x = 0;
        if (myPositionIndex != halfWay) { x = position * horizontalCardSpacing; }
        x += (1920 / 2);
        float y = Mathf.Abs(position) * verticalCardSpacing + 200 - scale;
        destination = new Vector3(x, y, 0);
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().SetBool("facingForward", true);
        }
    }
    private void createImplicits()
    {
        Debug.Log(card.implicits);

    }
}

