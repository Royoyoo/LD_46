using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Button UpgradeBtn;
    public Text PriceText;
    public Text EffectText;

    public UpgradeData Data;

    private void OnValidate()
    {
        if(Data != null)
        {
            if (Data.Icon != null)
                UpgradeBtn.image.sprite = Data.Icon;

            PriceText.text = Data.Price.ToString();

            EffectText.text = Data.ToString();
        }
    }
}
