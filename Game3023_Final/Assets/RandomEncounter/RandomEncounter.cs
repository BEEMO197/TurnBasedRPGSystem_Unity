using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;

public class RandomEncounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("Random Battle");
        
        if(collider.gameObject.GetComponent<PlayerCharacterController>())
        {
            if(collider.gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0.2)
            {
                if(Random.Range(1, 100) == 5)
                {
                    SceneManager.LoadScene("Battle Scene");
                    Debug.Log("Random Battle");
                }
            }
        }
        
    }
}
