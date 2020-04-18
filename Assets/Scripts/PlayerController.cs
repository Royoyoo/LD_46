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

    // int - количество на лодке
    public event Action<int> OnSoulsCountChange;

    private PlayerMove playerMove;

    private void Awake()
    {
        boatContainer = GetComponent<SoulsContainer>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void OnEnable()
    {
        boatContainer.OnSoulsCountChange += OnSoulsCountChange;
        playerMove.OnColladedWithObstable += PlayerMove_OnColladedWithObstable;
    }    

    private void OnDisable()
    {
        boatContainer.OnSoulsCountChange -= OnSoulsCountChange;
        playerMove.OnColladedWithObstable -= PlayerMove_OnColladedWithObstable;
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
                TryRemoveFromShore();               
            }

            if (InteractionType == InteractionType.Add)
            {
                TryAddToShore();
            }
        }
    }    

    private void TryRemoveFromShore()
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

        ShowHideSoulDisplay();

        Debug.Log("+1 душа на лодке");

        OnSoulsCountChange?.Invoke(boatContainer.SoulsCount);
    }

    private void TryAddToShore()
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

        ShowHideSoulDisplay();

        Debug.Log("-1 душа на лодке");

        OnSoulsCountChange?.Invoke(boatContainer.SoulsCount);
    }

    private void ShowHideSoulDisplay()
    {
        if (boatContainer.SoulsCount != 0)
            soulDisplay.gameObject.SetActive(true);
        else
            soulDisplay.gameObject.SetActive(false);
    }

    private void PlayerMove_OnColladedWithObstable(float speed)
    {
       // Debug.Log("speed = " + speed);

        var speedRatio = speed / playerMove.speed;
       // Debug.Log("forceOfStrike = " + speedRatio);

        var fallOverboard = (int) (boatContainer.SoulsCount * speedRatio);
        //Debug.Log("fallOverboard " + fallOverboard);

        if (fallOverboard != 0) 
        {
            boatContainer.Remove(fallOverboard);
            
            ShowHideSoulDisplay();

            OnSoulsCountChange?.Invoke(boatContainer.SoulsCount);
        }
    }
}
