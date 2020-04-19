using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    GameplayUI gameplayUI;
      
    public ObstableSpawner ObstableSpawner;

    public bool isStarted = false;

    public List<Quest> allQuests = new List<Quest>();
    public Quest activeQuest = null;
    public float questStartTime;

    void Start()
    {
        gameplayUI = FindObjectOfType<GameplayUI>();
        StartCoroutine(GameProgress());
    }

    IEnumerator GameProgress()
    {
        while (!isStarted)
        {
            yield return null;
        }

        var textDelay = new WaitForSeconds(4f);

        gameplayUI.ShowDialog(DialogSide.Left, DialogPortrait.Haron, "Let's start.");
        yield return textDelay;

        gameplayUI.ShowDialog(DialogSide.Right, DialogPortrait.Aid, "Go-go-go!");

        StartCoroutine(HellProcess());
        StartCoroutine(QuestProcess());
    }

    IEnumerator HellProcess()
    {
        while (true)
        {
            if (Data.player.HellDoorPopulation <= 0)
            {
                yield return null;
                continue;
            }

            if (Data.player.HellPopulation >= Data.player.TargetHellPopulation)
            {
                HellAttack();
                Data.player.HellPopulation -= Data.player.TargetHellPopulation;
            }

            Data.player.HellPopulation += Data.consts.GoToHellRate * Time.deltaTime;
            Data.player.HellDoorPopulation -= Data.consts.GoToHellRate * Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator QuestProcess()
    {
        questStartTime = Time.time - Data.consts.QuestCooldownTime - Data.consts.QuestActiveTime;

        while (true)
        {
            if (Time.time > questStartTime + Data.consts.QuestCooldownTime + Data.consts.QuestActiveTime && !Data.player.gotQuest)
            {
                questStartTime = Time.time;
                activeQuest = allQuests[Random.Range(0, allQuests.Count)];
                //Debug.Log(activeQuest);
            }

            if (Time.time > questStartTime + Data.consts.QuestActiveTime && !Data.player.gotQuest)
            {
                questStartTime = Time.time;
                activeQuest = null;
            }

            yield return null;
        }
    }

    public void CollectSouls(ISoulsSource source)
    {
        if(source.SoulsCount > 0 && Data.player.CurrentBoatCapacity < Data.player.MaxBoatCapacity)
        {
            var collectedAmount = source.CollectSpeed * Time.deltaTime;
            source.SoulsCount = Mathf.Max(source.SoulsCount - collectedAmount, 0f);
            Data.player.CurrentBoatCapacity = Mathf.Min(Data.player.CurrentBoatCapacity + collectedAmount, Data.player.MaxBoatCapacity);
        }
    }

    public void RessurectSouls()
    {
        if (Data.player.CurrentBoatCapacity > 0)
        {
            var amount = Data.consts.RessurectRate * Time.deltaTime;
            Data.player.WorldPopulation += amount;
            Data.player.CurrentBoatCapacity = Mathf.Max(Data.player.CurrentBoatCapacity - amount, 0f);
            Data.player.Coins += Data.consts.CoinsRate * amount;
        }

        if(Data.player.currentQuest != null)
        {
            FinishQuest();           
        }
    }
    
    public void ShowQuestMessage()
    {
        gameplayUI.ShowQuestMessage(activeQuest.startMessage);
    }

    public void AcceptQuest()
    {
        Data.player.gotQuest = true;
        Data.player.currentQuest = activeQuest;
        Debug.Log(activeQuest);
    }

    public void FinishQuest()
    {
        gameplayUI.ShowQuestMessage(activeQuest.finishMessage);
        Data.player.currentQuest = null;
        Data.player.gotQuest = false;
        Debug.Log("QuestFinished");
    }

    [ContextMenu("HellAttack")]
    public void HellAttack()
    {
        gameplayUI.ShowDialog(DialogSide.Right, DialogPortrait.Tanatos, "I'll kill you all!");

        //for (int i = 0; i < obstaclesSpawnCount; i++)
        //{
        //    var pos = new Vector3(Random.Range(-LocationBounds.x, LocationBounds.x), 0f, Random.Range(-LocationBounds.z, LocationBounds.z));
        //    Instantiate(ObstaclePrefab, pos, Quaternion.Euler(0f, Random.Range(0, 360f), 0f));
        //}
        ObstableSpawner.Spawn();      

        Data.player.WorldPopulation *= (100f - Data.consts.HellAttackDeathPercent) / 100f;
    }
}
