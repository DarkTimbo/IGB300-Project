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

        if (Player.GetComponent<Player>().Components == 0) {
            Player.GetComponent<Player>().Components += 1;
            Player.GetComponent<Player>().Corruption += 20;
            ChoicesCanvas.enabled = false;
            ErrorText.enabled = false;
            Destroy(OptionOneButton);
            Player.GetComponent<Player>().isInSelction = false;

        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "You can only have One component at a time";

        }
    }

    public void OnOptionTwoClick() {

        if (Player.GetComponent<Player>().scrapTotal >= 10){

            if(Player.GetComponent<Player>().LifePoints < 3) {
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

   

}
