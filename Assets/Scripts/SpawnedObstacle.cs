using UnityEngine;

public class SpawnedObstacle : MonoBehaviour
{ 
    public BoxCollider Collider; 

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
    }   
}
