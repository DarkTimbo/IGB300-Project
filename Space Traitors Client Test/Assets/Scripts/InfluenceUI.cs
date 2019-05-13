using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceUI : MonoBehaviour
{

    public Slider InfluenceSlider;
    public int AmountOfInfluence;
    private GameObject ManagerObject;

    // Start is called before the first frame update
    void Start() {
        ManagerObject = GameObject.Find("Manager");
        InfluenceSlider.minValue = 0;
        InfluenceSlider.maxValue = 100;
    }

        // Update is called once per frame
    void Update(){

        InfluenceSlider.value = ManagerObject.GetComponent<Manager>().InfluencePoints;

        
    }

  

}
