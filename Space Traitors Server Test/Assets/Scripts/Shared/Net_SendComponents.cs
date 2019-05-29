using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_SendComponents : NetMessage {
    public Net_SendComponents() {

        OperationCode = NetOP.SendComponents;

    }
    public int ComponentNumber { set; get; }
    public bool Installed { set; get; }
}
