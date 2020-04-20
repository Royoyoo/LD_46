using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOs/Consts", fileName = "Consts")]
public class Consts : ScriptableObject
{
    public float StartDelay = 4f;

    public float TargetPopulation;
    [Header("Not used")]
    public float SoulsFromPopulationRate;
    [Space]
    public float SoulsDeadShoreSpeed;

    [Space]
    [Header("Поставки в ад")]
    public int HellDeliveryCount;
    public int HellDeliveryTimeout;
    [Space]

    public float GoToHellRate;
    public float HellAttackDeathPercent;

    public float RessurectRate;
    public float CoinsRate;

    public float QuestActiveTime;
    public float QuestCooldownTime;
}
