using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KBar : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    private GameObject Player;
    public GameObject OptionTwoButton;
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


        Player.GetComponent<Player>().scrapTotal += 2;
        ChoicesCanvas.enabled = false;
        ErrorText.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionTwoClick() {


        if (Player.GetComponent<Player>().scrapTotal >= 2) {

           
            Player.GetComponent<Player>().scrapTotal -= 2;
            Player.GetComponent<Player>().Brawn += 1;
            ChoicesCanvas.enabled = false;
            Destroy(OptionTwoButton);
            ErrorText.enabled = false;
            Player.GetComponent<Player>().isInSelction = false;

        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "Not Enough Scrap";

        }

    }

   

    }

