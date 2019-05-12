using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public Canvas InventoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayerIconPress() {

        InventoryCanvas.enabled = true;


    }

    public void CloseInventoryUI() {

        InventoryCanvas.enabled = false;

    }
}
