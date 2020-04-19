using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{

    MeshRenderer thisMR;
    GameLogic gameLogic;

    void Start()
    {
        thisMR = GetComponent<MeshRenderer>();
        gameLogic = FindObjectOfType<GameLogic>();
    }

    void Update()
    {
        thisMR.enabled = gameLogic.questSet;
    }
}
