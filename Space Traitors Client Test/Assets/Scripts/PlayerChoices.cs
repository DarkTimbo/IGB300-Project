using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoices : MonoBehaviour
{
    public Canvas ChoicesCanvas;
    public Text OptionOneText;
    public Text OptionTwoText;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnClickExitButton() {

        ChoicesCanvas.enabled = false;

    }

    public void OnOptionOneClick() {




    }

    public void OnOptionTwoClick() {




    }
}
