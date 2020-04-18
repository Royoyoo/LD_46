using UnityEngine;

public class SpawnedObstacle : MonoBehaviour
{
    BoxCollider collider;

    bool colliderChanged = false;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            if (colliderChanged)
                return;

            // сплющиваем 
            var newVector = collider.size;            
            newVector.y = collider.size.y * 0.5f;            
            collider.size = newVector;

            // и смещаем кверху чтобы притонул в воде
            var newCenter = collider.center;
            newCenter.y = collider.center.y + 0.25f;
            collider.center = newCenter;

            // но только 1 раз
            colliderChanged = true;
        }
    }
}
