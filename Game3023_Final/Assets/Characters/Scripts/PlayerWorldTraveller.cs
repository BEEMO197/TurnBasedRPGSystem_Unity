using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWorldTraveller : MonoBehaviour
{
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // private void OnLevelWasLoaded(int level)
    // {

    //     Debug.Log("Scene Change Level: "+level);
    //     if(level == 1) // 1 == BattleScene
    //     {
    //         SpawnPoint p = FindObjectOfType<SpawnPoint>();
    //         transform.position = p.transform.position;
    //         GetComponent<SpriteRenderer>().flipX = false; 
    //     } 
    //     else if (level == 0) // 0 == Overworld
    //     {
    //         if(GetComponent<EncounterManager>().lastPosition != null)
    //         {
    //             transform.position = GetComponent<EncounterManager>().lastPosition;
    //         } 
    //     }
    // }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("On Scene Loaded: "+scene.name);

        if(scene.name == "Battle Scene")
        {
            SpawnPoint p = FindObjectOfType<SpawnPoint>();
            transform.position = p.transform.position;
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<PlayerCharacterController>().ToggleBattleMenu(true);
            FindObjectOfType<TurnManager>().Player = this.gameObject.GetComponent<PlayerCharacterController>();
            GetComponent<PlayerCharacterController>().turnManager = FindObjectOfType<TurnManager>();
            GetComponent<PlayerCharacterController>().SetAnimatorBattleFlag(true);
            GetComponent<PlayerCharacterController>().textBoxAnimator = FindObjectOfType<TextBoxAnimator>(); 
             
        }
        else if(scene.name == "Overworld")
        {
            GetComponent<PlayerCharacterController>().ToggleBattleMenu(false);
            GetComponent<PlayerCharacterController>().SetAnimatorBattleFlag(false);
            if(GetComponent<EncounterManager>().lastPosition != Vector3.zero)
            {
                transform.position = GetComponent<EncounterManager>().lastPosition;
            } 
            else 
            {
                SpawnPoint p = FindObjectOfType<SpawnPoint>();
                transform.position = p.transform.position; 
            }
        }
    }
}
