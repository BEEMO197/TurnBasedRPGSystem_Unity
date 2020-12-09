using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOptionsController : MonoBehaviour
{
    public void AttackButtonBehaviour()
    {
        Debug.Log("Attacking");
        GetComponentInParent<PlayerCharacterController>().EndTurn(); 
    }
    public void DefendButtonBehaviour()
    {
        Debug.Log("Defending");
        GetComponentInParent<PlayerCharacterController>().EndTurn(); 
    }
    public void ThrowKunai()
    {
        Debug.Log("Throwing Kunai");
        GetComponentInParent<PlayerCharacterController>().EndTurn();
    }
}
