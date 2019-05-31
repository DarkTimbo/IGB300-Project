using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteAnimations : MonoBehaviour
{
    private Animator anim;
    private Player thisPlayer;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        thisPlayer = GetComponent<Player>();
    
        
    }

    // Update is called once per frame
    void Update()
    {
        if(thisPlayer.isMoving == true) {

            anim.SetBool("IsWalking", true);

        }
        else {
            anim.SetBool("IsWalking", false);
        }
      
        
    }
}
