using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Net_OnCreateAccount : NetMessage
{
   public Net_OnCreateAccount()
    {
        OperationCode = NetOP.ChangeRoom;
    }

    public string Username { set; get; }
    public string Password { set; get; }
    public string Email { set; get; }
}
