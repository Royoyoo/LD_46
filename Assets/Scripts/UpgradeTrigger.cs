using UnityEngine;

public class UpgradeTrigger : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null)
            return;
               
        player.upgradeUI.Show(true);  
        Debug.Log("UpgradeTrigger OnTriggerEnter");
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        player.upgradeUI.Show(false);
        Debug.Log("UpgradeTrigger OnTriggerExit");
    }  
}
