using UnityEngine;

public class UpgradeTrigger : MonoBehaviour
{
    public UpgradeUI upgradeUI;

    private void OnTriggerEnter(Collider other)
    {
        //var player = other.GetComponent<PlayerController>();
        if (other.gameObject.tag != "Player")
            return;

        upgradeUI.Show(true);
        //player.upgradeUI.Show(true);  
       // Debug.Log("UpgradeTrigger OnTriggerEnter");
    }

    private void OnTriggerExit(Collider other)
    {
       // var player = other.GetComponent<PlayerController>();
        if (other.gameObject.tag != "Player")
            return;

        upgradeUI.Show(false);
        //player.upgradeUI.Show(false);
        //Debug.Log("UpgradeTrigger OnTriggerExit");
    }  
}
