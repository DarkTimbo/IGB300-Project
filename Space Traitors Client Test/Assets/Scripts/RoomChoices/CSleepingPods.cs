using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSleepingPods : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    public Button OptionOneButton;
    private GameObject Player;
    public Text ErrorText;

    public GameObject sfxSource;
    private SFXManager sfxManager;

    // Start is called before the first frame update
    void Start() {

        Player = GameObject.Find("Player");

        sfxManager = sfxSource.GetComponent<SFXManager>();
    }

    public void OnClickExitButton() {

        ChoicesCanvas.enabled = false;
        ErrorText.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionOneClick() {

        if (Player.GetComponent<Player>().ChoiceMade == false) {
            if (Player.GetComponent<Player>().Components == 0) {
                Player.GetComponent<Player>().Components += 1;
                Player.GetComponent<Player>().Corruption += 20;
                ChoicesCanvas.enabled = false;
                ErrorText.enabled = false;
                Destroy(OptionOneButton);
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

   

}
