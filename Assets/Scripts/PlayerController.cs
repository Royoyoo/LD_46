using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject soulDisplay;

    public int SoulsCount;

    public int BoatCapacity = 50;

    public SoulsContainer boatContainer;

    public SoulsContainer InteractionContainer { get; internal set; }

    public event Action<int> OnSoulsCountChange;

    private void Awake()
    {
        boatContainer = GetComponent<SoulsContainer>();       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Input.GetKeyDown(KeyCode.Space)");
            if (InteractionContainer == null)
            {
                Debug.Log("InteractionContainer = null!");
                return;
            }

            if (!boatContainer.CanAdd())
            {
                Debug.Log("Can't Add");
                return;
            }

            if (!InteractionContainer.CanRemove())
            {
                //Debug.Log("InteractionContainer.SoulsCount = " + InteractionContainer.SoulsCount);              
                Debug.Log("InteractionContainer can't Remove");
                return;
            }
          
            InteractionContainer.Remove();
            boatContainer.Add();

            if (boatContainer.SoulsCount > 1)
                soulDisplay.gameObject.SetActive(true);

            Debug.Log("Boat +1 soul");
        }
    }


}
