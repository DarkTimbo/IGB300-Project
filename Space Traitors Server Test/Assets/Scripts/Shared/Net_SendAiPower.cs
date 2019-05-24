using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_SendAiPower: NetMessage {
    public Net_SendAiPower() {

        OperationCode = NetOP.SendAIPower;

    }
    public int AIpowerAmountGained { set; get; }
}
