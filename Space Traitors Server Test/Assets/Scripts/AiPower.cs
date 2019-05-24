using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiPower : MonoBehaviour
{

    public int AIPower = 0;
    public Slider AIPowerSliderUI;


    // Start is called before the first frame update
    void Start()
    {
        AIPower = 0;
        AIPowerSliderUI.value = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        AIPowerSliderUI.value = AIPower;

    }
}
