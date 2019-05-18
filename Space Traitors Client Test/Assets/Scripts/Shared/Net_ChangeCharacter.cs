[System.Serializable]
public class Net_ChangeCharacter : NetMessage
{
    public Net_ChangeCharacter()
    {
        OperationCode = NetOP.ChangeCharacter;
    }

    public string Character { set; get; }
}