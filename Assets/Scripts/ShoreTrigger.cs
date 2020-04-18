using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShoreType
{
    Living,
    Aid,
    Resurrection,
}

[RequireComponent(typeof(BoxCollider))]
public class ShoreTrigger : MonoBehaviour
{
    public ShoreType Type;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("OnTriggerEnter Player " + Type);
        }     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("OnTriggerExit Player " + Type);
        }
    }
}
