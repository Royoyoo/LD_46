using System;
using UnityEngine;

public class SoulsSpawner : MonoBehaviour
{
    [Range(0.5f, 5f)]
    public float DesinationAreaRadius = 1;
       
    public int MaxVisibleSoulsCount = 50;

    public Transform soulDestinationMarker;

    //public event Action<int> OnSoulSpawn;

    public Soul SoulPrefab;

    // В секундах
    public float SpawnDelay = 1f;

    float lastSpawnCount;

    public SoulsContainer souls;

    public bool SpawnFromContainer;

    void Start()
    {
        lastSpawnCount = souls.SoulsCount;
    }

    void Update()
    {
        souls.SoulsCount += Data.consts.SoulsFromPopulationRate * Data.player.WorldPopulation * Time.deltaTime;
        CheckSpawn();
    }

    void CheckSpawn()
    {
        if (souls.SoulsCount - lastSpawnCount > 1f)
        {
            lastSpawnCount = lastSpawnCount + 1f;
            Spawn();
            CheckSpawn();
        }
        else if(souls.SoulsCount - lastSpawnCount < -1f)
        {
            lastSpawnCount = lastSpawnCount - 1f;
            DestroySoul();
            CheckSpawn();
        }
    }

    private void Spawn()
    {
        //Debug.Log("Spawn");

        // TODO: добавлять в контейнер, когда они придут к точке назначения
      

        if (souls.NeedVisible)
        {
            var newSoul = Instantiate(SoulPrefab, this.transform);
            newSoul.Active = true;
            var randomPoint = UnityEngine.Random.insideUnitSphere;
            randomPoint.y = 0;

            newSoul.Destination = soulDestinationMarker.position + randomPoint * DesinationAreaRadius;
        }      

        //OnSoulSpawn?.Invoke(souls.SoulsCount);        
    }

    void DestroySoul()
    {
        //Debug.LogWarning("DestroySoul Object - Implement this!");
    }

    private void OnDrawGizmos()
    {
        // Место спавна
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);

        //Пункт назначения
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(soulDestinationMarker.position, DesinationAreaRadius);
    }
}
