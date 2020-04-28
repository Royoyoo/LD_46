using UnityEngine;

public class Whirlpool : MonoBehaviour
{   
    [Header("Сила в прямоугольнике")]
    [Range(1f, 1000f)]
    [SerializeField] float rectangleForce;

    [Header("Сила в сфере")]
    [Range(1f, 1000f)]
    [SerializeField] float sphereForce;  
    [Range(1f, 50f)]
    [SerializeField] float radiusSphereForce;

    private void OnTriggerStay(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        if (rb == null)
            return;

        var direction = (transform.position - other.transform.position);
        // Debug.Log("direction " + other.name + " " + direction);           
        // Debug.Log(other.name + " " + force);
        rb.AddForce(direction.normalized * rectangleForce, ForceMode.Force);

        // если в радиусе сферы, то добавляем силу
        var sqrDistance = direction.sqrMagnitude;
        if (sqrDistance < radiusSphereForce * radiusSphereForce)
        {
            rb.AddForce(direction.normalized * sphereForce, ForceMode.Force);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusSphereForce);

    }
}
