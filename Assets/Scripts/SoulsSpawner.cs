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

    private float currentProgress = 0;

    public SoulsContainer souls;

    public bool SpawnFromContainer;

    private void Update()
    {
        if (SpawnFromContainer)
        {            
            if (souls.Remove())
            {
                Spawn();               
            }
        }
        else
        {
            currentProgress += Time.deltaTime;
            if (currentProgress > SpawnDelay)
            {
                currentProgress -= SpawnDelay;
                var added = souls.Add();
                if (added)
                {
                    Spawn();
                }
                //OnSoulSpawn?.Invoke(souls.SoulsCount);
            }
        }       
    }

    private void Spawn()
    {
        Debug.Log("Spawn");

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
