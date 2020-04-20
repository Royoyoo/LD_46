using System;
using System.Collections;
using UnityEngine;

public class SoulsDissolver : MonoBehaviour
{
    [Range(0.5f, 10f)]
    public float DesinationAreaRadius = 1;

    [Range(0.5f, 10f)]
    public float SpawnArea = 1;

    public Transform soulDestinationMarker;

    public Soul SoulPrefab;

    // В секундах
    public float SpawnDelay = 0.1f;  
    //public SoulsContainer souls;
    //public int NeedSpawn;

    public float NeedDissolve=0;

    private void Start()
    {

        StartCoroutine(Dissolve());
    }

    public IEnumerator Dissolve()
    {
        yield return new WaitForSeconds(Data.consts.StartDelay);

        while (true)
        {           

            if (NeedDissolve <= 0)
                yield return null;
            else
            {
                NeedDissolve--;
                Spawn1();
                yield return new WaitForSeconds(SpawnDelay);
            }           
        }

    }

    public void Spawn1()
    {        
            var newSoul = Instantiate(SoulPrefab, this.transform);
            newSoul.transform.localPosition += UnityEngine.Random.insideUnitSphere;
            newSoul.Active = true;
            newSoul.Speed = 3f;
            var randomPoint = UnityEngine.Random.insideUnitSphere;
            randomPoint.y = 0;
            newSoul.Destination = soulDestinationMarker.position + randomPoint * DesinationAreaRadius;

            Destroy(newSoul.gameObject, 5f);            
        
    }

    public void StartSpawn(int count)
    {
        if (count <= 0)
            return;

        StartCoroutine(Spawn(count));
    }

    private IEnumerator Spawn(int count)
    {      
        for (int i = 0; i < count; i++)
        {
            var newSoul = Instantiate(SoulPrefab, this.transform);
            newSoul.transform.localPosition += UnityEngine.Random.insideUnitSphere;
            newSoul.Active = true;
            newSoul.Speed = 3f;
            var randomPoint = UnityEngine.Random.insideUnitSphere * SpawnArea;
            randomPoint.y = 0;
            newSoul.Destination = soulDestinationMarker.position + randomPoint * DesinationAreaRadius;

            Destroy(newSoul.gameObject, 5f);

           // NeedSpawn--;
            yield return new WaitForSeconds(SpawnDelay);
        }       
    }

    [ContextMenu("Test spawn")]
    public void TestStartSpawn()
    {
        StartCoroutine(Spawn(50));
    }

    private void OnDrawGizmos()
    {
        // Место спавна
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SpawnArea);

        //Пункт назначения
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(soulDestinationMarker.position, DesinationAreaRadius);
    }
}
