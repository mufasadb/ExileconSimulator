using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightHandler : MonoBehaviour
{
    public bool isFighting;
    public GameObject playerAttack;
    public GameObject playerDefence;
    public GameObject enemyAttack;
    public GameObject enemyDefence;
    public GameObject FightUI;
    public TextMeshProUGUI text;
    public GameObject SelectionUI;
    public CardSelection cardSelection;
    private float seperatingDistance = 18;
    string targetName;
    int LastFoughtID;
    int heldFoughtID;
    // private StaffMember currentFightTarget;
    private Stats fightTargetAttack;
    private Stats fightTargetDefence;
    private Stats storedFightTargetAttack;
    private Stats storedFightTargetDefence;
    private int fightTargetTier;
    private bool isChrisFight = false;
    ClipMethod fightTargetClipMethod;
    int fightTargetClipCount;
    [SerializeField] QuestHandler questHandler;
    #region Singleton

    public static FightHandler instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Fight Handler");
            return;
        }
        instance = this;
    }
    #endregion
    public void Start()
    {
        cardSelection = Hand.instance.cardSelection;
    }
    public void doFight()
    {

        if (ResolveFight(Hand.instance.cardSelection.attack, Hand.instance.cardSelection.defence, fightTargetAttack, fightTargetDefence))
        {
            LastFoughtID = heldFoughtID;
            if (isChrisFight) GameEventManager.instance.WinGame();
            if (targetName == "Guardian of The Hydra") GlobalVariables.instance.RewardContainer.GetComponent<RewardHandler>().DoSpecificReward("Fragment of Hydra");
            else if (targetName == "Guardian of The Pheonix") GlobalVariables.instance.RewardContainer.GetComponent<RewardHandler>().DoSpecificReward("Fragment of Phoenix");
            else if (targetName == "Guardian of The Chimera") GlobalVariables.instance.RewardContainer.GetComponent<RewardHandler>().DoSpecificReward("Fragment of Chimera");
            else if (targetName == "Guardian of The Minotaur") GlobalVariables.instance.RewardContainer.GetComponent<RewardHandler>().DoSpecificReward("Fragment of Minotaur");
            else GlobalVariables.instance.RewardContainer.GetComponent<RewardHandler>().DoReward(cardSelection.extraDraw + 2, fightTargetTier, cardSelection.extraDraw + 1);

            questHandler.MarkDefeatQuestcomplete(targetName);
            List<CardDisplay> allSelectedCards = new List<CardDisplay>();
            foreach (var card in cardSelection.twoHandedWeapons) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.oneHandedWeapons) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.shields) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.amulets) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.rings) { allSelectedCards.Add(card); }
            foreach (var card in cardSelection.chests) { allSelectedCards.Add(card); }
            ClipMethodResolver.HandleClip(allSelectedCards, fightTargetClipMethod, fightTargetClipCount);


            //offer cards


        }

        //clip cards

        //reward cards
    }

    public bool ResolveFight(Stats playerAttack, Stats playerDefence, Stats enemyAttack, Stats enemyDefence)
    {
        //player Attack vs Enemy Defence.
        int playerSpare = 0;
        int playerWild = playerAttack.wild;
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.fire, enemyDefence.fire);
        #region "Hrimburn"
        if (playerWild < 0)
        {
            Debug.Log("before using cold my wild attack is " + playerWild);
            if (cardSelection.chests.Find(c => c.name == "Hrimburn"))
            {
                Debug.Log("my attack cold is " + playerAttack.cold);
                playerAttack.cold += playerWild;
                playerWild = 0;
                Debug.Log("my new attack cold is " + playerAttack.cold);
                if (playerAttack.cold < 0)
                {
                    announceResults("not enough Fire attack even with Hrimburn using cold"); return false;
                }
            }
            else
            {
                #endregion
                announceResults("not enough fire Attack"); return false;
            }
        }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.cold, enemyDefence.cold);
        if (playerWild < 0) { announceResults("not enough cold Attack"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.lightning, enemyDefence.lightning);
        if (playerWild < 0) { announceResults("not enough lightning Attack"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.physical, enemyDefence.physical);
        if (playerWild < 0) { announceResults("not enough physical Attack"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerAttack.chaos, enemyDefence.chaos);
        if (playerWild < 0) { announceResults("not enough chaos Attack"); return false; }
        playerWild += playerSpare;
        playerWild -= enemyDefence.wild;
        if (playerWild < 0) { announceResults("not enough spare stats Attack " + playerWild + " left over"); return false; }

        //player Defence vs Enemy Attack
        playerSpare = 0;
        playerWild = playerDefence.wild;
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.fire, enemyAttack.fire);
        if (playerWild < 0)
        {
            #region "Hrimburn"

            if (cardSelection.chests.Find(c => c.name == "Hrimburn"))
            {
                playerDefence.cold += playerWild;
                if (playerDefence.cold < 0)
                {
                    announceResults("not enough Fire defence even with Hrimburn using cold"); return false;
                }
            }
            else
            {
                #endregion
                announceResults("not enough fire Defence"); return false;
            }
        }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.cold, enemyAttack.cold);
        if (playerWild < 0) { announceResults("not enough cold Defence"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.lightning, enemyAttack.lightning);
        if (playerWild < 0) { announceResults("not enough lightning Defence"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.armour, enemyAttack.armour);
        if (playerWild < 0)
        {
            #region Atziris disfavour   
            if (cardSelection.twoHandedWeapons.Find(c => c.name == "Atziris Disfavour"))
            {
                playerDefence.life += playerWild;
                if (playerDefence.life < 0)
                {
                    announceResults("not enough Armour defence despite using Atizirs favour's 'life for armour'"); return false;
                }
            }
            else
            {
                #endregion
                announceResults("not enough armour Defence"); return false;
            }
        }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.life, enemyAttack.life);
        if (playerWild < 0) { announceResults("not enough defence Life"); return false; }
        (playerWild, playerSpare) = ResolveSingleStat(playerWild, playerSpare, playerDefence.chaos, enemyAttack.chaos);
        if (playerWild < 0) { announceResults("not enough chaos Defence"); return false; }
        playerWild += playerSpare;
        playerWild -= enemyAttack.wild;
        if (playerWild < 0) { announceResults("not enough spare stats Defence"); return false; }

        return true;
    }
    private (int playerWild, int playerSpare) ResolveSingleStat(int playerWild, int playerSpare, int playerVal, int enemyVal)
    {
        int remainingVal = playerVal - enemyVal;
        if (remainingVal < 0)
        {
            playerWild += remainingVal;
        }
        else if (remainingVal > 0)
        {
            playerSpare += remainingVal;
        }
        return (playerWild, playerSpare);
    }
    private void announceResults(string reason)
    {
        // comment out to clean up noise
        GlobalVariables.instance.errorHandler.NewError("You Lost the fight because: " + reason);
        Debug.Log(reason);
    }
    public void reCalculateStats()
    {
        fightTargetAttack = new Stats();
        fightTargetDefence = new Stats();
        if (storedFightTargetDefence != null)
        {
            CopyStats(storedFightTargetAttack, storedFightTargetDefence);
        }

        //Handle Unique Card Shavronnes Wrappings
        #region Handle Unique Edits to Stats
        if (cardSelection.chests.Find(c => c.name == "Shavronnes Wrappings"))
        {
            fightTargetAttack.chaos = 0;
            fightTargetDefence.chaos = 0;
        }
        if (cardSelection.chests.Find(c => c.name == "The Eternity Shroud"))
        {
            fightTargetDefence.chaos = 0;
        }
        if (cardSelection.oneHandedWeapons.Find(c => c.name == "Mjolner"))
        {
            cardSelection.attack.wild = cardSelection.attack.wild * 2;
            cardSelection.defence.wild = cardSelection.defence.wild * 2;
        }
        if (cardSelection.chests.Find(c => c.name == "Cloak of Defiance"))
        {
            fightTargetAttack.wild = 0;
        }
        if (cardSelection.oneHandedWeapons.Find(c => c.name == "Soul Taker"))
        {
            fightTargetAttack.wild += 3;
            fightTargetDefence.physical = 0;
            fightTargetDefence.cold = 0;
            fightTargetDefence.lightning = 0;
            fightTargetDefence.chaos = 0;
            fightTargetDefence.fire = 0;
            fightTargetDefence.life = 0;
            fightTargetDefence.armour = 0;
            fightTargetDefence.wild = 0;
        }
        if (cardSelection.chests.Find(c => c.name == "Tombfist"))
        {
            foreach (var card in cardSelection.oneHandedWeapons)
            {
                cardSelection.attack.physical += card.card.implicits.physical;
                cardSelection.attack.physical += card.card.explicits.physical;
                cardSelection.attack.life += card.card.implicits.life;
                cardSelection.attack.life += card.card.explicits.life;
                cardSelection.attack.armour += card.card.implicits.armour;
                cardSelection.attack.armour += card.card.explicits.armour;
                cardSelection.attack.chaos += card.card.implicits.chaos;
                cardSelection.attack.chaos += card.card.explicits.chaos;
                cardSelection.attack.wild += card.card.implicits.wild;
                cardSelection.attack.wild += card.card.explicits.wild;
                cardSelection.attack.cold += card.card.implicits.cold;
                cardSelection.attack.cold += card.card.explicits.cold;
                cardSelection.attack.fire += card.card.implicits.fire;
                cardSelection.attack.fire += card.card.explicits.fire;
                cardSelection.attack.lightning += card.card.implicits.lightning;
                cardSelection.attack.lightning += card.card.explicits.lightning;
            }
            foreach (var card in cardSelection.twoHandedWeapons)
            {
                cardSelection.attack.physical += card.card.implicits.physical;
                cardSelection.attack.physical += card.card.explicits.physical;
                cardSelection.attack.life += card.card.implicits.life;
                cardSelection.attack.life += card.card.explicits.life;
                cardSelection.attack.armour += card.card.implicits.armour;
                cardSelection.attack.armour += card.card.explicits.armour;
                cardSelection.attack.chaos += card.card.implicits.chaos;
                cardSelection.attack.chaos += card.card.explicits.chaos;
                cardSelection.attack.wild += card.card.implicits.wild;
                cardSelection.attack.wild += card.card.explicits.wild;
                cardSelection.attack.cold += card.card.implicits.cold;
                cardSelection.attack.cold += card.card.explicits.cold;
                cardSelection.attack.fire += card.card.implicits.fire;
                cardSelection.attack.fire += card.card.explicits.fire;
                cardSelection.attack.lightning += card.card.implicits.lightning;
                cardSelection.attack.lightning += card.card.explicits.lightning;
            }
        }
        #endregion
        foreach (Transform child in playerAttack.transform) { Destroy(child.gameObject); }
        foreach (Transform child in playerDefence.transform) { Destroy(child.gameObject); }
        foreach (Transform child in enemyAttack.transform) { Destroy(child.gameObject); }
        foreach (Transform child in enemyDefence.transform) { Destroy(child.gameObject); }
        if (cardSelection.attack != null)
        {
            statDisplay(cardSelection.attack, true, playerAttack.transform);
        }
        if (cardSelection.defence != null)
        {
            statDisplay(cardSelection.defence, true, playerDefence.transform);
        }
        if (fightTargetAttack != null)
        {
            statDisplay(fightTargetAttack, true, enemyAttack.transform);
        }
        if (fightTargetDefence != null)
        {
            statDisplay(fightTargetDefence, true, enemyDefence.transform);
        }

    }
    public void ChrisFight(GameObject TargetEnemy, StaffMember staffMember)
    {
        isChrisFight = true;
        InitiateFight(TargetEnemy, staffMember);
    }
    public bool InitiateFight(GameObject TargetEnemy, StaffMember staffMember)
    {
        if (!isFighting)
        {
            GlobalVariables.instance.atFrontOfQueue = false;
            int thisTargetID = TargetEnemy.GetInstanceID();
            if (LastFoughtID == thisTargetID)
            {
                GlobalVariables.instance.errorHandler.NewError("You can't fight the same enemy twice in a row");
                return false;
            }
            heldFoughtID = TargetEnemy.GetInstanceID();
            storedFightTargetAttack = staffMember.attack;
            storedFightTargetDefence = staffMember.defence;
            fightTargetAttack = new Stats();
            fightTargetDefence = new Stats();
            CopyStats(storedFightTargetAttack, storedFightTargetDefence);
            fightTargetClipMethod = staffMember.clipMethod;
            fightTargetClipCount = staffMember.clipCount;
            fightTargetTier = staffMember.tier;
            text.text = staffMember.name;
            targetName = staffMember.name;
            SetupStatsAndDisplaysForFight();
            return true;
        }
        return false;
    }
    public void InitiateFight(Monster monster)
    {
        if (!isFighting)
        {
            storedFightTargetAttack = monster.attack;
            storedFightTargetDefence = monster.defence;
            fightTargetAttack = new Stats();
            fightTargetDefence = new Stats();
            CopyStats(storedFightTargetAttack, storedFightTargetDefence);
            fightTargetClipMethod = monster.clipMethod;
            fightTargetClipCount = monster.clipCount;
            fightTargetTier = monster.tier;
            text.text = monster.name;
            SetupStatsAndDisplaysForFight();
        }
    }
    private void CopyStats(Stats staffAttack, Stats staffDefence)
    {
        fightTargetAttack.physical = staffAttack.physical;
        fightTargetAttack.life = staffAttack.life;
        fightTargetAttack.armour = staffAttack.armour;
        fightTargetAttack.cold = staffAttack.cold;
        fightTargetAttack.lightning = staffAttack.lightning;
        fightTargetAttack.fire = staffAttack.fire;
        fightTargetAttack.chaos = staffAttack.chaos;
        fightTargetAttack.wild = staffAttack.wild;

        fightTargetDefence.physical = staffDefence.physical;
        fightTargetDefence.life = staffDefence.life;
        fightTargetDefence.armour = staffDefence.armour;
        fightTargetDefence.cold = staffDefence.cold;
        fightTargetDefence.lightning = staffDefence.lightning;
        fightTargetDefence.fire = staffDefence.fire;
        fightTargetDefence.chaos = staffDefence.chaos;
        fightTargetDefence.wild = staffDefence.wild;
        //had to impliment a copy of stats otherwise updating stats with uniques would overwrite staff member original stats
    }

    private void SetupStatsAndDisplaysForFight()
    {
        isFighting = true;
        GameEventManager.instance.BeginFightScreen();
        reCalculateStats();
    }

    public void CancelFight()
    {
        if (GlobalVariables.instance.rewardPendingCount == 0)
        {
            handleFightEnd();
            GameEventManager.instance.EndFightScreen();
            if (GlobalVariables.instance.selectionState == SelectionState.InMaps)
            {
                MapHandler.instance.endMapInteraction();
            }
        }
    }
    public void handleFightEnd()
    {
        isFighting = false;
        removeChildren();
        fightTargetAttack = null;
        fightTargetDefence = null;
        fightTargetClipCount = 0;
        targetName = "";
    }
    public void removeChildren()
    {
        foreach (Transform child in playerAttack.transform) { Destroy(child.gameObject); }
        foreach (Transform child in playerDefence.transform) { Destroy(child.gameObject); }
        foreach (Transform child in enemyAttack.transform) { Destroy(child.gameObject); }
        foreach (Transform child in enemyDefence.transform) { Destroy(child.gameObject); }
    }
    void statDisplay(Stats stats, bool offence, Transform canvas)
    {
        List<string> statEleList = new List<string>();

        statEleList = CalculateAndAddStringsFromStatEle("Fire", stats.fire, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Cold", stats.cold, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Lightning", stats.lightning, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Physical", stats.physical, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Armour", stats.armour, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Life", stats.life, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Chaos", stats.chaos, statEleList);
        statEleList = CalculateAndAddStringsFromStatEle("Wild", stats.wild, statEleList);

        for (int i = 0; i < statEleList.Count; i++)
        {
            PositionAndCallStat(statEleList[i], i, canvas, statEleList.Count % 2 == 0, statEleList.Count, true);
        }
    }
    private List<string> CalculateAndAddStringsFromStatEle(string baseEleName, int statVal, List<string> statEleList)
    {
        if (baseEleName == "Wild")
        {
            if (statVal > 0) statEleList.Add(baseEleName + statVal);
        }
        else
        {
            int workingStat = statVal % 3;
            int statTriple = (int)Mathf.Floor(statVal / 3);
            for (int i = 0; i < workingStat; i++)
            {
                statEleList.Add(baseEleName);
            }
            for (int i = 0; i < statTriple; i++)
            {
                statEleList.Add(baseEleName + "Triple");
            }
        }
        return statEleList;
    }
    private int PositionAndCallStat(string statEle, int currentPosition, Transform canvas, bool evenStats, int totalStats, bool offence)
    {
        int workingStatCount = totalStats;
        int workingCurrentPosition = currentPosition;
        int y = 0;
        if (workingStatCount > 5)
        {
            evenStats = false;
            if (workingStatCount % 2 == 0) { evenStats = true; }
            y = 20;
            workingStatCount = totalStats / 2;
            if (workingCurrentPosition > workingStatCount)
            {
                workingCurrentPosition = workingCurrentPosition - workingStatCount;
                y = -20;
            }
            workingStatCount = workingStatCount + 1;
        }
        float x = (-(workingStatCount / 2) + workingCurrentPosition) * seperatingDistance;
        if (evenStats)
        {
            x = (-(workingStatCount / 2) + 0.5f + workingCurrentPosition) * seperatingDistance;
        }

        GameObject statIcon = AddStat(statEle);
        statIcon.transform.SetParent(canvas, false);
        statIcon.transform.localPosition = new Vector3(x, y, 0);
        currentPosition = currentPosition + 1;
        return currentPosition;
    }
    public GameObject AddStat(string statEle)
    {
        GameObject statIcon = new GameObject("statIcon");
        RectTransform rt = statIcon.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.localScale = new Vector2(.4f, .6f);
        Image image = statIcon.AddComponent<Image>();
        image.sprite = CardImageHolder.instance.getStatImage(statEle);
        rt.sizeDelta = new Vector2(image.sprite.bounds.size.x * 100, image.sprite.bounds.size.y * 100);
        return statIcon;
    }
}
