using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab = null;

    public static PlayerCharacterController player = null;
    void Awake()
    {
        if(player == null)
        {
            player = Instantiate(playerPrefab, transform.position, transform.rotation).GetComponent<PlayerCharacterController>();
        }
    }
}
