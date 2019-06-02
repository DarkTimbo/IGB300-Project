using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetAllowMovement : NetMessage
{
    public NetAllowMovement() {

        OperationCode = NetOP.AllowMovement;

    }
   
    public bool AllowToMove { set; get; }
}
