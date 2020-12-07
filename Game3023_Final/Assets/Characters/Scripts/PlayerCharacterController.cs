using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    public Rigidbody2D rigidBody;

    [SerializeField]
    public GameObject cameraGameObject;

    [SerializeField]
    private Animator animator;
 
    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private PlayerSoundsManager.Track tileOn;

    public PlayerSoundsManager.Track prevTileOn;

    [SerializeField]
    private PlayerSoundsManager playerSoundManager;

    public bool moveable = true;

    public Collider2D feetCol;

    void Start()
    {
        tileOn = PlayerSoundsManager.Track.Grass;
        playerSoundManager = FindObjectOfType<PlayerSoundsManager>();
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
        else if((SceneManager.GetActiveScene().name == "Overworld"))
        {
            //moveable = true;
        }

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
                playerSoundManager.PlayAudio();
            } else {
                animator.SetInteger("runState", 0);
            }
        } 
        else 
        {
            animator.SetInteger("runState", 0);
        }
    }

    public void ChangeTileOn(PlayerSoundsManager.Track tile)
    {
        prevTileOn = tileOn;
        tileOn = tile;
        playerSoundManager.SetTrack(tileOn);
    }
}