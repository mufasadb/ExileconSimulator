using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSet : MonoBehaviour
{
    #region Singleton
    public static SelectionSet instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Selection  Container");
            return;
        }
        instance = this;

    }
    #endregion
    public List<SelectionContainer> selectionContainers = new List<SelectionContainer>();
    public SelectionContainer FindOpenContainerForType(Type type)
    {
        List<SelectionContainer> typedContainers = selectionContainers.FindAll(cont => cont.type == type && cont.isUsed == false);
        foreach (SelectionContainer selCont in typedContainers)
        {
            if (!selCont.isUsed) { return selCont; }
        }
        return typedContainers[0];
    }
    public CardActionHandler GetCardByContainer(int selectContainerUID)
    {
        foreach (GameObject card in Hand.instance.hand)
        {
            CardActionHandler cardAH = card.GetComponent<CardActionHandler>();
            if (cardAH.homeContainerID == selectContainerUID)
            {
                return cardAH;
            }
        }
        return null;
    }

    public SelectionContainer GetContainerByID(int id)
    {
        return selectionContainers.Find(cont => cont.GetInstanceID() == id);
    }
    public Vector3 FindByGUID(int id)
    {
        SelectionContainer selCont = selectionContainers.Find(cont => cont.GetInstanceID() == id);
        return selCont.transform.position;
    }
    public GameObject FindParentContainerByGUID(int id)
    {
        SelectionContainer selCont = selectionContainers.Find(cont => cont.GetInstanceID() == id);
        return selCont.gameObject;
    }
    public void DetachByID(int id)
    {
        SelectionContainer selCont = selectionContainers.Find(cont => cont.GetInstanceID() == id);
        if (selCont != null)
        {
            selCont.isUsed = false;
        }
    }
}
