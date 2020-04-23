using System.Collections;
using UnityEngine;

public class ObstableSpawner : MonoBehaviour
{
    [Header("Сталактиты")]
    public int obstaclesSpawnCount = 2;
    public Vector2 LocationBounds;
    public SpawnedObstacle[] ObstaclePrefab;
    [Range(1, 60)]
    public float TimeOfLife = 20;
    // За сколько секунд до смерти начинает тонуть
    [Range(1, 5)]
    public float SinkTime = 2;
     
    public IEnumerator Spawn()
    {
        for (int i = 0; i < obstaclesSpawnCount; i++)
        {
            var randomX = Random.Range(-LocationBounds.x, LocationBounds.x) + transform.position.x;
            var y = transform.position.y;
            var randomZ = Random.Range(-LocationBounds.y, LocationBounds.y) + transform.position.z;
            var pos = new Vector3(randomX, y, randomZ);

            var randomIndex = Random.Range(0, ObstaclePrefab.Length);
            var randomPrefab = ObstaclePrefab[randomIndex];
            var obstacle = Instantiate(randomPrefab, pos, Quaternion.Euler(0f, Random.Range(0, 360f), 0f), this.transform);

            var newScale = obstacle.transform.localScale;
            newScale.x *= Random.Range(0.9f, 1.3f);
            newScale.z *= Random.Range(0.9f, 1.3f);
            obstacle.transform.localScale = newScale;
            // уничтожаем коллайдер, чтобы объект стал проваливаться под землю/тонуть
            Destroy(obstacle.Collider, TimeOfLife - SinkTime);
            Destroy(obstacle.gameObject, TimeOfLife);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Spawn2(int additionalHeight)
    {
        for (int i = 0; i < obstaclesSpawnCount; i++)
        {
            var randomX = Random.Range(-LocationBounds.x, LocationBounds.x) + transform.position.x;
            // каждый следующий выше (имитация задержки)
            var y = transform.position.y + i*2 + additionalHeight;
            var randomZ = Random.Range(-LocationBounds.y, LocationBounds.y) + transform.position.z;
            var pos = new Vector3(randomX, y, randomZ);

            var randomIndex = Random.Range(0, ObstaclePrefab.Length);
            var randomPrefab = ObstaclePrefab[randomIndex];
            var obstacle = Instantiate(randomPrefab, pos, Quaternion.Euler(0f, Random.Range(0, 360f), 0f), this.transform);

            var newScale = obstacle.transform.localScale;
            newScale.x *= Random.Range(0.9f, 1.3f);
            newScale.z *= Random.Range(0.9f, 1.3f);
            obstacle.transform.localScale = newScale;
            // уничтожаем коллайдер, чтобы объект стал проваливаться под землю/тонуть
            Destroy(obstacle.Collider, TimeOfLife - SinkTime);
            Destroy(obstacle.gameObject, TimeOfLife);
        }
    }

    [ContextMenu("Spawn")]
    public void SpawnTest()
    {
        StartCoroutine(Spawn());        
    }

    private void OnDrawGizmos()
    {
        var center = new Vector3(transform.position.x, transform.position.y / 2, transform.position.z);
        var size = new Vector3(LocationBounds.x * 2, transform.position.y, LocationBounds.y * 2);
        Gizmos.DrawWireCube(center, size);
    }
}
