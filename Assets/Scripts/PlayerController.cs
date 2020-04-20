using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject soulDisplay;

    //public SoulsContainer boatContainer;

    public SoulsContainer InteractionContainer { get; internal set; }
    public bool CanTakeQuest { get; internal set; }

    //public InteractionType InteractionType { get; internal set; }

    // int - количество на лодке
    public event Action<float> OnSoulsCountChange;

    public PlayerMove playerMove;

    GameLogic gameLogic;

    public UpgradeUI upgradeUI;

    public SoulsParticle particle;

    public float MinSpeed;

    private void Awake()
    {
        //boatContainer = GetComponent<SoulsContainer>();
        playerMove = GetComponent<PlayerMove>();
        gameLogic = FindObjectOfType<GameLogic>();
        //MinSpeed = playerMove.speed / 2;
        //Debug.Log("MinSpeed " + MinSpeed);
    }

    private void OnEnable()
    {
        //boatContainer.OnSoulsCountChange += OnSoulsCountChange;
        playerMove.OnColladedWithObstable += PlayerMove_OnColladedWithObstable;
       // upgradeUI.OnClickUpgrade += UpgradeUI_OnUpgradeClick;
    }

    private void OnDisable()
    {
       // boatContainer.OnSoulsCountChange -= OnSoulsCountChange;
        playerMove.OnColladedWithObstable -= PlayerMove_OnColladedWithObstable;
       // upgradeUI.OnClickUpgrade -= UpgradeUI_OnUpgradeClick;
    }

   
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanTakeQuest)
            {
                gameLogic.AcceptQuest();                
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {   
            if (InteractionContainer == null)
            {
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
                    var resurrected = gameLogic.RessurectSouls(InteractionContainer);
                    InteractionContainer.NeedDissolve(resurrected);// += resurrected;
                   // InteractionContainer.SoulsCount += resurrected;

                    // var resValue = Math.Max(1, (int)resurrected);
                    //// Debug.Log(Math.Max(1, (int)resurrected));
                   // InteractionContainer.Dissolve((int)Data.player.CurrentBoatCapacity);
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

        // улучшения показываются из UpgradeTrigger
       /* if (Input.GetKeyDown(KeyCode.F))
        {
          

            if (upgradeUI.gameObject.activeSelf)
                upgradeUI.Show(false);
            else
                upgradeUI.Show(true);
        }*/
    }
/*
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
    */
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
        Debug.Log("speed = " + speed);

        // количество порогов скорости в текущей скорости       
        var speedRatio = (int)(speed / Data.player.OverboardSpeedThreshold);
        Debug.Log("speedRatio = " + speedRatio);

        // какая часть душ вывалится
        var overboardParts = speedRatio * Data.player.OverboardPart;

        //var fallOverboard = (int)(Data.player.soulsCount * speedRatio);
        var fallOverboard = (int)(Data.player.CurrentBoatCapacity * overboardParts);
        Debug.Log("fallOverboard " + fallOverboard);
               
        if (fallOverboard != 0)
        {
            particle.Emit(fallOverboard);

            Data.player.CurrentBoatCapacity -= fallOverboard;
            if (Data.player.CurrentBoatCapacity < 0)
                Data.player.CurrentBoatCapacity = 0;
            //boatContainer.Remove(fallOverboard);

            ShowHideSoulDisplay();

          //  OnSoulsCountChange?.Invoke(boatContainer.soulsCount);
        }
    }

    private void UpgradeUI_OnUpgradeClick(UpgradeData upgrade)
    {
        // Проверка наличия денег
        if(Data.player.Coins < upgrade.Price)
        {
            Debug.Log("Не хватает денег!");
            return;
        }

        Data.player.Coins -= upgrade.Price;

        Debug.Log("OnUpgradeClick " + upgrade.ToString());
        switch (upgrade.Type)
        {
            case UpgradeType.Speed:
                playerMove.speed += upgrade.Effect;
                break;

            case UpgradeType.Mobility:
                playerMove.torque += upgrade.Effect;
                break;

            case UpgradeType.Capacity:
                Data.player.MaxBoatCapacity += upgrade.Effect;
               // boatContainer.MaxSoulsCount += upgrade.Effect;
                break;

            //Debug.LogError("Неизвестный тип улучшения");
        }
    }      
}
