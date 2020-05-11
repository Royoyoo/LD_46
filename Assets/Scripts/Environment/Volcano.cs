using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class Volcano : MonoBehaviour
{
    [SerializeField] private GameObject lavaItemPrefab;

    [Range(1, 60)]
    [SerializeField] private int timeout;

    [Header("Пузырики")]
    [SerializeField] private bool bubleEnable;
    [SerializeField] private GameObject lavaBublePrefab;
    [SerializeField] private Transform lavaLevelSphere;
    [Range(0.1f, 3f)]
    [SerializeField] private float bubleDelay = 1;
    [Range(0.01f, 1f)]
    [SerializeField] private float bubleSize = 0.1f;
    [Range(0.01f, 1f)]
    [SerializeField] private float bubleSpeed = 0.1f;
    private float bubleProgress;

    [Space]
    [Header("Лавовые снаряды")]
   // [SerializeField] private bool enable;
    [SerializeField] private int shellCount;
    [SerializeField] private LavaShell lavaShellPrefab;
    [SerializeField] private Transform lavaShellsContentParent;
    [Range(1.1f, 5f)]
    [SerializeField] private float launchSpeedKoef = 1.5f;
    [Range(1f, 50f)]
    [SerializeField] private float targetArea;
    [SerializeField] private Transform targetPoint;
    [Range(0.1f, 5f)]
    [SerializeField] private float launchTimeout = 0.1f;

    [SerializeField] ObstableSpawner[] obstableAreas;

  


    private List<GameObject> bubles;
    private List<GameObject> destroyBubles;

    // Use this for initialization
    void Start()
    {
        bubles = new List<GameObject>(10);
        destroyBubles = new List<GameObject>(10);

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(Data.consts.StartDelay);

        while (true)
        {
            yield return new WaitForSeconds(timeout);

            var points = obstableAreas.Select(t => t.GetRandomXY()).ToArray();

            yield return LaunchAll(points);
        }
    }

    private void Update()
    {
        if (bubleEnable)
        {
            bubleProgress += Time.deltaTime;
            if (bubleProgress > bubleDelay)
            {
                var randomPosition = lavaLevelSphere.position + Random.insideUnitSphere;
                var buble = Instantiate(lavaBublePrefab, randomPosition, Quaternion.identity, lavaLevelSphere);
                buble.transform.localScale = Vector3.one * bubleSize;
                bubles.Add(buble);

                bubleProgress -= bubleDelay;
            }

            destroyBubles.Clear();
            foreach (var buble in bubles)
            {
                buble.transform.Translate(Vector3.up * bubleSpeed * Time.deltaTime);

                // проверяем выход пузыря из лавы
                var distance = buble.transform.position - lavaLevelSphere.position;
                if (distance.sqrMagnitude > lavaLevelSphere.localScale.x * 0.5f * lavaLevelSphere.localScale.x * 0.5f)
                    destroyBubles.Add(buble);
            }

            foreach (var buble in destroyBubles)
            {
                bubles.Remove(buble);
                Destroy(buble);
            }
        }
         
        
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    var points = obstableAreas.Select(t => t.GetRandomXY()).ToArray();         
        //    StartCoroutine(LaunchAll(points));
        //}
        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            //  explode();
            StopAllCoroutines();
        }*/
    }

    private IEnumerator LaunchAll(Vector3 targetPoint)
    {        
        for (int i = 0; i < shellCount; i++)
        {
            var randomPoint = targetPoint + Random.insideUnitSphere * targetArea;
            randomPoint.y = 0;
            Launch(randomPoint);

            yield return new WaitForSeconds(launchTimeout);
        }
    }

    private IEnumerator LaunchAll(Vector3[] targetPoints)
    {
        foreach (var targetPoint in targetPoints)
        {
            for (int i = 0; i < shellCount; i++)
            {
                var randomPoint = targetPoint + Random.insideUnitSphere * targetArea;
                randomPoint.y = 0;
                Launch(randomPoint);

                yield return new WaitForSeconds(launchTimeout);
            }         
        }      
    }

    public void Launch(Vector3 targetPoint)
    {
        var launchPoint = lavaLevelSphere.position/* + Random.insideUnitSphere*/;
        // var targetPoint = new Vector3(launchPoint.x + 3f, 0f, launchPoint.z);
        // Debug.Log(launchPoint + " " +targetPoint.position);

        Vector2 dir;
        dir.x = targetPoint.x - launchPoint.x;
        dir.y = targetPoint.z - launchPoint.z;
        float x = dir.magnitude + 0.25001f;
        float y = -launchPoint.y;
        dir /= x;

        float g = 9.81f;
        var minLaunchSpeed = Mathf.Sqrt(g * (y + Mathf.Sqrt(x * x + y * y)));
        Debug.Log(minLaunchSpeed);
        float s = minLaunchSpeed * launchSpeedKoef;
        float s2 = s * s;

        float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
        Assert.IsTrue(r >= 0);
        float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
        float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
        float sinTheta = cosTheta * tanTheta;

        //Vector3 prev = launchPoint, next;
        //for (int i = 1; i <= 10; i++)
        //{
        //    float t = i / 10f;
        //    float dx = s * cosTheta * t;
        //    float dy = s * sinTheta * t - 0.5f * g * t * t;
        //    next = launchPoint + new Vector3(dir.x * dx, dy, dir.y * dx);
        //    Debug.DrawLine(prev, next, Color.blue, 10f);
        //    prev = next;
        //}

      //  Debug.DrawLine(targetPoint, targetPoint + Vector3.up * 10, Color.blue, 10f);
        // заряд лавы
        var lavaShell = Instantiate(lavaShellPrefab, lavaShellsContentParent);
        //lavaShell.transform.position += lavaShellsContentParent.transform.position;
        var launchVelocity = new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y);
        lavaShell.Init(launchPoint, targetPoint, launchVelocity);        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(targetPoint.position, targetArea);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(lavaLevelSphere.position, 1f);
    }
}
