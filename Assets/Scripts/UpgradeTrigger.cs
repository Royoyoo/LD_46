using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(SphereCollider))]
public class UpgradeTrigger : MonoBehaviour
{
    SphereCollider collider;
    void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(collider.center, collider.radius);
    //}
}
