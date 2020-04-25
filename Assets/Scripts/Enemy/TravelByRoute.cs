using System.Collections;
using UnityEngine;

public class TravelByRoute : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    [SerializeField] private float torque = 10;
    [Range(1f, 20f)]
    [SerializeField] private float appropriateDistance;

    [SerializeField] private Route route;
  
    private int destinationIndex = -1;
    private Vector3 destination;

    private void Start()
    {       
        if (route.Points == null || route.Points.Length == 0)
            return;

        destinationIndex = 0;
        destination = route.Points[0].position;       
    }

    private void Update()
    {
        if (destinationIndex == -1)
            return;

        // проверяем расстояние до точки, игнорируя высоту
        var destinationWithoutY = new Vector3(destination.x, transform.localPosition.y, destination.z);
        var heading = destinationWithoutY - transform.position; 
        var sqrDistance = heading.sqrMagnitude;
        if (sqrDistance < appropriateDistance * appropriateDistance)
        {
            // меняем пункт назначения
            destinationIndex++;
            if(destinationIndex > route.Points.Length-1)
            {
                destinationIndex = 0;
            }
            destination = route.Points[destinationIndex].position;
        }

        // движение
        var direction = Vector3.forward;        
        // Приближение к высоте точки назначения 
        if (!Mathf.Approximately(transform.position.y , destination.y))
        {
            if (transform.position.y < destination.y)
            {
                direction += Vector3.up;
                //transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            if (transform.position.y > destination.y)
            {
                direction += Vector3.down;
                //transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
        transform.Translate(direction * Time.deltaTime * speed);

        // поворот
        var fromRotation = transform.rotation;
        // повороты только лево/право             
        var toRotation = Quaternion.LookRotation(destinationWithoutY - transform.position);     
      
        transform.rotation = Quaternion.Slerp(fromRotation, toRotation, torque * Time.deltaTime);        
    }
       

    private void OnDrawGizmos()
    {
        if (destinationIndex == -1)
            return;

        Gizmos.color = Color.red;
       // Gizmos.DrawSphere(destination, route.GizmoSize * 1.5f);

        Gizmos.DrawWireSphere(destination, appropriateDistance);
    }
}
