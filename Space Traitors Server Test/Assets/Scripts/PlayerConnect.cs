using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
This script handles the player while they are still in the lobby.
Decided to create seperate scripts/objects from regular players to avoid complications-
i.e. don't want players to be able to move around in the lobby, and so on
*/
public class PlayerConnect : MonoBehaviour
{
    //Player connection values
    public int playerID;
    public int playerNo;
    public bool connected;
    public Image playerImage;
    public string characterName;


    // Start is called before the first frame update
    void Start()
    {
        playerImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
