using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [Range(0.1f, 2f)]
    public float Speed = 1;
    [Range(0.1f, 2f)]
    public float MovingTime = 1;
    
   
    private float startTime;
    private float journeyLength;

    public Vector3 startPosition;
    public Vector3 Destination;

    void Start()
    {
        startPosition = transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, Destination);
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * Speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPosition, Destination, fracJourney);

        //transform.localPosition = Vector3.Lerp(startPosition, Destination, Time.deltaTime / MovingTime);

        // поворот к центру 
        //transform.localRotation = 
    }
}
