using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_SendRoomNumber : NetMessage {

    public Net_SendRoomNumber() {
        OperationCode = NetOP.RoomNumber;
    }
    public int Room { set; get; }

}

