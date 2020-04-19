using UnityEngine;

[CreateAssetMenu(menuName = "SOs/DisasterData", fileName = "DisasterData")]
public class DisasterData : ScriptableObject
{
    public string Name;

    public Sprite Icon;

    [Header("Процент от всего количества")]
    [Range(0.05f, 0.5f)]
    public float MinusSoulsPercent;

    [Header("Затем вычитание этого числа")] 
    [Range(0f, 500f)]
    public int MinusSoulsCount;  
}
