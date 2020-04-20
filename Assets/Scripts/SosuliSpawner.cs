using System.Collections;
using UnityEngine;

public class SosuliSpawner : MonoBehaviour
{
    [Range(1, 60)]
    public int Timeout;

    ObstableSpawner[] spawners;

    void Start()
    {
        spawners = GetComponentsInChildren<ObstableSpawner>();

        StartCoroutine(Spawn());
    }

    
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Data.consts.StartDelay);

        while (true)
        {
            yield return new WaitForSeconds(Timeout);
            
            SpawnAll();
        }
    }

    [ContextMenu("SpawnAll")]
    private void SpawnAll()
    {
        foreach (var spawner in spawners)
        {
            spawner.Spawn2();
        }
    }
}
