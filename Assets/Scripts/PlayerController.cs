using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject soulDisplay;

    public int SoulsCount;

    public int BoatCapacity = 50;

    public SoulsContainer boatContainer;

    public SoulsContainer InteractionContainer { get; internal set; }
    //public InteractionType InteractionType { get; internal set; }

    // int - количество на лодке
    public event Action<float> OnSoulsCountChange;

    private PlayerMove playerMove;

    GameLogic gameLogic;

    private void Awake()
    {
        boatContainer = GetComponent<SoulsContainer>();
        playerMove = GetComponent<PlayerMove>();
        gameLogic = FindObjectOfType<GameLogic>();
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
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Input.GetKeyDown(KeyCode.Space)");
            if (InteractionContainer == null)
            {
                Debug.Log("InteractionContainer = null!");
                return;
            }

            switch (InteractionContainer.Type)
            {
                case ContainerType.Living:
                    gameLogic.CollectSouls(InteractionContainer);
                    break;
                case ContainerType.Aid:
                    gameLogic.CollectSouls(InteractionContainer);
                    break;
                case ContainerType.Resurrection:
                    gameLogic.RessurectSouls();
                    break;
                default:
                    break;
            }
            
            ShowHideSoulDisplay();

            //if (InteractionType == InteractionType.Remove)
            //{
            //    TryRemoveFromShore();
            //}

            //if (InteractionType == InteractionType.Add)
            //{
            //    TryAddToShore();
            //}
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

        OnSoulsCountChange?.Invoke(boatContainer.soulsCount);
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

        OnSoulsCountChange?.Invoke(boatContainer.soulsCount);
    }

    private void ShowHideSoulDisplay()
    {
        soulDisplay.gameObject.SetActive(Data.player.CurrentBoatCapacity > 0.5f);

        //if (boatContainer.soulsCount != 0)
        //    soulDisplay.gameObject.SetActive(true);
        //else
        //    soulDisplay.gameObject.SetActive(false);
    }

    private void PlayerMove_OnColladedWithObstable(float speed)
    {
       // Debug.Log("speed = " + speed);

        var speedRatio = speed / playerMove.speed;
       // Debug.Log("forceOfStrike = " + speedRatio);

        var fallOverboard = (int) (boatContainer.soulsCount * speedRatio);
        //Debug.Log("fallOverboard " + fallOverboard);

        if (fallOverboard != 0) 
        {
            boatContainer.Remove(fallOverboard);
            
            ShowHideSoulDisplay();

            OnSoulsCountChange?.Invoke(boatContainer.soulsCount);
        }
    }
}
