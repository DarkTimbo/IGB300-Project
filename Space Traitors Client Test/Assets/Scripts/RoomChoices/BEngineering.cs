using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BEngineering : MonoBehaviour
{

    public Canvas ChoicesCanvas;
    public GameObject OptionOneButton;
    public GameObject OptionTwoButton;
    public Text ErrorText;
    private GameObject Player;
 
   
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

        if(Player.GetComponent<Player>().scrapTotal >= 2) {
            Player.GetComponent<Player>().scrapTotal -= 2;
            Player.GetComponent<Player>().Tech += 1;
            Destroy(OptionTwoButton);
            ChoicesCanvas.enabled = false;
            Player.GetComponent<Player>().isInSelction = false;
        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "Not Enough Scrap";

        }
        
    }

    // Start is called before the first frame update
    void Start() {

        Player = GameObject.Find("Player");

    }
 

    
}
