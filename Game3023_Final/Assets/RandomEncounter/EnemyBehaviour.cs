using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIONS{
    ATTACK,
    FIREBOW,
    INTIMIDATE,
    TAUNT,
    Count
}


public class EnemyBehaviour : MonoBehaviour
{
    public TurnManager turnManager;
    public GameObject battleMenu;

    public ACTIONS action;

    private bool _isEnabled;
    public bool IsEnabled
    {
        get{
            return _isEnabled; 
        }
        set{
            _isEnabled = value;
            if(value)
            {
                StartCoroutine(EnemyPhaseActions()); 
            }
        }
    }
    public void EnableTurn()
    {
        Debug.Log("Bushman is Preparing");
        IsEnabled = true; 
    }

    IEnumerator EnemyPhaseActions()
    {
        battleMenu.SetActive(true);
        action = (ACTIONS)Random.Range(0,(float)ACTIONS.Count);
        switch(action)
        {
            case ACTIONS.ATTACK:
                StartCoroutine(AttackPlayer());
                break;
            case ACTIONS.FIREBOW:
                StartCoroutine(FireBow());
                break;
            case ACTIONS.INTIMIDATE:
                StartCoroutine(Intimidate());
                break;
            case ACTIONS.TAUNT:
                StartCoroutine(Taunt());
                break;
            default:
                Debug.Log("Unknown Enemy Action");
                break;
        }
        
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator AttackPlayer()
    {
        Debug.Log("Attacking Player");
        yield return new WaitForSeconds(3.0f);
        PassTurntoPlayer();
    }

    IEnumerator FireBow()
    {
        Debug.Log("Firing Bow at Player");
        yield return new WaitForSeconds(3.0f);
        PassTurntoPlayer();
    }

    IEnumerator Intimidate()
    {
        Debug.Log("Intimidating Player");
        yield return new WaitForSeconds(3.0f);
        PassTurntoPlayer();
    }

    IEnumerator Taunt()
    {
        Debug.Log("Taunting Player");
        yield return new WaitForSeconds(3.0f);
        PassTurntoPlayer();
    }

    void PassTurntoPlayer()
    {
        Debug.Log("Bushman Ending Turn");
        IsEnabled = false;
        turnManager.IsPlayerPhase = true;
        battleMenu.SetActive(false); 
    }
}
