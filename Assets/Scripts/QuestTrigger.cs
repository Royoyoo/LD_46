using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    SphereCollider collider;

    //public Quest Quest;

    void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        player.CanTakeQuest = true;
        FindObjectOfType<GameLogic>().ShowQuestMessage();

        //if (Data.player.gotQuest == false)
        //{
        //    player.CanTakeQuest = true;
        //    Data.player.currentQuest = Quest;
        //}               

        Debug.Log("QuestTrigger OnTriggerEnter");
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        player.CanTakeQuest = false;
        FindObjectOfType<GameplayUI>().HideQuestMessage();

        //if (Data.player.gotQuest == false)
        //{ 
        //    Data.player.currentQuest = null; 
        //}

        Debug.Log("QuestTrigger OnTriggerExit");
    }   
}
