using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EncounterManager : MonoBehaviour
{

    public UnityEvent onEnterEncounter;
    public UnityEvent onExitEncounter;

    public Vector3 lastPosition{ get; set; }  
    public void EnterEncounter()
    {
        StartCoroutine(EnterEncounterCoroutine());
    }

    IEnumerator EnterEncounterCoroutine()
    {
        onEnterEncounter.Invoke();
        lastPosition = gameObject.transform.position;
        GetComponent<PlayerCharacterController>().moveable = false;   
        yield return new WaitForSeconds(2.0f);  
        SceneManager.LoadScene("Battle Scene");
    }

    public void ExitEncounter()
    {
        onExitEncounter.Invoke();
        GetComponent<PlayerCharacterController>().moveable = true;    
        // In a full game, your code should remember the player's last area and return there
        SceneManager.LoadScene("Overworld"); 
    }
}
