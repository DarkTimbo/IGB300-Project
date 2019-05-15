using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientSpawner : MonoBehaviour
{
    public string customServerIP;
    public GameObject client;
    public InputField ipBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectClick()
    {
        //Change ipv4 based on what the user inputs as a room code. If left blank, default is the user's current ipb4 address
        if (ipBox.text != "")
        {
            customServerIP = ipBox.text;
            client.GetComponent<Client>().serverIP = customServerIP;
        }
        
        client.GetComponent<Client>().Initialise();


    }
}
