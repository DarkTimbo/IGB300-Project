using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public List<GameObject> playersInGame = new List<GameObject>();
    public GameObject server;
    public GameObject playerObject;
    private int PlayerIndex;
    private int Round;
    private bool setup = false, randomised = true, initialisePlayer = false;
    private Scene currentScene;
    private string sceneName;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        server = GameObject.FindGameObjectWithTag("Server");
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "server")
        {

            //Allow a frame for the list to be filled up with players before acting
            if (!setup)
            {
                setup = true;
            }
            else
            {

                if (!randomised)
                {
                    //Randomise the list
                    playersInGame = playersInGame.OrderBy(x => Random.value).ToList();
                    randomised = true;
                }

                if (PlayerIndex >= playersInGame.Count)
                //If all players have been cycled through, the round is over
                {
                    //Restart a new round
                    PlayerIndex = 0;
                    Round++;
                }

                ////First player in the array gets their turn
                //if (!initialisePlayer)
                //{
                //    //Debug.Log(playersInGame[0]);
                    
                //    initialisePlayer = true;
                //}

            }
        }
    }

    public void IncrementTurn()
    {
        playersInGame[PlayerIndex].GetComponent<Player>().Turn = false;
        server.GetComponent<Server>().ClientTurnChange(playersInGame[PlayerIndex].GetComponent<Player>().playerID);
        PlayerIndex++;
        playersInGame[PlayerIndex].GetComponent<Player>().Turn = true;
        server.GetComponent<Server>().ClientTurnChange(playersInGame[PlayerIndex].GetComponent<Player>().playerID);
        
    }

    public void turn1()
    {
        index = playersInGame[0].GetComponent<Player>().playerID;
        server.GetComponent<Server>().ClientTurnChange(index);
    }
}
