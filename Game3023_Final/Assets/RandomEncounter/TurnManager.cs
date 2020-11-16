using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager : MonoBehaviour
{
    public GameObject PlayerBattleMenu; 
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
                PlayerBattleMenu.SetActive(false);
                enemy.EnableTurn(); 
            } 
            else 
            {
                PlayerBattleMenu.SetActive(true);
            }
        }
    }
}
