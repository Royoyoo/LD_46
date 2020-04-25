using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    [Range(0.1f, 10f)]
    [SerializeField] private float gizmoSize;
    [SerializeField] private bool showGizmo;

    [SerializeField] private Transform[] points;

    public Transform[] Points => points;

    public float GizmoSize => gizmoSize;

    [ContextMenu("Присвоить точки маршрута")]
    private void AssignPoints()
    {
        var allPoints = GetComponentsInChildren<Transform>();
        points = allPoints.Where(t => t != this.transform).ToArray();

        Debug.Log(points.Length);
    }

    private void OnDrawGizmos()
    {
        if (points == null || showGizmo == false)
            return;

        Gizmos.color = Color.yellow;
        foreach (var item in points)
        {
            Gizmos.DrawSphere(item.position, gizmoSize);            
        }
    }   
}
