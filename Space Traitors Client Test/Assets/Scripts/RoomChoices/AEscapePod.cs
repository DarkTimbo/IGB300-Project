using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AEscapePod : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    private GameObject Player;
    private GameObject Handler;
    private bool result;
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
            if (Player.GetComponent<Player>().Components > 0) {

                Player.GetComponent<Player>().Components -= 1;
                Player.GetComponent<Player>().Installed = true;
                Player.GetComponent<Player>().ChoiceMade = true;
            }
            else {
                ErrorText.enabled = true;
                ErrorText.text = "You don't have any components";
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
