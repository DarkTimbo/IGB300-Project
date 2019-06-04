using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiPower : MonoBehaviour
{
    public GameObject server;
    public Slider AIPowerSliderUI;

    public int power = 0;
    public int increase = 17;
    public int traitorAmount = 50; //Half the max amount (100)
    private bool traitorSent = false;

    // Start is called before the first frame update
    void Start()
    {
        server = GameObject.FindGameObjectWithTag("Server");
        power = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        AIPowerSliderUI.value = power;
        if (power >= traitorAmount)
        {
            if (!traitorSent)
            {
                server.GetComponent<Server>().ChooseTraitor();
                traitorSent = true;
            }
        }
    }

    public void incrementAIPower()
    {
        power += increase;
    }


}
