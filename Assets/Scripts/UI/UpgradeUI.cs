using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    Upgrade[] upgrades;

    public Action<UpgradeData> OnClickUpgrade;

    private void Awake()
    {
        upgrades = GetComponentsInChildren<Upgrade>(true);
        foreach (var item in upgrades)
        {
            item.OnClickUpgrade += (data) => OnClickUpgrade(data);
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
