using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetOP
{
    public const int None = 0;
    
    public const int ChangeRoom = 1;
    public const int SendPoints = 2;
    public const int ChangeCharacter = 3;
    public const int OnChangeRoom = 4;
    public const int OnLoginRequest = 5;
    public const int OnChangeCharacter = 6;
    public const int SendTurnEnd = 7;
    public const int OnSendTurnEnd = 8;
    public const int SendScrap = 9;
    public const int SendComponents = 10;
    public const int SendAIPower = 11;
    public const int SendRoomCost = 12;
    public const int RoomNumber = 13;
    public const int SendWinLoss = 14;
    public const int AssignTraitor = 15;
    public const int AllowMovement = 16;
}

[System.Serializable]
public abstract class NetMessage
{
    public byte OperationCode { set; get; }
    
    public NetMessage()
    {
        OperationCode = NetOP.None;
    }
}

