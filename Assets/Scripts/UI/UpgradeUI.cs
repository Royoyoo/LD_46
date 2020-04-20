using System;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    Upgrade[] upgrades;
       
    public PlayerController Player;

    private void Awake()
    {
        upgrades = GetComponentsInChildren<Upgrade>(true);
       
        foreach (var item in upgrades)
        {
            item.Player = Player;
          
            item.Refresh();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            upgrades[0].OnClick();
            //OnClickUpgrade?.Invoke(upgrades[0].Data);
            //.OnClickUpgrade(upgrades[0].Data);
        }
        /*
         TODO: маневренности нету
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            upgrades[1].OnClick();
           // OnClickUpgrade?.Invoke(upgrades[1].Data);
            //upgrades[1]?.OnClickUpgrade(upgrades[1].Data);
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            upgrades[2].OnClick();
            //OnClickUpgrade?.Invoke(upgrades[2].Data);
            //upgrades[2]?.OnClickUpgrade(upgrades[2].Data);
        }
    }

    //private void OnEnable()
    //{

    //}


    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }


}
