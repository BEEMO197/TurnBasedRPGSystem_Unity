using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWalkSound : MonoBehaviour
{
    public PlayerCharacterController pcc;
    public PlayerSoundsManager.Track tileType;
    void Start()
    {
        pcc = FindObjectOfType<PlayerCharacterController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col == pcc.feetCol)
        {
            Debug.Log("Entered: " + tileType.ToString());
            pcc.ChangeTileOn(tileType);
        }
    }
}
