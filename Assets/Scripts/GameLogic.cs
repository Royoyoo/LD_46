using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    GameplayUI gameplayUI;
      
    public List<ObstableSpawner> ObstableSpawners;

    public bool isStarted = false;

    public List<Quest> allQuests = new List<Quest>();
    public Quest activeQuest = null;
    public bool questSet = false;
    public float questStartTime;

    public SoulsContainer DeadShoreContainer;
      
    [Header("Бедствия")]
    public List<DisasterData> Disasters;

    public Animation openingAnim;
    public GameObject helpScreen;
    int openingStep = 0;

    public SoulsContainer LiveShoreContainer;
    public SoulsContainer AidShoreContainer;

    void Start()
    {
        isStarted = false;
        Time.timeScale = 1f;
        gameplayUI = FindObjectOfType<GameplayUI>();
        StartCoroutine(GameProgress());
    }

    public void StartGame()
    {
        isStarted = true;
    }

    public void NextOpeningStep()
    {
        openingStep++;
        openingAnim.Play($"OP_{openingStep}");
    }

    IEnumerator GameProgress()
    {
        gameplayUI.gameObject.SetActive(false);
        openingAnim.gameObject.SetActive(true);

        while (openingStep < 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                NextOpeningStep();

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        openingAnim.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(true);
        helpScreen.SetActive(true);

        while (!isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartGame();
            yield return null;
        }

        helpScreen.SetActive(false);

        StartCoroutine(HellDelivery());
        StartCoroutine(HellProcess());
        StartCoroutine(QuestProcess());
        StartCoroutine(WinLoseProcess());       
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
    int deliveryTimes = 0;
    IEnumerator HellDelivery()
    {
        yield return new WaitForSeconds(Data.consts.HellDeliveryTimeout);

        while (true)
        {
            var bonus = Data.consts.HellDeliveryCount * deliveryTimes / 2f;
            //Debug.Log("Bonus " + bonus);
            var deliveryCount = Data.consts.HellDeliveryCount + (bonus);
            if (deliveryCount > Data.player.DeadShorePopulation)
            {
                deliveryCount = (int) Data.player.DeadShorePopulation;
            }

            // перенос душ с берега к вратам ада
            LiveShoreContainer.SoulsCount -= deliveryCount;
            AidShoreContainer.SoulsCount += deliveryCount;
            deliveryTimes++;

            //Debug.Log("HellDelivery + " + deliveryCount);

            yield return new WaitForSeconds(Data.consts.HellDeliveryTimeout);
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
                questSet = true;
                //Debug.Log(activeQuest);
            }

            if (Time.time > questStartTime + Data.consts.QuestActiveTime && !Data.player.gotQuest)
            {
                //questStartTime = Time.time;
                activeQuest = null;
                questSet = false;
            }

            yield return null;
        }
    }

    IEnumerator WinLoseProcess()
    {
        while (true)
        {
            if (Data.player.WorldPopulation < 1)
            {
                gameplayUI.ShowLoseUI();
                yield return null;
                continue;
            }

            if (Data.player.WorldPopulation >= Data.consts.TargetPopulation)
            {
                gameplayUI.ShowWinUI();
                yield return null;
                continue;
            }

            yield return null;
        }
    }

    public void CollectSouls(ISoulsSource source)
    {
        if (source.SoulsCount < 1)
            return;

        if(Data.player.CurrentBoatCapacity < Data.player.MaxBoatCapacity)
        {
            var collectedAmount = source.CollectSpeed * Time.deltaTime;
            source.SoulsCount = Mathf.Max(source.SoulsCount - collectedAmount, 0f);
            Data.player.CurrentBoatCapacity = Mathf.Min(Data.player.CurrentBoatCapacity + collectedAmount, Data.player.MaxBoatCapacity);
        }
    }

    public float RessurectSouls()
    {
        var amount = 0f;
        if (Data.player.CurrentBoatCapacity > 0)
        {
            amount = Data.consts.RessurectRate * Time.deltaTime;
            Data.player.WorldPopulation += amount;
            Data.player.CurrentBoatCapacity = Mathf.Max(Data.player.CurrentBoatCapacity - amount, 0f);
            Data.player.Coins += Data.consts.CoinsRate * amount;
        }

        if(Data.player.gotQuest)
        {
            FinishQuest();           
        }

        return amount;
    }

    public float RessurectSouls(ISoulsSource source)
    {
        var amount = 0f;
        if (Data.player.CurrentBoatCapacity > 0)
        {
            // TODO CollectSpeed не зависит от контейнера
            amount = /*Data.consts.RessurectRate*/source.CollectSpeed * Time.deltaTime;
            Data.player.WorldPopulation += amount;
            Data.player.CurrentBoatCapacity = Mathf.Max(Data.player.CurrentBoatCapacity - amount, 0f);
            Data.player.Coins += Data.consts.CoinsRate * amount;
        }

        if (Data.player.gotQuest)
        {
            FinishQuest();
        }

        return amount;
    }

    public void ShowQuestMessage()
    {
        if(questSet)
            gameplayUI.ShowQuestMessage(activeQuest.startMessage);
    }

    public void AcceptQuest()
    {
        if (!questSet)
            return;

        Data.player.gotQuest = true;
        questSet = false;
        Data.player.currentQuest = activeQuest;

        gameplayUI.HideQuestMessage();
    }

    public void FinishQuest()
    {
        gameplayUI.ShowQuestMessage(activeQuest.finishMessage);
        Data.player.WorldPopulation += Data.player.currentQuest.Effect;

        Data.player.currentQuest = null;
        Data.player.gotQuest = false;

        StartCoroutine(HideQuestUI());
    }

    IEnumerator HideQuestUI()
    {
        yield return new WaitForSeconds(3f);
        gameplayUI.HideQuestMessage();
    }

    [ContextMenu("HellAttack")]
    public void HellAttack()
    {
        gameplayUI.ShowDialog(DialogSide.Right, DialogPortrait.Tanatos, "OH, YOU PISSED ME OFF! TAKE THIS!");

        // бедствие
        var randomIndex = Random.Range(0, Disasters.Count);
        var randomDisaster = Disasters[randomIndex];

        var killedValue =(int) (Data.player.WorldPopulation * randomDisaster.MinusSoulsPercent + randomDisaster.MinusSoulsCount);
        if (killedValue > Data.player.WorldPopulation)
            killedValue = (int)Data.player.WorldPopulation;
            
        Data.player.WorldPopulation -= killedValue;//*= (100f - Data.consts.HellAttackDeathPercent) / 100f;

        // убитые люди превращаются в души на берегу
        //Debug.Log("Data.player.DeadShorePopulation " + Data.player.DeadShorePopulation);
        //Debug.Log("killedValue " + killedValue);
        DeadShoreContainer.SoulsCount += killedValue;
        //Data.player.DeadShorePopulation += killedValue;
       // Debug.Log("Data.player.DeadShorePopulation " + Data.player.DeadShorePopulation);

        gameplayUI.DisasterUi.UpdateUI(randomDisaster, killedValue);

        // сталактиты
        //foreach (var spawner in ObstableSpawners)
        //{
        //    StartCoroutine(spawner.Spawn());
        //}                  
    }
}
