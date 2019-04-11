using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
   
{
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableImage(Image picture) {

        if (!picture.enabled) {

            picture.enabled = true;

        }
        else {

            picture.enabled = false;

        }



    }
    public void enableInventory() {

        GameObject[] images = GameObject.FindGameObjectsWithTag("Inventory");


        if (images[0].GetComponent<Image>().enabled == true) {


            for (int i = 0; i < images.Length; i++) {

                images[i].GetComponent<Image>().enabled = false;

            }

        }
        else {
            for (int i = 0; i < images.Length; i++) {

                images[i].GetComponent<Image>().enabled = true;

            }

        }
        
        
       
        }

            
            
        


    }


    

