using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_SendPoints : NetMessage
{
    public Net_SendPoints()
    {
        OperationCode = NetOP.SendPoints;
    }

    public string Influence { set; get; }

}
