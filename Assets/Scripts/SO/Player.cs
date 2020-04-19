using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string startMessage;
    public string finishMessage;
    public float Effect;
}

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

    [Header("Скорость (Для выпадения душ)")]
    // Устанавливается экспериментальным путем с текущими параметрами лодки    
    public float MaxSpeed = 1.44f;
    // TODO
    public float MaxSpeedUpgr1 = 1;
    public float MaxSpeedUpgr2 = 1;
    public float MaxSpeedUpgr3 = 1;

    public bool gotQuest = false;
    public Quest currentQuest = null;
}
