using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EKitchen : MonoBehaviour {
    
    public Canvas ChoicesCanvas;
    public GameObject OptionOneButton;
    public GameObject OptionTwoButton;
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

        result = SpecChallange(Player.GetComponent<Player>().Skill, targetScore);

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


        if (Player.GetComponent<Player>().scrapTotal >= 2) {
            Player.GetComponent<Player>().scrapTotal -= 2;
            Player.GetComponent<Player>().Skill += 1;
            Destroy(OptionTwoButton);
            ChoicesCanvas.enabled = false;
            Player.GetComponent<Player>().isInSelction = false;
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
