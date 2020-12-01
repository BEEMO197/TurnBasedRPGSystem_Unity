using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FleeButtonBehaviour : MonoBehaviour
{
    public void OnFleeButtonPressed()
    {
        EncounterManager encounterManager = FindObjectOfType<EncounterManager>();
        if(encounterManager != null)
        {
            encounterManager.ExitEncounter(); 
        }
        
    }
}
