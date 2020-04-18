using UnityEngine;

public enum InteractionType
{
    // добавить на берег из лодки
    Ressurect,
    // взять с берега на лодку
    Collect,    
}

[RequireComponent(typeof(BoxCollider))]
public class ShoreTrigger : MonoBehaviour
{
    public SoulsContainer souls;

    //public InteractionType InteractionType;

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Player")
        //{
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
          
            player.InteractionContainer = souls;
            //player.InteractionType = InteractionType;

            Debug.Log("souls.SoulsCount  " + souls.soulsCount);
            //Debug.Log("OnTriggerEnter Player " + Type);
        }
        //}     
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.InteractionContainer = null;
            //Debug.Log("OnTriggerExit Player " + Type);
        }
        //if (other.gameObject.tag == "Player")
        //{
        //    Debug.Log("OnTriggerExit Player " + Type);
        //}
    }
}
