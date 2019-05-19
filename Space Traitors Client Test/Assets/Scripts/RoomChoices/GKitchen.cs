using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GKitchen : MonoBehaviour {

    public Canvas ChoicesCanvas;
    public int targetScore = 3;
    private GameObject Player;
    public Text ErrorText;
    private bool result;


    // Start is called before the first frame update
    void Start() {

        Player = GameObject.Find("Player");

    }

    public void OnClickExitButton() {

        ChoicesCanvas.enabled = false;
        ErrorText.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionOneClick() {

        result = SpecChallange(Player.GetComponent<Player>().Brawn, targetScore);

        if (result == false) {

            Player.GetComponent<Player>().Corruption += 15;
        }
        else {
            Player.GetComponent<Player>().scrapTotal += 12;
        }
        ChoicesCanvas.enabled = false;
        ErrorText.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionTwoClick() {


        if (Player.GetComponent<Player>().scrapTotal >= 10) {

            if (Player.GetComponent<Player>().LifePoints < 2) {
                Player.GetComponent<Player>().scrapTotal -= 10;
                Player.GetComponent<Player>().LifePoints += 1;
                ChoicesCanvas.enabled = false;
                ErrorText.enabled = false;
                Player.GetComponent<Player>().isInSelction = false;
            }
            else {
                ErrorText.enabled = true;
                ErrorText.text = "Already at max health";
            }

        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "Not Enough Scrap";

        }

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
