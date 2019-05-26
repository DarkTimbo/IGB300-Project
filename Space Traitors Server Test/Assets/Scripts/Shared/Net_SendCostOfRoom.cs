using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_SendCostOfRoom : NetMessage
{

    public Net_SendCostOfRoom() {
        OperationCode = NetOP.SendRoomCost;
    }

    public int RoomCost { set; get; }

}
