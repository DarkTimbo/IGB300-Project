using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour { 

    public int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Move() {

        

            transform.position += Vector3.right * speed * Time.deltaTime;

      
       
    }
}
