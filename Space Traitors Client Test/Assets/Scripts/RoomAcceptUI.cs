using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomAcceptUI : MonoBehaviour
{
    public Canvas RoomAcceptCanvas;
    private int RoomNumber;
    private GameObject ManagerObject;

    // Start is called before the first frame update
    void Start() {
        ManagerObject = GameObject.Find("Manager");
        RoomAcceptCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        RoomNumber = ManagerObject.GetComponent<Manager>().MovingToRoomNumber;
        
    }

   public void YesButtonPress() {

        Client.Instance.SendLocation(RoomNumber);
        RoomAcceptCanvas.enabled = false;


    }

    public void NoButtonPress() {

        RoomAcceptCanvas.enabled = false;


    }
}
