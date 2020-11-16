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

        Debug.Log("Scene Change!");
        if(spawnLocation != null)
        {
            SpawnPoint p = FindObjectOfType<SpawnPoint>();

            transform.position = p.transform.position;
        }
    }
}
