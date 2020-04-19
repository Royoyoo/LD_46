using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DialogSide
{
    Left, Right
}

public enum DialogPortrait
{
    Haron, Aid, Tanatos, Soul, Upgrader
}

[System.Serializable]
public class DialogPopup
{
    public Image portrait;
    public TextMeshProUGUI text;

    public void Setup(Sprite portrait, string message)
    {
        this.portrait.sprite = portrait;
        text.text = message;
    }
}

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
    public TextMeshProUGUI CoinsCount;

    public Animation popupAnim;
    public List<DialogPopup> popups = new List<DialogPopup>();
    public List<Sprite> portraits = new List<Sprite>();

    public UpgradeUI UpgradeUi;
    public DisasterUI DisasterUi;
    
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
        CoinsCount.text = $"Coins: {Data.player.Coins.ToString("F0")}";
    }

    public void ShowDialog(DialogSide side, DialogPortrait portrait, string message)
    {
        switch (side)
        {
            case DialogSide.Left:
                popupAnim.Play("UI_LeftTextPopup");
                popups[0].Setup(portraits[(int)portrait], message);
                break;
            case DialogSide.Right:
                popupAnim.Play("UI_RightTextPopup");
                popups[1].Setup(portraits[(int)portrait], message);
                break;
            default:
                break;
        }
    }

    public void ShowQuestMessage(string message)
    {
        Debug.Log("ShowQuestMessage");
    }
}
