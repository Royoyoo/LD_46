using System.Collections;
using UnityEngine;

public class WhirlpoolSpawner : MonoBehaviour
{
    [Range(1, 60)]
    [SerializeField] private int Timeout;

    [Range(1, 60)]
    [SerializeField] private int TimeOfAction;

    Whirlpool[] whirlpools;

    private void Awake()
    {
        whirlpools = GetComponentsInChildren<Whirlpool>(true);
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
            SpawnAll();
            yield return new WaitForSeconds(TimeOfAction);
            HideAll();
        }
    }

    [ContextMenu("SpawnAll")]
    private void SpawnAll()
    {
        for (int i = 0; i < whirlpools.Length; i++)
        {
            whirlpools[i].gameObject.SetActive(true);
        }
    }

    [ContextMenu("HideAll")]
    private void HideAll()
    {
        for (int i = 0; i < whirlpools.Length; i++)
        {
             whirlpools[i].gameObject.SetActive(false);
        }
    }
}
