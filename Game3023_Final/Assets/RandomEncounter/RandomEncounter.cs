
using UnityEngine;



public class RandomEncounter : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {   
        if(collider.gameObject.GetComponent<PlayerCharacterController>())
        {
            if(collider.gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0.2)
            {
                if(Random.Range(1, 100) <= 15)
                {
                    collider.gameObject.GetComponent<EncounterManager>().EnterEncounter();
                    Debug.Log("Random Battle");
                }
            }
        }
        
    }
}
