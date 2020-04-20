using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Button UpgradeBtn;
    public Text PriceText;
    public Text EffectText;

    [FormerlySerializedAs("Data")]
    public UpgradeData upgrade;

    public Action<UpgradeData> OnClickUpgrade;

    public PlayerController Player;

    public void OnClick()
    {
        // Проверка наличия денег
        if (Data.player.Coins < upgrade.Price)
        {
            Debug.Log("Не хватает денег!");
            return;
        }

        Data.player.Coins -= upgrade.Price;

        Debug.Log("OnUpgradeClick " + upgrade.ToString());
        switch (upgrade.Type)
        {
            case UpgradeType.Speed:
                Player.playerMove.speed += upgrade.Effect;
                break;

            case UpgradeType.Mobility:
                Player.playerMove.torque += upgrade.Effect;
                break;

            case UpgradeType.Capacity:
                Data.player.MaxBoatCapacity += upgrade.Effect;
                // boatContainer.MaxSoulsCount += upgrade.Effect;
                break;

                //Debug.LogError("Неизвестный тип улучшения");
        }     
    }

    private void OnValidate()
    {
        if(upgrade != null)
        {
            if (upgrade.Icon != null)
                UpgradeBtn.image.sprite = upgrade.Icon;

            PriceText.text = upgrade.Price.ToString();

            EffectText.text = upgrade.ToString();
        }
    }
}
