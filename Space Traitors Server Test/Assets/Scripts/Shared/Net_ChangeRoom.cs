using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_ChangeRoom : NetMessage
{
   public Net_ChangeRoom()
    {
        OperationCode = NetOP.ChangeRoom;
    }

    public int Location { set; get; }
}
