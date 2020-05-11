using UnityEngine;

public class LavaShell : MonoBehaviour
{
    private SphereCollider collider;
    private Rigidbody rb;

    [Range(0.1f, 10f)]
    [SerializeField] private float explosionRadius;
    [Range(1f, 50f)]
    [SerializeField] private float explosionForse;

    private Vector3 launchPoint, targetPoint, launchVelocity;

    private float age;

    // для колайдера
    private float lastY;
    private bool enabled = false;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.radius = explosionRadius;

        rb = GetComponent<Rigidbody>();
        //collider.enabled = false;
        //rb.useGravity = false;
    }

    public void Init(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity)
    {
        this.launchPoint = launchPoint;
        this.targetPoint = targetPoint;
        this.launchVelocity = launchVelocity;        
    }

    public void Update()
    {
        //if(collider.enabled == false)
        //{

        age += Time.deltaTime;
        Vector3 p = launchPoint + launchVelocity * age;
        p.y -= 0.5f * 9.81f * age * age;
        transform.position = p;

        //}		
        /*
		// при падении включаем 
		if (lastY > transform.position.y && collider.enabled == false)
		{			
			Debug.Log("enabled");
			collider.enabled = true;
			rb.useGravity = true;
		}
		
		lastY = transform.position.y;
	*/
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (transform.position.y > 1f)
            return;

        var player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        var direction = (other.transform.position - transform.position);
             
        player.playerMove.RigidBody.AddForce(direction.normalized * explosionForse, ForceMode.Force);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x * explosionRadius );
    }
}
