using UnityEngine;

public enum UpgradeType 
{ 
    Speed,
    // поворачиваемость
    Mobility,
    Capacity
}

[CreateAssetMenu(menuName = "SOs/UpgradeData", fileName = "UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType Type;
    public int Effect;

    public int Price;

    public int StartPrice;

    public Sprite Icon;

    [Space]
    [Header("Негативный эффект")]
    public UpgradeType NegativeType;
    public int NegativeEffect;

    public override string ToString()
    {
        return $"{Type}+{Effect}";
    }
}
