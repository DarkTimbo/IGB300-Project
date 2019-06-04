using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int CurrentRoomNumber;
    public int scrapTotal = 0;
    public int previousScrapTotal = 0;
    public bool isInSelction = false;
    public string CharacterName;

    public int ActionPoints = 0;
    public int ActionPointCost = 0;
    public bool Turn = false;
    public bool traitor = false;
    private bool TurnStarted = true;
    public int rollMin = 1, rollMax = 4;
    public bool allowMovement = true;

    public int LifePoints = 3;
    public int Brawn = 0;
    public int Skill = 0;
    public int Tech = 0;
    public int Charm = 0;
    public int Components = 0;
    public int PreviousComponent = 0;
    public int Corruption = 0;
    public int AIPower = 0;
    public bool Installed = false;
    public bool ChoiceMade = false;

    public Canvas AcceptRoomCanvas;
    public Canvas WinLossCanvas;
    public Text WinLossText;
    public Text RoomNameText;
    public Text EnergyCost;
    public Text Energy;
    public Text Error;
    public GameObject RoomSelected;
    public GameObject EndTurnButton;
    public GameObject rooms;
    private GameObject lobbyScene;
    private GameObject controller;

    public GameObject sfxSource;
    private SFXManager sfxManager;

    public GameObject playerMarker;

    // Start is called before the first frame update
    void Start() {
        EndTurnButton = GameObject.FindGameObjectWithTag("End");
        lobbyScene = GameObject.FindGameObjectWithTag("LobbyScene");
        Turn = false; //TODO: have it so the server switches to the player's turn
        controller = GameObject.Find("Controller");
        CharacterName = controller.GetComponent<LobbyScene>().character;

        EndTurnButton.SetActive(false);
        rooms.SetActive(false);

        if (CharacterName == "Brute") {

            Brawn = 6;
            Skill = 3;
            Tech = 2;
            Charm = 4;

        }
        else if (CharacterName == "Butler") {

            Brawn = 4;
            Skill = 5;
            Tech = 3;
            Charm = 3;
        }
        else if(CharacterName == "Techie") {

            Brawn = 2;
            Skill = 2;
            Tech = 6;
            Charm = 5;
        }
        else if (CharacterName == "Singer") {

            Brawn = 2;
            Skill = 5;
            Tech = 2;
            Charm = 6;
        }
        else if (CharacterName == "Engineer") {

            Brawn = 4;
            Skill = 3;
            Tech = 5;
            Charm = 3;
        }
        else if (CharacterName == "Chef") {

            Brawn = 3;
            Skill = 6;
            Tech = 5;
            Charm = 2;
        }

        sfxManager = sfxSource.GetComponent<SFXManager>();


    }

    // Update is called once per frame
    void Update() {
        if (Turn)
        {

            if (TurnStarted)
            {
                //Set action points
                ActionPointsRoll();
                EndTurnButton.SetActive(true);
                rooms.SetActive(true);
                ChoiceMade = false;
                TurnStarted = false;
            }
            EnergyCost.text = ("Actions Point Cost: " + ActionPointCost);

            ClickOnRoom();
            Energy.text = ActionPoints.ToString();

            if (scrapTotal != previousScrapTotal) {

                previousScrapTotal = scrapTotal;
                Client.Instance.SendScrap(scrapTotal);
             
            }

            if (Components != PreviousComponent) {
                if (Installed == false) {

                    Debug.Log("HELP");

                    PreviousComponent = Components;
                    Client.Instance.SendComponents(Components);

                }
                else {
                    PreviousComponent = Components;
                    Client.Instance.SendComponents(Components, Installed);
                    Installed = false;
                }
            }


            if (AIPower > 0) {

                Client.Instance.SendAIPower(AIPower);
                AIPower = 0;

            }

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
                        Client.Instance.SendRoomNumber(RoomSelected.GetComponent<Rooms>().RoomNumber);

                        RoomNameText.text = ("Do you want to move to " + RoomSelected.name + "?");
                        AcceptRoomCanvas.enabled = true;
                        EnergyCost.enabled = true;
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

    public void EndTurn() {

        if (allowMovement == true) {
            //If action points are not emptied, remove them
            ActionPoints = 0;
            //Reinitialise variables for next turn
            Turn = false;
            TurnStarted = true;
            EndTurnButton.SetActive(false);
            //Player can't select rooms when it is not their turn
            rooms.SetActive(false);
            //Send a notification to the server to let them know the player's turn has ended
            lobbyScene.GetComponent<LobbyScene>().OnSendTurnEnd();
        }
    }

    public void AcceptButtonClick() {

        if (ActionPoints >= ActionPointCost ) {

            ActionPoints -= ActionPointCost;
            Error.enabled = false;
            AcceptRoomCanvas.enabled = false;     
            RoomSelected.GetComponent<Rooms>().RoomChoices.enabled = true;

            playerMarker.GetComponent<MarkerController>().UpdateRoomPos(RoomSelected.GetComponent<Rooms>().RoomNumber);
            Client.Instance.ChangeLocation(RoomSelected.GetComponent<Rooms>().RoomNumber);
        }
        else {

            Error.enabled = true;
            Error.text = "You dont have enough Action Points to move there";
            sfxManager.PlayFailedChoice();
        }

    }
    public void DenyButtonClick() {

        AcceptRoomCanvas.enabled = false;
        isInSelction = false;
        Error.enabled = false;
    }
}
