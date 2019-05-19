using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBar : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    public int targetScore = 4;
    public GameObject OptionTwoButton;
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

        Player.GetComponent<Player>().scrapTotal += 2;
        ChoicesCanvas.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;

    }

    public void OnOptionTwoClick() {

        result = SpecChallange(Player.GetComponent<Player>().Tech, targetScore);

        if (result == false) {

            Player.GetComponent<Player>().Corruption += 20;
        }
        else {
            Player.GetComponent<Player>().Charm += 2;
            Player.GetComponent<Player>().Skill += 1;
        }
        ChoicesCanvas.enabled = false;
        Destroy(OptionTwoButton);
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
