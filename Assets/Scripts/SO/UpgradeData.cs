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

    public Sprite Icon;

    public override string ToString()
    {
        return $"{Type}+{Effect}";
    }
}
