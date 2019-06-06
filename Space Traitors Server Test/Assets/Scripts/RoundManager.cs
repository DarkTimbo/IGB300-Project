using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public List<GameObject> playersInGame = new List<GameObject>();
    private Scene currentScene;
    private Text rndText;

    public GameObject server;
    public GameObject playerObject;
    public GameObject canvas;
    private GameObject AI;
    private bool SetObject = true;

    private int index, PlayerIndex;
    private int Round = 1;
    private bool setup = false, randomised = true, initialisePlayer = false;
    private string sceneName;

    public GameObject sfxSource;
    private SFXManager sfxManager;
  
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        server = GameObject.FindGameObjectWithTag("Server");

        sfxManager = sfxSource.GetComponent<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "server") {

            if (SetObject == true) {

                AI = GameObject.FindGameObjectWithTag("Ai Power");
                rndText = GameObject.Find("RoundText").GetComponent<Text>();
                SetObject = false;
            }

            //Allow a frame for the list to be filled up with players before acting
            if (!setup)
            {
                setup = true;
            }
            else
            {
                rndText.text = rndText.text = "Round " + Round.ToString(); 

                if (!randomised)
                {
                    //Randomise the list
                    playersInGame = playersInGame.OrderBy(x => Random.value).ToList();
                    randomised = true;
                }
            }
        }
    
    }

    public void IncrementTurn()
    {
        playersInGame[PlayerIndex].GetComponent<Player>().Turn = false;
        server.GetComponent<Server>().ClientTurnChange(playersInGame[PlayerIndex].GetComponent<Player>().playerID, false);
        PlayerIndex++;
        TurnIncrement();
        playersInGame[PlayerIndex].GetComponent<Player>().Turn = true;
        server.GetComponent<Server>().ClientTurnChange(playersInGame[PlayerIndex].GetComponent<Player>().playerID, true);
    }

    public void TurnIncrement()
    {
        if (PlayerIndex >= playersInGame.Count)
        //If all players have been cycled through, the round is over
        {
            //Start a new round
            PlayerIndex = 0;
            Round++;
            AI.GetComponent<AiPower>().incrementAIPower();

            sfxManager.PlayRoundEnd();
        }
    }

    public void turn1()
    {
        index = playersInGame[0].GetComponent<Player>().playerID;
        server.GetComponent<Server>().ClientTurnChange(index, true);
    }
}
