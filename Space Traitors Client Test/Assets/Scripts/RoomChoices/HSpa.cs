using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSpa : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    public int targetScore = 5;
    public GameObject OptionOneButton;
    public GameObject OptionTwoButton;
    public Text ErrorText;
    private GameObject Player;
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
        Player.GetComponent<Player>().isInSelction = false;
        ErrorText.enabled = false;
    }

    public void OnOptionOneClick() {

        if (Player.GetComponent<Player>().ChoiceMade == false) {
            result = SpecChallange(Player.GetComponent<Player>().Brawn, targetScore);
            SuccessFailCanvas.GetComponent<SuccessCanvas>().SuccessFailCanvas.enabled = true;

            if (result == false) {

                Player.GetComponent<Player>().AIPower += 10;
                SuccessFailCanvas.GetComponent<SuccessCanvas>().Text.text = "Failed";
                SuccessFailCanvas.GetComponent<SuccessCanvas>().BackGroundColor.color = Color.red;
                sfxManager.PlaySpecChallengeFail();
            }
            else {
                Player.GetComponent<Player>().Brawn += 2;
                Player.GetComponent<Player>().Tech += 1;
                SuccessFailCanvas.GetComponent<SuccessCanvas>().Text.text = "Success";
                SuccessFailCanvas.GetComponent<SuccessCanvas>().BackGroundColor.color = Color.green;
                sfxManager.PlaySpecChallengeSuccess();
            }
            ChoicesCanvas.enabled = false;
            Destroy(OptionOneButton);
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
