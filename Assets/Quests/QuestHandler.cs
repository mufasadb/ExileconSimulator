using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHandler : MonoBehaviour
{

    public CraftHandler craftHandler;
    public RewardHandler rewardHandler;
    public GameObject questTier1Container;
    public GameObject questTier2Container;
    public GameObject questTier3Container;
    public GameObject questTier4Container;
    public GameObject questPrefab;
    [HideInInspector]
    public bool questTier1Received;
    [HideInInspector]
    public bool questTier2Received;
    [HideInInspector]
    public bool questTier3Received;
    [HideInInspector]
    public bool questTier4Received;
    [HideInInspector]
    public bool questTier1ReadyToReceive;
    [HideInInspector]
    public bool questTier2ReadyToReceive;
    [HideInInspector]
    public bool questTier3ReadyToReceive;
    [HideInInspector]
    public bool questTier4ReadyToReceive;
    public bool questTier1Minimized;
    public bool questTier2Minimized;
    public bool questTier3Minimized;
    public bool questTier4Minimized;
    public bool allMin = false;
    public Quest[] quests;
    private void Start()
    {
        foreach (Quest quest in quests)
        {
            if (quest.tier == 1) { AddQuest(quest, questTier1Container); }
            if (quest.tier == 2) { AddQuest(quest, questTier2Container); }
            if (quest.tier == 3) { AddQuest(quest, questTier3Container); }
            if (quest.tier == 4) { AddQuest(quest, questTier4Container); }
        }
    }
    private void AddQuest(Quest quest, GameObject container)
    {
        GameObject newQuest = Instantiate(questPrefab, container.transform.position, Quaternion.identity, container.transform);
        string questText;
        if (quest.defeatEnemy == "")
        {
            questText = "Craft with " + quest.craftWith;
        }
        else
        {
            questText = "Defeat " + quest.defeatEnemy;
        }
        newQuest.GetComponent<TextMeshProUGUI>().text = questText;
        quest.tickBox = newQuest.transform.Find("Tick").gameObject;
    }
    private void Update()
    {
        if (questTier1Minimized) { questTier1Container.transform.localScale = Vector3.Lerp(questTier1Container.transform.localScale, Vector3.zero, Time.deltaTime * 5f); } else { questTier1Container.transform.localScale = Vector3.Lerp(questTier1Container.transform.localScale, Vector3.one, Time.deltaTime * 5f); }
        if (questTier2Minimized) { questTier2Container.transform.localScale = Vector3.Lerp(questTier2Container.transform.localScale, Vector3.zero, Time.deltaTime * 5f); } else { questTier2Container.transform.localScale = Vector3.Lerp(questTier2Container.transform.localScale, Vector3.one, Time.deltaTime * 5f); }
        if (questTier3Minimized) { questTier3Container.transform.localScale = Vector3.Lerp(questTier3Container.transform.localScale, Vector3.zero, Time.deltaTime * 5f); } else { questTier3Container.transform.localScale = Vector3.Lerp(questTier3Container.transform.localScale, Vector3.one, Time.deltaTime * 5f); }
        if (questTier4Minimized) { questTier4Container.transform.localScale = Vector3.Lerp(questTier4Container.transform.localScale, Vector3.zero, Time.deltaTime * 5f); } else { questTier4Container.transform.localScale = Vector3.Lerp(questTier4Container.transform.localScale, Vector3.one, Time.deltaTime * 5f); }
    }
    public void ToggleMinimize()
    {
        if (allMin)
        {
            allMin = false;
            questTier1Minimized = false;
            questTier2Minimized = false;
            questTier3Minimized = false;
            questTier4Minimized = false;
        }
        else
        {
            allMin = true;
            questTier1Minimized = true;
            questTier2Minimized = true;
            questTier3Minimized = true;
            questTier4Minimized = true;
        }
    }
    public void TakeRewards()
    {
        if (questTier1ReadyToReceive && !questTier1Received) { questTier1Received = true; rewardHandler.DoReward(1, 2, 1); CheckIfTiercomplete(); return; }
        if (questTier2ReadyToReceive && !questTier2Received) { questTier2Received = true; rewardHandler.DoReward(1, 3, 1); CheckIfTiercomplete(); return; }
        if (questTier3ReadyToReceive && !questTier3Received) { questTier3Received = true; rewardHandler.DoReward(1, 4, 1); CheckIfTiercomplete(); return; }
        if (questTier4ReadyToReceive && !questTier4Received) { questTier4Received = true; rewardHandler.DoSpecificReward("map"); CheckIfTiercomplete(); return; }
    }
    public void MarkCraftQuestComplete(string carftingItem, string craftingCurrency)
    {
        foreach (var quest in quests)
        {
            if (quest.craftWith == carftingItem) { quest.isComplete = true; CheckIfTiercomplete(); quest.TickTickBox(); }
            if (quest.craftWith == craftingCurrency) { quest.isComplete = true; CheckIfTiercomplete(); quest.TickTickBox(); }
        }
    }
    public void MarkDefeatQuestcomplete(string enemyName)
    {
        foreach (var quest in quests)
        {
            if (quest.defeatEnemy == enemyName) { quest.isComplete = true; CheckIfTiercomplete(); quest.TickTickBox(); }
        }
    }
    public void CheckIfTiercomplete()
    {
        if (!questTier1Received) questTier1ReadyToReceive = CheckIndividualTierReceiving(questTier1ReadyToReceive, 1);
        if (!questTier2Received) questTier2ReadyToReceive = CheckIndividualTierReceiving(questTier2ReadyToReceive, 2);
        if (!questTier3Received) questTier3ReadyToReceive = CheckIndividualTierReceiving(questTier3ReadyToReceive, 3);
        if (!questTier4Received) questTier4ReadyToReceive = CheckIndividualTierReceiving(questTier4ReadyToReceive, 4);
    }
    public bool CheckIndividualTierReceiving(bool questTierToReceive, int tier)
    {
        if (!questTierToReceive)
        {
            questTierToReceive = true;
            foreach (var q in quests)
            {
                if (q.tier == tier && q.isComplete == false) { questTierToReceive = false; }
            }
        }
        if (questTierToReceive) craftHandler.DisplayAcceptRewardButton(true);
        return questTierToReceive;
    }
}
