using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    float startPositionY;
    Vector3 vector = Vector3.up;

    private void Awake()
    {
        startPositionY = transform.localPosition.y;
    }

    private void Update()
    {
        if (transform.localPosition.y > startPositionY + 1f)
        {
            vector = Vector3.down;
        }
        if (transform.localPosition.y < startPositionY - 1f)
        {
            vector = Vector3.up;
        }
        //var newPosition = Marker.localPosition;
        //newPosition.y += Mathf.Sin(Time.time) * 0.1f;


        transform.localPosition += vector * Time.deltaTime * 5;
    }
}
