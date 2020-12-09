using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float health = 100.0f;

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

    public bool moveable = true;

    public Collider2D feetCol;

    void Start()
    {
        tileOn = PlayerSoundsManager.Track.Grass;
        playerSoundManager = PlayerSoundsManager.Instance;
        renderer = GetComponent<SpriteRenderer>(); 
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
}