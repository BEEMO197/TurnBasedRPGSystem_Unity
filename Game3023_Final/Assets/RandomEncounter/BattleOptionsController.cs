using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOptionsController : MonoBehaviour
{
    public TurnManager turnManager;

    public void AttackButtonBehaviour()
    {
        Debug.Log("Attacking"); 
    }

    public void DefendButtonBehaviour()
    {
        Debug.Log("Defending"); 
    }

    public void EndTurnBehaviour()
    {
        turnManager.IsPlayerPhase = false; 
    }
}
