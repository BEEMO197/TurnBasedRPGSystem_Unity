﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsManager : MonoBehaviour
{
    private PlayerSoundsManager() { }

    private static PlayerSoundsManager instance = null;

    public static PlayerSoundsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerSoundsManager>();
            }

            return instance;
        }
        private set
        {

        }
    }

    [SerializeField]
    AudioSource audioSource = null;

    [SerializeField]
    AudioClip[] audioTracks = null;

    public enum Track
    {
        Grass,
        TallGrass,
        Brick
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerSoundsManager[] clones = FindObjectsOfType<PlayerSoundsManager>();

        foreach (var mgr in clones)
        {
            if (mgr != Instance)
            {
                Destroy(mgr.gameObject);
            }
        }
        DontDestroyOnLoad(gameObject.transform.root);
    }

    public void SetTrack(Track trackID)
    {
        audioSource.clip = audioTracks[(int)trackID];
    }

    public void PlayAudio()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
