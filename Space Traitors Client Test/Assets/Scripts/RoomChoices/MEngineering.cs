using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MEngineering : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    public int targetScore = 3;
    public Button OptionTwoButton;
    public Text ErrorText;
    private GameObject Player;
    private bool result;
    private GameObject SuccessFailCanvas;


    // Start is called before the first frame update
    void Start() {

        Player = GameObject.Find("Player");
        SuccessFailCanvas = GameObject.Find("Success / Fail Canvas");

    }

    public void OnClickExitButton() {

        ChoicesCanvas.enabled = false;
        ErrorText.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionOneClick() {
        if (Player.GetComponent<Player>().ChoiceMade == false) {
            result = SpecChallange(Player.GetComponent<Player>().Tech, targetScore);

            if (result == false) {

                Player.GetComponent<Player>().Corruption += 15;
                SuccessFailCanvas.GetComponent<SuccessCanvas>().Text.text = "Failed";
                SuccessFailCanvas.GetComponent<SuccessCanvas>().BackGroundColor.color = Color.red;
            }
            else {
                Player.GetComponent<Player>().scrapTotal += 12;
                SuccessFailCanvas.GetComponent<SuccessCanvas>().Text.text = "Success";
                SuccessFailCanvas.GetComponent<SuccessCanvas>().BackGroundColor.color = Color.green;

            }
            ChoicesCanvas.enabled = false;
            ErrorText.enabled = false;
            Player.GetComponent<Player>().isInSelction = false;
            Player.GetComponent<Player>().ChoiceMade = true;
        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "You can only make one choice per round.";
        }
    }

    public void OnOptionTwoClick() {

        if (Player.GetComponent<Player>().ChoiceMade == false) {
            if (Player.GetComponent<Player>().Components == 0) {
                Player.GetComponent<Player>().Components += 1;
                Player.GetComponent<Player>().Corruption += 20;
                ChoicesCanvas.enabled = false;
                ErrorText.enabled = false;
                Destroy(OptionTwoButton);
                Player.GetComponent<Player>().isInSelction = false;
                Player.GetComponent<Player>().ChoiceMade = true;

            }
            else {
                ErrorText.enabled = true;
                ErrorText.text = "You can only have One component at a time";

            }
        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "You can only make one choice per round.";
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
