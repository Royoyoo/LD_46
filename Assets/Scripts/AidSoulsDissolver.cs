using System;
using System.Collections;
using UnityEngine;

public class AidSoulsDissolver : MonoBehaviour
{
    [Range(0.5f, 10f)]
    public float DesinationAreaRadius = 1;

    [Range(0.5f, 10f)]
    public float SpawnArea = 1;

    public Transform soulDestinationMarker;

    public Soul SoulPrefab;

    private void Start()
    {     
        StartCoroutine(Dissolve());
    }

    public IEnumerator Dissolve()
    {
        yield return new WaitForSeconds(Data.consts.StartDelay - 1f);

        while (true)
        {
           var needDissolve = Data.player.HellDoorPopulation;

            if (needDissolve <= 0)
                yield return null;
            else
            {
                needDissolve--;
                Spawn1();
                yield return new WaitForSeconds(1 / Data.consts.GoToHellRate );
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
