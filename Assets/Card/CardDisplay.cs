using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Card card;
    // Start is called before the first frame update
    private Image baseImage;
    public Image typeIcon;
    public Image itemImage;
    public TMPro.TMP_Text title;
    public GameObject craftingContainer;
    public GameObject implicitContainer;
    private List<Image> implicitList;
    public GameObject explicitContainer;
    private int horizontalCardSpacing = 120; //positions spacing between cards
    private int verticalCardSpacing = -10; //positions spacing between cards
    private Vector3 cachedScale;
    private Canvas canvas;
    private float onHoverCooldown = 0;
    private Renderer rendererObj;
    private Vector3 destination;
    public int sortedOrderIndex;
    public bool selected;
    public GameObject parentContainer;
    private GameObject selectedContainer;
    private CardSelection cardselection;

    private void Awake()
    {
        // parentContainer = parentCont;
        selectedContainer = GameObject.FindGameObjectWithTag("SelectedCardContainer");
        rendererObj = GetComponent<Renderer>();
        baseImage = GetComponent<Image>();
        typeIcon.sprite = CardImageHolder.instance.getTypeIcon(card.type);
        itemImage.sprite = CardImageHolder.instance.getItem();
        baseImage.sprite = CardImageHolder.instance.getBase(card.rarity, card.durability);
        title.text = card.name;
        card.implicits.StatDisplay(implicitContainer.transform);
        card.explicits.StatDisplay(explicitContainer.transform);
        // createImplicits();
        canvas = GetComponent<Canvas>();
    }
    public void updatePositionScaleCaches(int index)
    {
        sortedOrderIndex = index;
        cachedScale = transform.localScale;
    }
    public void OnPointerExit(PointerEventData eventData)
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

    public void OnPointerClick(PointerEventData evendData)
    {
        Hand.instance.selectCard(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Hand.instance.cooldDownHover <= 0)
        {
            transform.SetAsLastSibling();
            transform.localScale = new Vector3(1.2f, 1.2f, 1);
            Hand.instance.cooldDownHover = 0.1f;
        }

    }
    public void DoSelect(Vector3 position)
    {
        selected = !selected;
        transform.localScale = new Vector3(0.4f, 0.4f, 1);
        transform.SetParent(selectedContainer.transform);
        transform.localPosition = position;
        // transform.parent = selectedContainer.transform;
    }
    public void DoUnselect()
    {
        selected = !selected;
        transform.localScale = cachedScale;
        transform.SetParent(parentContainer.transform);
        Hand.instance.UpdateCardDisplay();
    }
    private void Update()
    {
        if (!selected)
        {
            if (onHoverCooldown > 0)
            {
                onHoverCooldown -= Time.deltaTime;
            }
            if (transform.position != destination)
            {
                transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 5f);
            }
        }
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

