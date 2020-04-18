using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    public Image BadBar;
    public Image GoodBar;
    public TextMeshProUGUI PopulationCount;

    public TextMeshProUGUI DeadShoresCount;
    public TextMeshProUGUI HellDoorCount;

    public Image HellBar;
    public TextMeshProUGUI HellCount;

    public Image BoatBar;
    public TextMeshProUGUI BoatCount;

    void Update()
    {
        PopulationCount.text = $"World: {Data.player.WorldPopulation.ToString("F0")}/{Data.consts.TargetPopulation.ToString("F0")}";
        GoodBar.fillAmount = Data.player.WorldPopulation / Data.consts.TargetPopulation;
        BadBar.fillAmount = 1 - Data.player.WorldPopulation / Data.consts.TargetPopulation;

        DeadShoresCount.text = Data.player.DeadShorePopulation.ToString("F0");
        HellDoorCount.text = Data.player.HellDoorPopulation.ToString("F0");

        HellBar.fillAmount = Data.player.HellPopulation / Data.player.TargetHellPopulation;
        HellCount.text = $"{Data.player.HellPopulation.ToString("F0")}/{Data.player.TargetHellPopulation.ToString("F0")}";

        BoatBar.fillAmount = Data.player.CurrentBoatCapacity / Data.player.MaxBoatCapacity;
        BoatCount.text = $"{Data.player.CurrentBoatCapacity.ToString("F0")}/{Data.player.MaxBoatCapacity.ToString("F0")}";
    }
}
