using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstableSpawner : MonoBehaviour
{
    [Header("Сталактиты")]
    public int obstaclesSpawnCount = 2;
    public Vector2 LocationBounds;
    public SpawnedObstacle ObstaclePrefab;
    [Range(1, 60)]
    public float TimeOfLife;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        for (int i = 0; i < obstaclesSpawnCount; i++)
        {
            var randomX = Random.Range(-LocationBounds.x, LocationBounds.x) + transform.position.x;
            var y = transform.position.y;
            var randomZ = Random.Range(-LocationBounds.y, LocationBounds.y) + transform.position.z;
            var pos = new Vector3(randomX, y, randomZ);

            var obstacle = Instantiate(ObstaclePrefab, pos, Quaternion.Euler(0f, Random.Range(0, 360f), 0f), this.transform);
            Destroy(obstacle.gameObject, TimeOfLife);
        }
    }

    private void OnDrawGizmos()
    {
        var center = new Vector3(transform.position.x, transform.position.y / 2, transform.position.z);
        var size = new Vector3(LocationBounds.x * 2, transform.position.y, LocationBounds.y * 2);
        Gizmos.DrawWireCube(center, size);
    }
}
