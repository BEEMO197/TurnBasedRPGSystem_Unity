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
    private Animator animator;
 
    [SerializeField]
    private SpriteRenderer renderer; 

    public bool moveable = true;
    void Start()
    {
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
            } else {
                animator.SetInteger("runState", 0);
            }
        } 
        else 
        {
            animator.SetInteger("runState", 0);
        }
    }
}