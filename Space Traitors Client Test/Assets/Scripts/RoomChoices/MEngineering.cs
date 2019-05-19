using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEngineering : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    public int targetScore = 3;
    private GameObject Player;
    private bool result;


    // Start is called before the first frame update
    void Start() {

        Player = GameObject.Find("Player");

    }

    public void OnClickExitButton() {

        ChoicesCanvas.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionOneClick() {

        result = SpecChallange(Player.GetComponent<Player>().Tech, targetScore);

        if (result == false) {

            Player.GetComponent<Player>().Corruption += 15;
        }
        else {
            Player.GetComponent<Player>().scrapTotal += 12;
         
        }
        ChoicesCanvas.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionTwoClick() {


        Player.GetComponent<Player>().Components += 1;
        Player.GetComponent<Player>().Corruption += 20;
        ChoicesCanvas.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;


    }

    private bool SpecChallange(int playerScore, int targetScore) {

        float successPercent;

        successPercent = Mathf.Round(50 + (playerScore - targetScore) * 50 / targetScore);

        if (Random.Range(0, 100) > successPercent) {

            return false;
        }
        else {
            return true;
        }


    }
}
