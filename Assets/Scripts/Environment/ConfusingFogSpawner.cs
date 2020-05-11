using System.Collections;
using UnityEngine;

public class ConfusingFogSpawner : MonoBehaviour
{
    [Range(1, 60)]
    [SerializeField] private int Timeout;

    [Range(1, 60)]
    [SerializeField] private int TimeOfAction;

    ConfusingFog[] confusingFogs;

    private void Awake()
    {
        confusingFogs = GetComponentsInChildren<ConfusingFog>(true);
    }

    void Start()
    {
        StartCoroutine(GameLoop());
    }


    private IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(Data.consts.StartDelay);

        while (true)
        {
            yield return new WaitForSeconds(Timeout);
            SpawnRandom();
            yield return new WaitForSeconds(TimeOfAction);
            HideAll();
        }
    }

    [ContextMenu("SpawnAll")]
    private void SpawnRandom()
    {
        var index = Random.Range(0, confusingFogs.Length);
        var randomFog = confusingFogs[index];
        randomFog.gameObject.SetActive(true);
    }

    [ContextMenu("HideAll")]
    private void HideAll()
    {
        for (int i = 0; i < confusingFogs.Length; i++)
        {
            confusingFogs[i].gameObject.SetActive(false);
        }
    }
}
