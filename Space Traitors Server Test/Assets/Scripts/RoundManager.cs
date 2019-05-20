using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public List<GameObject> playersInGame = new List<GameObject>();
    public GameObject server;
    public GameObject playerObject;
    private int PlayerIndex;
    private int Round;
    private bool setup = false, randomised = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        server = GameObject.FindGameObjectWithTag("Server");
    }

    // Update is called once per frame
    void Update()
    {
        //Allow a line for the list to be filled up with players before acting
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
            
        }
    }

    public void IncrementTurn()
    {
        playersInGame[PlayerIndex].GetComponent<Player>().Turn = false;
        PlayerIndex++;
        playersInGame[PlayerIndex].GetComponent<Player>().Turn = true;
        playersInGame[PlayerIndex].GetComponent<Player>().SetPlayerTurn();
    }
}
