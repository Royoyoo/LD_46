using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameplayUI gameplayUI;

    public int obstaclesSpawnCount = 2;
    public Vector3 LocationBounds;
    public GameObject ObstaclePrefab;

    public bool isStarted = false;

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

    public void HellAttack()
    {
        for (int i = 0; i < obstaclesSpawnCount; i++)
        {
            var pos = new Vector3(Random.Range(-LocationBounds.x, LocationBounds.x), 0f, Random.Range(-LocationBounds.z, LocationBounds.z));
            Instantiate(ObstaclePrefab, pos, Quaternion.Euler(0f, Random.Range(0, 360f), 0f));
        }

        Data.player.WorldPopulation *= (100f - Data.consts.HellAttackDeathPercent) / 100f;
    }
}
