using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterController : MonoBehaviour
{
    List<int> wew;

    [SerializeField]
    float speed = 5;

    [SerializeField]
    public Rigidbody2D rigidBody;

    public bool moveable = true;
    // Update is called once per frame
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
            moveable = false;
            movementVector = new Vector2(0.0f, 0.0f);
            rigidBody.velocity = movementVector;
        }
        else if((SceneManager.GetActiveScene().name == "Overworld"))
        {
            moveable = true;
        }

        if (moveable)
        {
            rigidBody.velocity = movementVector;
        }
    }
}