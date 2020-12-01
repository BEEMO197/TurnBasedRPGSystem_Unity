using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWorldTraveller : MonoBehaviour
{
    string spawnLocation = null;

    //Property
    public string SpawnLocation
        {
        get;
        set;
        }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {

        Debug.Log("Scene Change Level: "+level);
        if(level == 1) // 1 == BattleScene
        {
            SpawnPoint p = FindObjectOfType<SpawnPoint>();
            transform.position = p.transform.position;
            GetComponent<SpriteRenderer>().flipX = false; 
        } 
        else if (level == 0) // 0 == Overworld
        {
            if(GetComponent<EncounterManager>().lastPosition != null)
            {
                transform.position = GetComponent<EncounterManager>().lastPosition;
            } 
        }
    }
}
