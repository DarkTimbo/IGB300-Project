using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_SendWinLoss : NetMessage {

    public Net_SendWinLoss() {
        OperationCode = NetOP.SendWinLoss;
    }

    public int WinOrLossCondition{ set; get; }

}
