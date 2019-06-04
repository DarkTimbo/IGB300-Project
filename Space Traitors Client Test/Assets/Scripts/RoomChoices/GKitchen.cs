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
    private GameObject SuccessFailCanvas;

    public GameObject sfxSource;
    private SFXManager sfxManager;

    // Start is called before the first frame update
    void Start() {

        Player = GameObject.Find("Player");
        SuccessFailCanvas = GameObject.Find("Success / Fail Canvas");

        sfxManager = sfxSource.GetComponent<SFXManager>();
    }

    public void OnClickExitButton() {

        ChoicesCanvas.enabled = false;
        ErrorText.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionOneClick() {
        if (Player.GetComponent<Player>().ChoiceMade == false) {
            result = SpecChallange(Player.GetComponent<Player>().Brawn, targetScore);
            SuccessFailCanvas.GetComponent<SuccessCanvas>().SuccessFailCanvas.enabled = true;

            if (result == false) {

                Player.GetComponent<Player>().Corruption += 15;
                SuccessFailCanvas.GetComponent<SuccessCanvas>().Text.text = "Failed";
                SuccessFailCanvas.GetComponent<SuccessCanvas>().BackGroundColor.color = Color.red;
                sfxManager.PlaySpecChallengeFail();
            }
            else {
                Player.GetComponent<Player>().scrapTotal += 12;
                SuccessFailCanvas.GetComponent<SuccessCanvas>().Text.text = "Success";
                SuccessFailCanvas.GetComponent<SuccessCanvas>().BackGroundColor.color = Color.green;
                sfxManager.PlaySpecChallengeSuccess();
            }
            ChoicesCanvas.enabled = false;
            ErrorText.enabled = false;
            Player.GetComponent<Player>().isInSelction = false;
            Player.GetComponent<Player>().ChoiceMade = true;
        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "You can only make one choice per round.";
            sfxManager.PlayFailedChoice();
        }
    }

    public void OnOptionTwoClick() {

        if (Player.GetComponent<Player>().ChoiceMade == false) {
            if (Player.GetComponent<Player>().scrapTotal >= 10) {

                if (Player.GetComponent<Player>().LifePoints < 3) {
                    Player.GetComponent<Player>().scrapTotal -= 10;
                    Player.GetComponent<Player>().LifePoints += 1;
                    ChoicesCanvas.enabled = false;
                    ErrorText.enabled = false;
                    Player.GetComponent<Player>().isInSelction = false;
                    Player.GetComponent<Player>().ChoiceMade = true;
                }
                else {
                    ErrorText.enabled = true;
                    ErrorText.text = "Already at max health";
                    sfxManager.PlayFailedChoice();
                }

            }
            else {
                ErrorText.enabled = true;
                ErrorText.text = "Not Enough Scrap";
                sfxManager.PlayFailedChoice();

            }
        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "You can only make one choice per round.";
            sfxManager.PlayFailedChoice();
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
