using System;
using UnityEngine;

public class SoulsSpawner : MonoBehaviour
{
    [Range(0.1f, 3f)]
    public float AreaRadius = 1;

    public int SoulsCount = 0;

    public int MaxVisibleSoulsCount = 100;

    public Transform soulDestinationMarker;

    public event Action<int> OnSoulSpawn;

    public Soul SoulPrefab;

    // В секундах
    public float SpawnDelay = 1f;

    private float currentProgress = 0;


    private void Update()
    {
        currentProgress += Time.deltaTime;
        if(currentProgress > SpawnDelay)
        {
            currentProgress -= SpawnDelay;
            Spawn();
        }
    }

    private void Spawn()
    {
        Debug.Log("Spawn");
        SoulsCount++;

        if(SoulsCount < MaxVisibleSoulsCount)
        {
            var newSoul = Instantiate(SoulPrefab, this.transform);
            var randomPoint = UnityEngine.Random.insideUnitSphere;
            randomPoint.y = 0;

            newSoul.Destination = soulDestinationMarker.position + randomPoint * AreaRadius;
        }

        OnSoulSpawn?.Invoke(SoulsCount);        
    }

    private void OnDrawGizmos()
    {
        // Место спавна
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);

        //Пункт назначения
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(soulDestinationMarker.position, AreaRadius);
    }
}
