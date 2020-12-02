using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    public PlayerCharacterController playerCharacterData;


    void Start()
    {
        playerCharacterData = FindObjectOfType<PlayerCharacterController>();
    }

    // Saves the Game State, Position, Abilities, etc.
    public void SaveGame()
    {
        Debug.Log("Saving...");

        PlayerPrefs.SetFloat("Player_Position_X", playerCharacterData.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", playerCharacterData.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z", playerCharacterData.transform.position.z);
        PlayerPrefs.Save();

        Debug.Log("Game Saved!");
    }

    // Loads the latest Game State, Position, Abilities, etc.
    public void LoadGame()
    {
        Debug.Log("Loading...");

        float playerLocX = PlayerPrefs.GetFloat("Player_Position_X");
        float playerLocY = PlayerPrefs.GetFloat("Player_Position_Y");
        float playerLocZ = PlayerPrefs.GetFloat("Player_Position_Z");

        playerCharacterData.transform.position = new Vector3(playerLocX, playerLocY, playerLocZ);
        playerCharacterData.cameraGameObject.transform.position = new Vector3(playerLocX, playerLocY, playerCharacterData.cameraGameObject.transform.position.z);

        Debug.Log("Game Loaded!");
        Debug.Log("Got: X: " + playerLocX + " Y: " + playerLocY + " Z: " + playerLocZ);
    }
}
