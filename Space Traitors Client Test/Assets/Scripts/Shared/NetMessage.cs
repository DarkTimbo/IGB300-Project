﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetOP
{
    public const int None = 0;
    
    public const int ChangeRoom = 1;
    public const int SendPoints = 2;
    public const int OnChangeRoom = 3;
    public const int OnLoginRequest = 4;

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

