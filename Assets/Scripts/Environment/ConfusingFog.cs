using UnityEngine;

public class ConfusingFog : MonoBehaviour
{     
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        player.playerMove.Confused = true;
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        player.playerMove.Confused = false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, radiusSpherForce);
    //}
}
