using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessCanvas : MonoBehaviour
{

    public Canvas SuccessFailCanvas;
    public Image BackGroundColor;
    public Text Text;
    private GameObject Player;

    void Start() {

        Player = GameObject.Find("Player");

    }

    public void OnClickExitButton() {

        SuccessFailCanvas.enabled = false;
        Player.GetComponent<Player>().isInSelction = false;
    }


}
