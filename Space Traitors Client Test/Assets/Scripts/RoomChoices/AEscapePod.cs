using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AEscapePod : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    private GameObject Player;
    private bool result;
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


        if(Player.GetComponent<Player>().Components > 0) {

            Player.GetComponent<Player>().Components -= 1;
        }
        else {
            ErrorText.enabled = true;
            ErrorText.text = "You don't have any components";
        }
        


    }

 



   
}
