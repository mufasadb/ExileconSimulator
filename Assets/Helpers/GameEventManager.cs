using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{
    #region Singleton

    public static GameEventManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GlobalVariables");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] GameObject handUI;
    [SerializeField] GameObject fightUI;
    [SerializeField] GameObject rewardUI;
    [SerializeField] GameObject selectionUI;
    [SerializeField] GameObject craftUI;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;
    [SerializeField] GameObject mapUI;
    [SerializeField] MapHandler mapHandler;
    [SerializeField] GameObject enemyUI;
    [SerializeField] TextMeshProUGUI cancelFightText;
    [SerializeField] GameObject optionsMenu;
    public bool handOpen = true;
    float menuCooldown = 0;

    public void OpenHand()
    {
        if (GlobalVariables.instance.cardsMovingCooldown <= 0)
        {

            OpenUIItem(handUI);
            OpenUIItem(leftButton);
            OpenUIItem(rightButton);

            GlobalVariables.instance.cardAnimation = false;
            if (handOpen != true)
            {
                Hand.instance.UpdateCardDisplay();
                AudioManager.instance.Play("cardSwipe");
                // StartCoroutine(ExecuteAfterTime(0.05f, () => { AudioManager.instance.Play("cardSwipe"); }));
                // StartCoroutine(ExecuteAfterTime(0.1f, () => { AudioManager.instance.Play("cardSwipe"); }));
                // StartCoroutine(ExecuteAfterTime(0.15f, () => { AudioManager.instance.Play("cardSwipe"); }));
                // StartCoroutine(ExecuteAfterTime(0.2f, () => { AudioManager.instance.Play("cardSwipe"); }));
                // StartCoroutine(ExecuteAfterTime(0.25f, () => { AudioManager.instance.Play("cardSwipe"); }));

            }
            handOpen = true;
            GlobalVariables.instance.cardsMovingCooldown = 1.5f;
        }
    }
    public void CloseHand()
    {
        if (GlobalVariables.instance.cardsMovingCooldown <= 0)
        {
            CloseUIItem(leftButton);
            CloseUIItem(rightButton);
            if (handOpen != false)
            {
                Hand.instance.CardsIntoDeck();
                GlobalVariables.instance.cardAnimation = true;
                StartCoroutine(ExecuteAfterTime(1.3f, () => { CloseUIItem(handUI); }));
                StartCoroutine(ExecuteAfterTime(1.5f, () => { CloseUIItem(handUI); }));
            }

            handOpen = false;
            GlobalVariables.instance.cardsMovingCooldown = 1.5f;
        }
    }
    IEnumerator ExecuteAfterTime(float time, Action func)
    {

        yield return new WaitForSeconds(time);
        CloseUIItem(handUI);
        // task();
    }

    public void BeginFightScreen()
    {
        // if (!isMapFight) { Debug.LogError("tried to initiate fight not in a map, but without a staff member (check fight handler initiate fight"); }
        OpenUIItem(fightUI);
        OpenUIItem(selectionUI);
        OpenUIItem(enemyUI);
        OpenHand();
        GlobalVariables.instance.preventMoving = true;
    }
    public void EndFightScreen()
    {
        CloseUIItem(fightUI);
        CloseUIItem(selectionUI);
        CloseHand();
        CloseUIItem(enemyUI);
        GlobalVariables.instance.preventMoving = false;
    }
    public void BeginRewardScreen()
    {
        // OpenUIItem(fightUI);
        OpenHand();
        OpenUIItem(rewardUI);
        CloseUIItem(selectionUI);
        CloseUIItem(fightUI);
        CloseUIItem(enemyUI);
        GlobalVariables.instance.preventMoving = true;
    }
    public void EndRewardScreen()
    {
        EndFightScreen();
        FightHandler.instance.handleFightEnd();
        CloseUIItem(selectionUI);
        // CloseHand();
        CloseUIItem(craftUI);
        CloseUIItem(rewardUI);

    }
    public void BeginMapScreen()
    {
        mapHandler.ShowMapButtons();
        OpenHand();
        OpenUIItem(mapUI);
        GlobalVariables.instance.preventMoving = true;
    }
    public void EndMapScreen()
    {
        // CloseUIItem(fightUI);
        CloseHand();
        // CloseUIItem(selectionUI);
        CloseUIItem(mapUI);
        GlobalVariables.instance.preventMoving = false;
    }
    public void BeginCraftScreen()
    {
        OpenHand();
        OpenUIItem(craftUI);
        GlobalVariables.instance.preventMoving = true;
    }
    public void EndCraftScreen()
    {
        CloseUIItem(craftUI);
        GlobalVariables.instance.preventMoving = false;
    }
    public void CloseAllUI()
    {
        if (GlobalVariables.instance.clipPendingCount == 0 && GlobalVariables.instance.rewardPendingCount == 0 && GlobalVariables.instance.selectionState != SelectionState.InMaps)
        {
            CloseUIItem(fightUI);
            CloseUIItem(selectionUI);
            CloseHand();
            CloseUIItem(craftUI);
            CloseUIItem(rewardUI);
            CloseUIItem(enemyUI);
            GlobalVariables.instance.preventMoving = false;
            FightHandler.instance.CancelFight();
        }
    }
    public void StepToNextFight()
    {
        if (mapHandler.isInMap)
        {
            if (mapHandler.fightsRemaining > 0)
            {
                FightHandler.instance.handleFightEnd();
                CloseUIItem(rewardUI);
                OpenUIItem(selectionUI);
                OpenUIItem(fightUI);
                OpenUIItem(enemyUI);
                mapHandler.NextFight();
                CloseHand();
            }
            else
            {
                EndRewardScreen();
                mapHandler.endMapInteraction();
            }
        }
        else
        {
            EndRewardScreen();
        }
    }

    private void OpenUIItem(GameObject UIItem)
    {
        if (!UIItem.activeSelf) { UIItem.SetActive(true); }
    }
    private void CloseUIItem(GameObject UIItem)
    {
        if (UIItem.activeSelf) { UIItem.SetActive(false); };
    }
    public void DisplayError(string errorString)
    {
        GlobalVariables.instance.errorHandler.NewError(errorString);
    }
    public void MapWeaponSelection()
    {
        OpenUIItem(selectionUI);
        GlobalVariables.instance.selectionState = SelectionState.EnteringMaps;
    }
    public void SetFightCancelButtonToCancel() { cancelFightText.text = "Cancel"; }
    public void SetFightCancelButtonToLeaveMap() { cancelFightText.text = "Leave Map"; }
    public void WinGame()
    {
        PlayerPrefs.SetFloat("WonAt", GlobalVariables.instance.timer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void LoseGame()
    {
        PlayerPrefs.SetFloat("WonAt", -1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Update()
    {
        if (menuCooldown > 0) menuCooldown -= Time.deltaTime;
        if (Input.GetButton("Cancel"))
        {
            if (menuCooldown <= 0) { menuCooldown = 0.3f; CancelButton(); }
        }
    }
    public void CancelButton()
    {
        if (handOpen) CloseAllUI();
        else
        {
            if (optionsMenu.activeSelf) optionsMenu.SetActive(false);
            else optionsMenu.SetActive(true);
        }
    }
}
