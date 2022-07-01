using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialWindowController : MonoBehaviour
{
    #region Singleton
    public static TutorialWindowController instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Tutorial Window Container");
            return;
        }
        instance = this;

    }
    #endregion
    public TextMeshProUGUI tmPro;
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
    void Progress()
    {

    }
    void FirstMessage()
    {

    }
}

public enum TutorialProgressions { Deck, Move, Interact, Select, Fight }