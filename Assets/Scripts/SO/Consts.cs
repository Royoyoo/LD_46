using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOs/Consts", fileName = "Consts")]
public class Consts : ScriptableObject
{
    public float TargetPopulation;
    public float GoToHellRate;
    public float HellAttackDeathPercent;

    public float RessurectRate;
    public float CoinsRate;

    public float QuestActiveTime;
    public float QuestCooldownTime;
}
