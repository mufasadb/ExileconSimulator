using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IDropHandler
{
    public Card card;
    // Start is called before the first frame update
    private Image baseImage;
    public Image typeIcon;
    public Image itemImage;
    public TMPro.TMP_Text title;
    public GameObject craftingContainer;
    public GameObject implicitContainer;
    public GameObject craftingSticker;
    public GameObject qualitySticker;
    private List<Image> implicitList;
    public GameObject explicitContainer;
    private int horizontalCardSpacing = 120; //positions spacing between cards
    private int verticalCardSpacing = -10; //positions spacing between cards
    private Vector3 cachedScale;
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
    [SerializeField] private GameObject asWeaponTrue;
    [SerializeField] private GameObject asDefenceTrue;
    public bool asWeapon = false;
    public void DoClip(int clipCount)
    {

        //animate and noise
        card.durability -= clipCount;
        destination = new Vector3(1920 / 2, 1080 / 2, 0);
        baseImage.sprite = CardImageHolder.instance.getBase(card.rarity, card.durability);
    }
    private void Awake()
    {
        // parentContainer = parentCont;
        selectedContainer = GlobalVariables.instance.SelectionContainer;
        rendererObj = GetComponent<Renderer>();
        baseImage = GetComponent<Image>();
        typeIcon.sprite = CardImageHolder.instance.getTypeIcon(card.type);
        if (card.type == Type.TwoHandedWeapon || card.type == Type.OneHandedWeapon) { asWeapon = true; }
        MoveTypeIcon(card.type);
        itemImage.sprite = CardImageHolder.instance.getItem(card.name, card.type);
        baseImage.sprite = CardImageHolder.instance.getBase(card.rarity, card.durability);
        title.text = card.name;
        if (card.type != Type.Currency && card.type != Type.Map)
        {
            card.implicits.StatDisplay(implicitContainer.transform);
            card.explicits.StatDisplay(explicitContainer.transform);
        }
        // createImplicits();
        canvas = GetComponent<Canvas>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (selected)
        {
            //tODO 
            // this.DoUnselect();
        }
    }
    public void AsWeaponTrue()
    {
        Debug.Log("as weapon");
        asWeapon = true;
        asWeaponTrue.SetActive(false);
        asDefenceTrue.SetActive(true);
        if (FightHandler.instance.isFighting) { FightHandler.instance.reCalculateStats(); }
    }
    public void AsDefenceTrue()
    {
        Debug.Log("as defence");
        asWeapon = false;
        asWeaponTrue.SetActive(true);
        asDefenceTrue.SetActive(false);
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
        cachedScale = transform.localScale;
    }

    public void Smallerise()
    {
        transform.SetSiblingIndex(sortedOrderIndex);
        if (selected)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 1);
        }
        else
        {
            transform.localScale = cachedScale;
        }
    }
    public void Biggerise()
    {
        transform.SetAsLastSibling();
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }
    public void DoSelect(Vector3 position, GameObject newParent)
    {
        selected = true;
        transform.localScale = new Vector3(0.4f, 0.4f, 1);
        transform.SetParent(newParent.transform);
        // transform.localPosition = position;
        destination = position;
        // transform.parent = selectedContainer.transform;
    }
    public void DoUnselect()
    {
        selected = false;
        transform.localScale = cachedScale;
        transform.SetParent(parentContainer.transform);
        Hand.instance.cardSelection.UnSelect(this);
        Hand.instance.UpdateCardDisplay();
    }
    public void MoveBy(Vector3 moveAmount)
    {
        destination = destination + moveAmount;
    }
    private void Update()
    {
        // if (!selected)
        // {
        if (onHoverCooldown > 0)
        {
            onHoverCooldown -= Time.deltaTime;
        }
        if (transform.position != destination)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 5f);
        }
        // }
    }

    public void repositionInHand(int myPositionIndex, int handCount, Vector3 handPosition)
    {
        float halfWay = handCount / 2;
        float position = myPositionIndex - halfWay;
        if (Mathf.Abs(position) > 5) { position = position * 2; }
        float scale = 0.8f - Mathf.Abs(position) * 0.01f;
        transform.localScale = new Vector3(scale, scale, 1);
        float x = 0;
        if (myPositionIndex != halfWay) { x = position * horizontalCardSpacing; }
        x += (1920 / 2);
        float y = Mathf.Abs(position) * verticalCardSpacing + 200 - scale;
        destination = new Vector3(x, y, 0);
    }
    private void createImplicits()
    {
        Debug.Log(card.implicits);

    }
}

