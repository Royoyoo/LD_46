using UnityEngine;

public class ConfusingFog : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField] private float confuseTime;

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null)
            return;

        player.playerMove.Confuse(confuseTime);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, radiusSpherForce);
    //}
}
