using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{

    public Material[] material;
    public GameObject Player;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[Player.GetComponent<Player>().playerID - 1];
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
