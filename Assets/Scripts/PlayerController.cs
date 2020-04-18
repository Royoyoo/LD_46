using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject soulDisplay;

    public int SoulsCount;

    public int BoatCapacity = 50;

    public SoulsContainer boatContainer;

    public SoulsContainer InteractionContainer { get; internal set; }
    public InteractionType InteractionType { get; internal set; }

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

            if (InteractionType == InteractionType.Remove)
            {
                if (!boatContainer.CanAdd())
                {
                    Debug.Log("Нельзя добавить на лодку");
                    return;
                }

                if (!InteractionContainer.CanRemove())
                {
                    //Debug.Log("InteractionContainer.SoulsCount = " + InteractionContainer.SoulsCount);              
                    Debug.Log("Нельзя забрать с берега " + InteractionContainer.name);
                    return;
                }

                InteractionContainer.Remove();
                boatContainer.Add();

                if (boatContainer.SoulsCount > 1)
                    soulDisplay.gameObject.SetActive(true);

                Debug.Log("+1 душа на лодке");
            }

            if (InteractionType == InteractionType.Add)
            {
                if (!boatContainer.CanRemove())
                {
                    Debug.Log("Невозможно удалить из лодки");
                    return;
                }

                if (!InteractionContainer.CanAdd())
                {
                    //Debug.Log("InteractionContainer.SoulsCount = " + InteractionContainer.SoulsCount);              
                    Debug.Log("Нельзя добавить на берег " + InteractionContainer.name);
                    return;
                }

                boatContainer.Remove();
                InteractionContainer.Add();

                if (boatContainer.SoulsCount == 0 )
                    soulDisplay.gameObject.SetActive(false);

                Debug.Log("-1 душа на лодке");
            }

        }
    }


}
