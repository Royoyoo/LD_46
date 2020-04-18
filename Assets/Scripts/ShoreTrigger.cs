using UnityEngine;

public enum ShoreType
{
    Living,
    Aid,
    Resurrection,
}

public enum InteractionType
{
    // добавить на берег из лодки
    Add,
    // взять с берега на лодку
    Remove,    
}


[RequireComponent(typeof(BoxCollider))]
public class ShoreTrigger : MonoBehaviour
{
    public SoulsContainer souls;

    public ShoreType Type;

    public InteractionType InteractionType;

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Player")
        //{
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
          
            player.InteractionContainer = souls;
            player.InteractionType = InteractionType;

            Debug.Log("souls.SoulsCount  " + souls.SoulsCount);
            Debug.Log("OnTriggerEnter Player " + Type);
        }
        //}     
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.InteractionContainer = null;
            Debug.Log("OnTriggerExit Player " + Type);
        }
        //if (other.gameObject.tag == "Player")
        //{
        //    Debug.Log("OnTriggerExit Player " + Type);
        //}
    }
}
