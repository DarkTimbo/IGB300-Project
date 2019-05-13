using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomClick: MonoBehaviour
{

    public int RoomNumber;
    private GameObject RoomAcceptCanvas;
    private int EnergyCost;
    


    // Start is called before the first frame update
    void Start()
    {
        RoomAcceptCanvas = GameObject.Find("Room Accept Button");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {

                if(hit.transform.position == this.transform.position) {

                    Debug.Log("Button Pressed");
                    RoomAcceptCanvas.GetComponent<Canvas>().enabled = true;

                }
            }
        }

        
    }
}
