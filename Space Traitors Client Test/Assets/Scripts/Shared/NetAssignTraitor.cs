using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_AssignTraitor : NetMessage
{
    public Net_AssignTraitor()
    {
        OperationCode = NetOP.AssignTraitor;
    }

    public bool Ended { set; get; }
}

