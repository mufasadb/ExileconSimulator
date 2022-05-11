using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    private int horizontalCardSpacing = 150; //positions spacing between cards
    private int verticalCardSpacing = -10; //positions spacing between cards
    private Vector3 cachedScale;
    private int cachedSiblingIndex;
    private Canvas canvas;
    private float onHoverCooldown = 0;
    private Renderer rendererObj;
    private Vector3 destination;

    void Start()
    {
        rendererObj = GetComponent<Renderer>();
        baseImage = GetComponent<Image>();
        typeIcon.sprite = CardImageHolder.instance.getTypeIcon(card.type);
        itemImage.sprite = CardImageHolder.instance.getItem();
        title.text = card.name;
        card.implicits.StatDisplay(implicitContainer.transform);
        card.explicits.StatDisplay(explicitContainer.transform);
        // createImplicits();
        canvas = GetComponent<Canvas>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        cachedSiblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        cachedScale = transform.localScale;
        transform.localScale = new Vector3(1.1f, 1.1f, 1);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.SetSiblingIndex(cachedSiblingIndex);
        transform.localScale = cachedScale;
    }
    private void Update()
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
    // private void Update()
    // {
    //     Debug.Log("d");
    //     // Quaternion lookRotation = Quaternion.LookRotation((transform.position - HandRotationAchor.position), Vector3.forward);
    //     Quaternion lookRotation = Quaternion.Euler(x, y, z);
    //     transform.rotation = lookRotation;

    public void repositionInHand(int myPositionIndex, int handCount, Vector3 handPosition)
    {
        float halfWay = handCount / 2;
        float position = myPositionIndex - halfWay;
        if (Mathf.Abs(position) > 5) { position = position * 2; }
        float scale = 1 - Mathf.Abs(position) * 0.05f;
        float x = 0;
        if (myPositionIndex != halfWay) { x = position * horizontalCardSpacing; }
        x += (1920 / 2);
        float y = Mathf.Abs(position) * verticalCardSpacing + 200;
        destination = new Vector3(x, y, 0);
        // transform.position = destination;

    }
    private void createImplicits()
    {
        Debug.Log(card.implicits);

    }
}

