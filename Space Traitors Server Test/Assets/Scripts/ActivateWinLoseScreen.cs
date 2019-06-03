using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWinLoseScreen : MonoBehaviour
{
    public Server server;

    private void Awake()
    {
        GetComponent<Canvas>().enabled = false;
    }

    private void Update()
    {
        if(server.InstalledComponents == 5)
        {
            GetComponent<Canvas>().enabled = true;
        }
    }
}
