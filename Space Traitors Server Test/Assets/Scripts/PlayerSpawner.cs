using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject server;
    private GameObject playerStorage;
    public List<GameObject> removeList;
    private int maxPlayers, currPlayers;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        server = GameObject.FindGameObjectWithTag("Server");
        playerStorage = GameObject.FindGameObjectWithTag("RoundManager");
        maxPlayers = server.GetComponent<Server>().players.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (currPlayers < maxPlayers)
        {
            int j = 0;
            foreach (GameObject player in server.GetComponent<Server>().players)
            {
                //Spawn player in game, give them the lobby player's ID
                GameObject spawnedPlayer = Instantiate(playerStorage.GetComponent<RoundManager>().playerObject, transform.position, Quaternion.identity) as GameObject; //TODO: spawn position is a placeholder, change later

                spawnedPlayer.GetComponent<Player>().playerID = server.GetComponent<Server>().playerIDs[j];
                spawnedPlayer.GetComponent<Player>().characterName = player.GetComponent<PlayerConnect>().characterName;

                j++;
            }
            currPlayers++;
        }
        ////Find player objects that have not been set with a player and remove them from round manager
        //foreach (GameObject player in playerStorage.GetComponent<RoundManager>().playersInGame)
        //{
        //    if (player.GetComponent<Player>().playerID == 0)
        //    {
        //        removeList.Add(player);
        //    }
        //}

        //foreach (GameObject player in removeList)
        //{
        //    playerStorage.GetComponent<RoundManager>().playersInGame.Remove(player);
        //}
    }

  
}
