using System.Collections;
using UnityEngine;

public class SosuliSpawner : MonoBehaviour
{
    [Range(1, 60)]
    [SerializeField] private int Timeout;

    [Range(1, 10)]
    [SerializeField] private int AdditionalHeight;

    [SerializeField] private SpawnedObstacle[] ObstaclePrefabs;  

    ObstableSpawner[] spawners;

    void Start()
    {
        spawners = GetComponentsInChildren<ObstableSpawner>();
        foreach (var spawner in spawners)
        {
            // todo 
            spawner.ObstaclePrefab = ObstaclePrefabs;
        }

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
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].Spawn2(i * 3);
        }
    }
}
