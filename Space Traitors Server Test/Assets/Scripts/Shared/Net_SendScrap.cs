using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_SendScrap : NetMessage
{
    public Net_SendScrap() {
        OperationCode = NetOP.SendScrap;

    }

    public int ScrapTotal { set; get; }
}
