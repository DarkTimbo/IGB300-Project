using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWinLoseScreen : MonoBehaviour
{
    private GameObject server;

    private void Awake()
    {
        GetComponent<Canvas>().enabled = false;
        server = GameObject.FindGameObjectWithTag("Server");
    }

    private void Update()
    {
        if (server.GetComponent<Server>().InstalledComponents == 5)
        {
            GetComponent<Canvas>().enabled = true;
            transform.SetAsLastSibling();
        }
    }
}
