using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConnect : MonoBehaviour
{
    //Player connection values
    public int playerID;
    public bool connected;
    public Image playerImage;
    public float influence;
    public GameObject[] inventory;

    // Start is called before the first frame update
    void Start()
    {
        playerImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
