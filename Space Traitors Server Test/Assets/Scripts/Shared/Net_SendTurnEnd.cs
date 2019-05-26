using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_SendTurnEnd : NetMessage
{
    public Net_SendTurnEnd()
    {
        OperationCode = NetOP.SendTurnEnd;
    }

    public bool Ended { set; get; }
}
