﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int CurrentRoomNumber;
    public int scrapTotal = 0;
    public bool isInSelction = false;

    private int ActionPoints = 0;
    public bool Turn = false;
    private bool TurnStarted = true;
    public int rollMin = 1, rollMax = 11;

    public int LifePoints = 2;
    public int Brawn = 0;
    public int Skill = 0;
    public int Tech = 0;
    public int Charm = 0;
    public int Components = 0;
    public int Corruption = 0;
    public int AIPower = 0;

    public Canvas AcceptRoomCanvas;
    public Text RoomNameText;
    public GameObject RoomSelected;
    public GameObject EndTurnButton;
    public GameObject rooms;
    private GameObject lobbyScene;

    // Start is called before the first frame update
    void Start() {
        EndTurnButton = GameObject.FindGameObjectWithTag("End");
        lobbyScene = GameObject.FindGameObjectWithTag("LobbyScene");
        Turn = false;

  
    }

    // Update is called once per frame
    void Update() {
        if (Turn)
        {
            rooms.SetActive(true);

            if (TurnStarted)
            {
                //Set action points
                ActionPointsRoll();
                EndTurnButton.SetActive(true);
                TurnStarted = false;
            }

            ClickOnRoom();

            if (ActionPoints <= 0)
            {
                EndTurn();
            }
        }
        else
        {
            //Player can't select rooms when it is not their turn
            rooms.SetActive(false);
        }

    }


    private void ClickOnRoom() {

        if (Input.GetMouseButtonDown(0)) {

            if (GameObject.Find("Player").GetComponent<Player>().isInSelction == false) {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {

                    if (hit.transform.tag == "Room") {

                        RoomSelected = hit.transform.gameObject;
                        RoomNameText.text = ("Do you want to move to " + RoomSelected.name + "?");
                        AcceptRoomCanvas.enabled= true;
                        isInSelction = true;

                    }
                }
            }
        }
    }

    private int ActionPointsRoll()
    {
        ActionPoints = Random.Range(rollMin, rollMax);
        return ActionPoints;
    }

    public void EndTurn()
    {
        //If action points are not emptied, remove them
        ActionPoints = 0;
        //Reinitialise variables for next turn
        Turn = false;
        TurnStarted = true;
        EndTurnButton.SetActive(false);
        //Send a notification to the server to let them know the player's turn has ended
        lobbyScene.GetComponent<LobbyScene>().OnSendTurnEnd();
    }

    public void AcceptButtonClick() {

        AcceptRoomCanvas.enabled = false;
        RoomSelected.GetComponent<Rooms>().RoomChoices.enabled = true;

        Client.Instance.SendLocation(RoomSelected.GetComponent<Rooms>().RoomNumber);


    }
    public void DenyButtonClick() {

        AcceptRoomCanvas.enabled = false;
        isInSelction = false;
    }
}
