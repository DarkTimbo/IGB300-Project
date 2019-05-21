using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFirstRound : MonoBehaviour
{
    public GameObject button;
    private GameObject round;
    // Start is called before the first frame update
    void Start()
    {
        round = GameObject.FindGameObjectWithTag("RoundManager");
    }


    public void playerinit()
    {
        button.SetActive(false);
        round.GetComponent<RoundManager>().turn1();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
