using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisasterUI : MonoBehaviour
{
    public DisasterData Disaster;

    public Image DisasterImage;
    public TextMeshProUGUI DisasterResultText;  

    [Header("Время показа, с")]
    [Range(0.5f, 20f)]
    public float DisplayTime;

    float startTime;
    bool _show = false;

    public void UpdateUI(DisasterData disaster, int result)
    {
        Show(true);

        startTime = 0f;    

        if (Disaster.Icon != null)
            DisasterImage.sprite = Disaster.Icon;

        DisasterResultText.text = $"After {disaster.Name} were killed {result} people.";
            //$"{Disaster.Name}. -{100}";
    }

    private void Update()
    {
        if (_show)
        {
            startTime += Time.deltaTime;

            if(startTime > DisplayTime)
            {
                Show(false);
            }
        }             
    }


    public void Show(bool show)
    {
        _show = show;
        gameObject.SetActive(show);
    }

    private void OnValidate()
    {
        if (Disaster != null)
        {
            if (Disaster.Icon != null)
                DisasterImage.sprite = Disaster.Icon;

            DisasterResultText.text = $"After {Disaster.Name.ToLower()} was killed {100} people.";
        }
    }
}
