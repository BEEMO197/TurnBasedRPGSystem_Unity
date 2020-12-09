using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager : MonoBehaviour
{
    public PlayerCharacterController Player; 
    public EnemyBehaviour enemy;
    private bool _isPlayerPhase;
    public bool IsPlayerPhase
    {
        get{
            return _isPlayerPhase; 
        }

        set{
            _isPlayerPhase = value;
            if(_isPlayerPhase == false)
            {
                enemy.EnableTurn(); 
            } 
            else 
            {
                Player.ToggleBattleMenu(true);
            }
        }
    }
}
