using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private MusicManager() {}

    static MusicManager instance;

    public MusicManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MusicManager>(); 
            }

            return instance; 
        }
        private set 
        {

        }
    } 


    [SerializeField]
    AudioSource musicSource; 

    [SerializeField]
    AudioClip[] musicTracks;

    public enum Track
    {
        Overworld,
        Battle
    }

    // Start is called before the first frame update
    void Start()
    {
        MusicManager[] clones = FindObjectsOfType<MusicManager>();
        
        foreach (var mgr in clones)
        {
            if(mgr != Instance)
            {
                Destroy(mgr.gameObject); 
            }
        }

        SpawnPoint.player.GetComponent<EncounterManager>().onEnterEncounter.AddListener(EnterEncounterHandler);
        SpawnPoint.player.GetComponent<EncounterManager>().onExitEncounter.AddListener(ExitEncounterHandler);
        DontDestroyOnLoad(gameObject.transform.root);
    }

    void EnterEncounterHandler()
    {
        PlayTrack(Track.Battle);
    }

    void ExitEncounterHandler()
    {
        FadeInTrack(Track.Overworld);
    }
    
    public void PlayTrack(Track trackID)
    {
        musicSource.clip = musicTracks[(int)trackID];
        musicSource.Play(); 
    }

    public void FadeInTrack(Track trackID)
    {
        musicSource.volume = 0;
        PlayTrack(trackID);
        StartCoroutine(RaiseVolume(3.0f)); 
    }

    IEnumerator RaiseVolume(float transitionTime)
    {
        float timer = 0.0f;
        while(timer < transitionTime)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / transitionTime; 
            musicSource.volume = Mathf.SmoothStep(0,1,normalizedTime);
            yield return new WaitForEndOfFrame(); 
        }
    }


}
