using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaterForce : MonoBehaviour
{
    //максимальная глубина погружения
    [SerializeField] private float MaxDeep = 0;
    // Максимальная высота над водой
    [SerializeField] private float MaxUpper = 1;
    [SerializeField] private float PopupPower = 25;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {      
        // сила поднимающая вверх
        if (transform.localPosition.y < MaxDeep)
        {
            // Debug.Log("Pop up!");
            rigidBody.AddForce(Vector3.up * PopupPower, ForceMode.Force);
        }

        // сила притягивающая к воде
        if (transform.localPosition.y > MaxUpper)
        {
            // Debug.Log("Pop up!");
            rigidBody.AddForce(Vector3.down * PopupPower * 0.5f, ForceMode.Force);
        }      
    }
}
