using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int CurrentRoomNumber;
    public int scrapTotal = 0;
    public bool isInSelction = false;

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

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        ClickOnRoom();

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

    public void AcceptButtonClick() {

        AcceptRoomCanvas.enabled = false;
        RoomSelected.GetComponent<Rooms>().RoomChoices.enabled = true;
        
        //Client.Instance.SendLocation(RoomSelected.GetComponent<Rooms>().RoomNumber);


    }
    public void DenyButtonClick() {

        AcceptRoomCanvas.enabled = false;
        isInSelction = false;
    }
}
