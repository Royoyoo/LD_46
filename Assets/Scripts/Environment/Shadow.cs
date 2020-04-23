using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Transform parentObject;  

    [SerializeField] private float levelUnderWater = 1f;
    [SerializeField] private Vector3 maxShadowScale;
       
    float startParentObjectY;
    Quaternion startRotation;

    private void Start()
    {       
        startParentObjectY = parentObject.position.y;
        transform.localScale = Vector3.one;

        startRotation = transform.rotation;
    }
      
    private void Update()
    {
        var newPosition = parentObject.position;
        newPosition.y = levelUnderWater;
        transform.position = newPosition;

        transform.rotation = startRotation;

        var fullDistance = startParentObjectY - levelUnderWater;
        var restDistance = (startParentObjectY - parentObject.position.y);
        var progress = restDistance / fullDistance;     
        //Debug.Log(progress);
        transform.localScale = Vector3.Lerp(Vector3.one, maxShadowScale, progress);       

        if (parentObject.position.y < transform.position.y)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
