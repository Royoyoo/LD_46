using System;
using System.Collections;
using UnityEngine;

public class SoulsParticle : MonoBehaviour
{
    [Range(0.5f, 20f)]
    public float DesinationAreaRadius = 1;

    [Range(0.1f, 20f)]
    public float Deep = 1;

    [Range(0.1f, 20f)]
    public float Speed = 1;

    public Soul SoulPrefab;
    
    [ContextMenu("Test Emit")]
    public void TestEmit()
    {
        Emit(50);
    }

    internal void Emit(int fallOverboard)
    {
        var lessValue = Mathf.Log(fallOverboard) + 1;
        for (int i = 0; i < lessValue; i++)
        {
            var newSoul = Instantiate(SoulPrefab, this.transform);
          //  newSoul.transform.localPosition = UnityEngine.Random.insideUnitSphere * SpawnArea;
            newSoul.Active = true;
            newSoul.Speed = Speed;
            var randomPoint = RandomOnCircle(DesinationAreaRadius);
            var deep = transform.localPosition.y - Deep;
            var destination = new Vector3(randomPoint.x, deep, randomPoint.y);            
            //randomPoint.y = 0;
            newSoul.Destination = transform.position + destination;

           // Debug.DrawLine(newSoul.transform.position, newSoul.Destination, Color.white, 2f);

            Destroy(newSoul.gameObject, 2f);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //        TestEmit();
    //}

    private Vector2 RandomOnCircle(float radius)
    {
        var randAngle = UnityEngine.Random.Range(0, Mathf.PI * 2);
        return new Vector2(Mathf.Cos(randAngle) * radius, Mathf.Sin(randAngle) * radius );
    }

    private void OnDrawGizmos()
    {
        // Место спавна
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, SpawnArea);

        //Пункт назначения
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DesinationAreaRadius);
    }

   
}
