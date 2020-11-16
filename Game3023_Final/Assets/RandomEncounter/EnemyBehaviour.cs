using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public TurnManager turnManager;
    public GameObject enemyTextLabel;

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
                StartCoroutine("EnemyPhaseActions"); 
            }
        }
    }
    public void EnableTurn()
    {
        IsEnabled = true; 
    }

    IEnumerator EnemyPhaseActions()
    {
        enemyTextLabel.SetActive(true); 
        yield return new WaitForSeconds(2.0f);
        enemyTextLabel.SetActive(false);
        turnManager.IsPlayerPhase = true;
        IsEnabled = false;   
    }
}
