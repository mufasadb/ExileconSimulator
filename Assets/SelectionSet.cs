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
    // public void FindOpenContainerForType(Type type)
    // {
    //     SelectionContainer selectedContainer = selectionContainers.Find(cont => cont.type == type && cont.isUsed == false);
    //     if (selectedContainer) { }
    // }
    // public void FindContainerForType(Type type)
    // {

    // }
    // private List<SelectionContainer> FindContainerByType(Type type, bool empty)
    // {
    //     if (empty)
    //     {

    //     }else{

    //     }
    //     return selectionContainers.FindAll(cont => cont.type == type);
    // }
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
