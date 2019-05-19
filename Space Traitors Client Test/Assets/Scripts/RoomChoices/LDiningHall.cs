using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LDiningHall : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    private GameObject Player;
    public GameObject OptionOneButton;
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


        if (Player.GetComponent<Player>().scrapTotal >= 2) {


            Player.GetComponent<Player>().scrapTotal -= 2;
            Player.GetComponent<Player>().Charm += 1;
            ChoicesCanvas.enabled = false;
            Destroy(OptionOneButton);
            ErrorText.enabled = false;
            Player.GetComponent<Player>().isInSelction = false;

        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "Not Enough Scrap";

        }

       

    }

    public void OnOptionTwoClick() {

        Player.GetComponent<Player>().Components += 1;
        Player.GetComponent<Player>().Corruption += 20;
        ChoicesCanvas.enabled = false;
        ErrorText.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }
}
