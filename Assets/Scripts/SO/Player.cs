using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SOs/Player", fileName = "Player")]
public class Player : ScriptableObject
{
    public float Coins;
    public float WorldPopulation;
    public float DeadShorePopulation;
    public float HellDoorPopulation;

    public float HellPopulation;
    public float TargetHellPopulation;

    public float CurrentBoatCapacity;
    public float MaxBoatCapacity;

}
