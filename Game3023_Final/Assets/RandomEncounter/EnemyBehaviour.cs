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
    public TextBoxAnimator textBoxAnimator;

    public ACTIONS action;

    // Controls the %chance of each move happening

    // move1Chance is the MAX chance for Move 1 to activate, and the MIN for Move 2
    // move2Chance is the Max chance for Move 2 to activate, and the MIN for Move 3
    // move3Chance is the Max chance for Move 3 to activate, and the MIN for Move 4

    // Move 4 is just the left over, so whatever else does not activate Move 4 will

    public int move1Chance = 25;
    public int move2Chance = 50;
    public int move3Chance = 75;

    [SerializeField]
    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public float defenceSkill = 0.35f;
    public float accuracySkill = 0.90f;
    public float damage = 50.0f;

    public ParticleSystem rockParticle;
    public ParticleSystem bowParticle;
    public ParticleSystem tauntParticle;
    public ParticleSystem intimidateParticle;  

    [SerializeField]
    public GameObject healthBar;
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
                if(health <= 0)
                {
                    turnManager.Player.StartPlayerWon(); 
                } 
                else  
                {
                    StartCoroutine(EnemyPhaseActions());
                }
                 
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

        int moveChance = Random.Range(0, 100);

        if(moveChance < move1Chance)
        {
            StartCoroutine(AttackPlayer());
        }
        else if (moveChance >= move1Chance && moveChance < move2Chance)
        {
            StartCoroutine(FireBow());
        }
        else if (moveChance >= move2Chance && moveChance < move3Chance)
        {
            StartCoroutine(Intimidate());
        }
        else
        {
            StartCoroutine(Taunt());
        }
        //action = (ACTIONS)Random.Range(0,(float)ACTIONS.Count);

        /*
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
        */

        if (turnManager.enemy.health < turnManager.enemy.maxHealth / 2.0f)
        {
            move1Chance = 20;
            move2Chance = 40;
            move3Chance = 70;
        }
        else if (turnManager.Player.health < turnManager.Player.maxHealth / 2.0f)
        {
            move1Chance = 30;
            move2Chance = 60;
            move3Chance = 80;
        }
        else if (turnManager.Player.defenceSkill >= 0.3)
        {
            move1Chance = 20;
            move2Chance = 40;
            move3Chance = 80;
        }
        else
        {
            move1Chance = 25;
            move2Chance = 50;
            move3Chance = 75;
        }

        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator AttackPlayer()
    {
        float hitChance = Random.Range(0.0f,1.0f);
        if(hitChance <= (accuracySkill+0.20f))
        {
            Debug.Log("Attacking Player");
            rockParticle.Play();
            turnManager.Player.TakeDamage(damage*0.5f);
            textBoxAnimator.AnimateText("BushMan throws a nearby rock for "+(damage*0.5f).ToString()+" damage");
        } 
        else 
        {
            Debug.Log("Bushman misses");
            textBoxAnimator.AnimateText("BushMan Misses in Disgrace");
        }
        yield return new WaitForSeconds(2.0f);
        PassTurntoPlayer();
    }

    IEnumerator FireBow()
    {
        float hitChance = Random.Range(0.0f,1.0f);
        if(hitChance <= accuracySkill)
        {
            Debug.Log("Firing Bow at Player");
            bowParticle.Play(); 
            turnManager.Player.TakeDamage(damage);
            textBoxAnimator.AnimateText("BushMan fires an arrow for "+damage.ToString()+" damage");
        }
        else 
        {
            Debug.Log("Bushman misses");
            textBoxAnimator.AnimateText("BushMan Misses in Disgrace");
        }
        yield return new WaitForSeconds(2.0f);
        PassTurntoPlayer();
    }

    IEnumerator Intimidate()
    {
        Debug.Log("Intimidating Player");
        intimidateParticle.Play(); 
        turnManager.Player.defenceSkill += (turnManager.Player.defenceSkill*0.05f)*(-1);
        textBoxAnimator.AnimateText("BushMan does a Scare lowers opponents defence");
        yield return new WaitForSeconds(2.0f);
        PassTurntoPlayer();
    }

    IEnumerator Taunt()
    {
        Debug.Log("Taunting Player");
        tauntParticle.Play();
        turnManager.Player.accuracySkill += (turnManager.Player.accuracySkill*0.05f)*(-1);
        textBoxAnimator.AnimateText("BushMan shows disrespect lowers opponents accuracy");
        yield return new WaitForSeconds(2.0f);
        PassTurntoPlayer();
    }

    void PassTurntoPlayer()
    {
        Debug.Log("Bushman Ending Turn");
        IsEnabled = false;
        turnManager.IsPlayerPhase = true;
    }

    public void TakeDamage(float damage)
    {
        health -= (defenceSkill*damage);
        if(health <= 0)
        {
            health = 0;
        } 
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(health/maxHealth,1,1);
    }
}
