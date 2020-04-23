using UnityEngine;

public enum InteractionType
{
    // добавить на берег из лодки
    Ressurect,
    // взять с берега на лодку
    Collect,    
}

[RequireComponent(typeof(Collider))]
public class ShoreTrigger : MonoBehaviour
{
    public SoulsContainer souls;

    //public InteractionType InteractionType;
      
    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Player")
        //{        
        var player = other.GetComponent<PlayerController>();
        if (player == null)        
            return;  

        player.InteractionContainer = souls;
        //player.InteractionType = InteractionType;

        EventBroker.Call_VisitShoreTrigger(souls);

        Debug.Log("souls.SoulsCount  " + souls.soulsCount);
        //}     
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null)        
            return;
            //Debug.Log("OnTriggerExit Player " + Type);
        
        player.InteractionContainer = null;
        //if (other.gameObject.tag == "Player")
        //{
        //    Debug.Log("OnTriggerExit Player " + Type);
        //}
    }
}
