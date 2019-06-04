using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentTracker : MonoBehaviour
{
    private Server server;

    private Text componentText;

    private string trackerString1;
    private string trackerString2;

    private void Awake()
    {
        componentText = gameObject.GetComponent<Text>();

        server = GameObject.FindGameObjectWithTag("Server").GetComponent<Server>();

        trackerString1 = "Find the Components and Return them to the Escape Shuttle!\nYou have installed ";
        trackerString2 = "/5 Components";
    }

    private void Update()
    {
        componentText.text = trackerString1 + server.InstalledComponents.ToString() + trackerString2;
    }
}
