using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FDining : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    public GameObject OptionOneButton;
    public GameObject OptionTwoButton;
    public int targetScore = 5;
    private GameObject Player;
    private bool result;


    // Start is called before the first frame update
    void Start() {

        Player = GameObject.Find("Player");

    }

    public void OnClickExitButton() {

        ChoicesCanvas.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionOneClick() {

        result = SpecChallange(Player.GetComponent<Player>().Skill, targetScore);

        if (result == false) {

            Player.GetComponent<Player>().AIPower += 10;
        }
        else {
            Player.GetComponent<Player>().Skill += 2;
            Player.GetComponent<Player>().Brawn += 1;
        }
        ChoicesCanvas.enabled = false;
        Destroy(OptionOneButton);
        Player.GetComponent<Player>().isInSelction = false;

        


    }

    public void OnOptionTwoClick() {

        Player.GetComponent<Player>().Components += 1;
        Player.GetComponent<Player>().Corruption += 20;
        ChoicesCanvas.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

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
