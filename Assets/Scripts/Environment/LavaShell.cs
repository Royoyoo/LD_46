using UnityEngine;

public class LavaShell : MonoBehaviour
{
	private SphereCollider collider;
	private Rigidbody rb;

	private Vector3 launchPoint, targetPoint, launchVelocity;

	private float age;

	// для колайдера
	private float lastY;
	private bool enabled = false;

	private void Awake()
	{
		collider = GetComponent<SphereCollider>();
		rb = GetComponent<Rigidbody>();
		collider.enabled = false;
		rb.useGravity = false;
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
}
