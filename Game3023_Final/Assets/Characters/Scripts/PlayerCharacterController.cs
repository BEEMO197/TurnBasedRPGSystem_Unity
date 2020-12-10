using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public float defenceSkill = 0.25f;
    public float accuracySkill = 0.60f;
    public float damage = 25.0f;  

    [SerializeField]
    public Rigidbody2D rigidBody;

    [SerializeField]
    public GameObject cameraGameObject;

    [SerializeField]
    public GameObject battleMenu;

    [SerializeField]
    public GameObject healthBar; 

    [SerializeField]
    private Animator animator = null;
 
    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private PlayerSoundsManager.Track tileOn;

    public PlayerSoundsManager.Track prevTileOn;

    [SerializeField]
    private PlayerSoundsManager playerSoundManager;

    [SerializeField]
    public TurnManager turnManager = null;
    public TextBoxAnimator textBoxAnimator;  

    public bool moveable = true;

    public Collider2D feetCol;

    public ParticleSystem kunaiParticle;
    public ParticleSystem defenceUpParticle;
    

    void Start()
    {
        tileOn = PlayerSoundsManager.Track.Grass;
        playerSoundManager = PlayerSoundsManager.Instance;
        renderer = GetComponent<SpriteRenderer>();
        UpdateHealthBar(); 
    }
    void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementVector *= speed;

        if(movementVector.magnitude <= 0.1f)
        {
            movementVector = new Vector2(0.0f, 0.0f);
        }

        if (SceneManager.GetActiveScene().name == "Battle Scene")
        {
            //moveable = false;
            movementVector = new Vector2(0.0f, 0.0f);
            rigidBody.velocity = movementVector;
        }
        // else if((SceneManager.GetActiveScene().name == "Overworld"))
        // {
        //     //moveable = true;
        // }

        if (moveable)
        {
            rigidBody.velocity = movementVector;
        }

        UpdateAnimator(movementVector);

    }

    void UpdateAnimator(Vector2 movementVector)
    {
        if(movementVector.x > 0)
        {
            renderer.flipX = false; 
        } 
        else if (movementVector.x < 0)
        {
            renderer.flipX = true; 
        }

        if(movementVector.sqrMagnitude != 0)
        {
            if(moveable)
            {
                animator.SetInteger("runState", 1);
                PlayerSoundsManager.Instance.PlayAudio();
            } else {
                animator.SetInteger("runState", 0);
            }
        } 
        else 
        {
            animator.SetInteger("runState", 0);
        }
    }

    public void SetAnimatorBattleFlag(bool flag)
    {
        animator.SetBool("battleIdle", flag);
    }

    public void ChangeTileOn(PlayerSoundsManager.Track tile)
    {
        prevTileOn = tileOn;
        tileOn = tile;
        PlayerSoundsManager.Instance.SetTrack(tileOn);
    }

    public void ToggleBattleMenu(bool flag)
    {
        battleMenu.SetActive(flag);
    }

    public void EndTurn()
    {
        if(turnManager != null)
        {
            Debug.Log("Player Ending Turn");
            turnManager.IsPlayerPhase = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= (defenceSkill*damage);
        if(health <= 0)
        {
            health = 0;
            StartCoroutine(PlayerLost()); 
        } 
        UpdateHealthBar();
        StartCoroutine(TakeHitAnimation());
    }

    public void UpdateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(health/maxHealth,1,1);
    }

    public void AttackEnemy()
    {
       StartCoroutine(AttackEnemyCoroutine()); 
    }

    public void DefenceUp()
    {
       StartCoroutine(DefenceUpCoroutine()); 
    }

    public void ThrowKunai()
    {
       StartCoroutine(ThrowKunaiCoroutine());
    }

    IEnumerator AttackEnemyCoroutine()
    {
         // Get Enemy
        if(turnManager.enemy != null)
        {
            float hit = Random.Range(0.0f, 1.0f);
            // Check if hits
            if(hit <= accuracySkill)
            {
                // Apple Damage
                Debug.Log("Attacking Enemy");
                textBoxAnimator.AnimateText("The player has attacked for "+damage.ToString()+" damage"); 
                turnManager.enemy.TakeDamage(damage);
            } 
            else 
            {
                textBoxAnimator.AnimateText("The player missed");
                Debug.Log("Missed Enemy"); 

            }
        }
        // Play Animation
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(3.0f);
        animator.SetBool("attack", false); 
         // Pass Turn
        EndTurn(); 
    }

    IEnumerator DefenceUpCoroutine()
    {
        Debug.Log("Raising Defence"); 
        // Increase Defense Skill
        defenceSkill *= 1.05f;
        textBoxAnimator.AnimateText("The Player is raising defence"); 
        if(defenceSkill >= 0.50f)
        {
            defenceSkill = 0.50f; 
        } 
        // Play Animation/Particle Effect
        defenceUpParticle.Play();
        yield return new WaitForSeconds(3.0f);
        EndTurn(); 
    }

    IEnumerator ThrowKunaiCoroutine()
    {
        Debug.Log("Throwing Kunai");
        textBoxAnimator.AnimateText("The player is throwing a kunai for " + (damage*0.10f).ToString() + " damage"); 
        // Apply 10% of Damage Level to Enemy
        turnManager.enemy.TakeDamage(damage*0.10f);
        // Play Animation/Particle Effect
        animator.SetBool("kunai", true);
        kunaiParticle.Play();
        yield return new WaitForSeconds(3.0f);
        animator.SetBool("kunai", false);
        EndTurn(); 
    }

    IEnumerator TakeHitAnimation()
    {
        animator.SetBool("hit", true);
        yield return new WaitForSeconds(2.5f); 
        animator.SetBool("hit", false);
    }
    public void StartPlayerWon()
    {
        StartCoroutine(PlayerWon()); 
    }

    IEnumerator PlayerWon()
    {
        textBoxAnimator.AnimateText("The Player has won the fight"); 
        yield return new WaitForSeconds(3.0f);
        textBoxAnimator.AnimateText("BushMan has fled the scene"); 
        yield return new WaitForSeconds(3.0f);
        GetComponent<EncounterManager>().ExitEncounter();  
    }

    IEnumerator PlayerLost()
    {
        textBoxAnimator.AnimateText("The Player has lost the fight"); 
        yield return new WaitForSeconds(3.0f);
        textBoxAnimator.AnimateText("BushMan dances in Victory!"); 
        yield return new WaitForSeconds(3.0f);
        GetComponent<EncounterManager>().ExitEncounter();  
    }
}