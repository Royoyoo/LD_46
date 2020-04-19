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
    // за каждые 0,5 скорости    
    public float OverboardSpeedThreshold = 0.5f;
    // выпадает 10% от текущего количества
    public float OverboardPart = 0.1f;   

    public bool gotQuest = false;
    public Quest currentQuest = null;
}
