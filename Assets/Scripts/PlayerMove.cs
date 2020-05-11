using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1;
    public float torque = 1; 

    private Collider collider;
    private Rigidbody rigidBody;

    public Rigidbody RigidBody => rigidBody;

    //public float MaxSpeed ;

    public event Action<float> OnColladedWithObstable;

    private bool confused = false;
    private float confuseTime;
       
    private float confuseProcess;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();
    }
   
    public void Confuse(float confuseTime)
    {
        this.confuseTime = confuseTime;
        confused = true;
        confuseProcess = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (confused)
        {
            confuseProcess += Time.deltaTime;
            if(confuseProcess >= confuseTime)
            {
                confuseProcess = 0;
                confused = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // вперед / назад
        var forward = Input.GetAxis("Vertical");
        if (confused)
            forward *= -1;

        var trimForward = Mathf.Clamp(forward, -0.5f, 1);
        var direction = transform.forward * trimForward;
        rigidBody.AddForce(direction * speed);

        // повороты
        float turn = Input.GetAxis("Horizontal");
        if (confused)
            turn *= -1;

        var rotation = transform.up  * turn;
        rigidBody.AddTorque(rotation * torque);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter" + collision.gameObject.name);

       // if (collision.gameObject.tag == "obstacle")
       // {
            //Debug.Log("Speed = " + speed);
            //Debug.Log("OnCollisionEnter + obstacle" + collision.gameObject.name);
           
            var speed = rigidBody.velocity.magnitude;
            OnColladedWithObstable?.Invoke(speed);            
       // }
    }
}
