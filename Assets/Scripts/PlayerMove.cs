using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float speed = 1;
    public float torque = 1; 

    private Collider collider;
    private Rigidbody rigidBody;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();
    }
   
    // Update is called once per frame
    void Update()
    {
        //Vector2 playerInput;
        //playerInput.x = Input.GetAxis("Horizontal");
        //playerInput.y = Input.GetAxis("Vertical");
        //playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        //Vector3 displacement = new Vector3(playerInput.x, 0, playerInput.y);
        //transform.localPosition += displacement;

        // вперед / назад
        /*var forward = Input.GetAxis("Vertical");
        var direction = new Vector3(forward, 0f, 0f); // Vector3.right
        var force = speed * direction * Time.deltaTime;
        Debug.Log("force = " + force );
        rigidBody.AddRelativeForce(force, ForceMode.Force);*/

        // повороты
        //var horizontal = Input.GetAxis("Horizontal");
        //var rotation = new Vector3(forward, 0f, 0f); // Vector3.right
        //rigidBody.AddTorque(1f, ForceMode.Force);

    }

    private void FixedUpdate()
    {
        // вперед / назад
        var forward = Input.GetAxis("Vertical");
        var trimForward = Mathf.Clamp(forward, -0.5f, 1);
        var direction = transform.forward * trimForward;
        rigidBody.AddForce(direction * speed);

        // повороты
        float turn = Input.GetAxis("Horizontal");
        var rotation = transform.up  * turn;
        rigidBody.AddTorque(rotation * torque);
    }
}
