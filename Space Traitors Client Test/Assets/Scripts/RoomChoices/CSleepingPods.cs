using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSleepingPods : MonoBehaviour
{
    public Canvas ChoicesCanvas;
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


        Player.GetComponent<Player>().Components += 1;
        Player.GetComponent<Player>().Corruption += 20;
        ChoicesCanvas.enabled = false;
        ErrorText.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;


    }

    public void OnOptionTwoClick() {

        if (Player.GetComponent<Player>().scrapTotal >= 10){

            if(Player.GetComponent<Player>().LifePoints < 2) {
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
