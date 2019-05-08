using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Client : MonoBehaviour
{
    public static Client Instance { get; set; }

    private byte reliableChannel;
    private byte error;
    private int hostID;
    private int connectionID;


    private const int maxUser = 100;
    private const int port = 26000;
    private const int webPort = 26001;
    private const int byteSize = 1024;
    private const string serverIP = "192.168.1.65";

    private bool isStarted = false;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Initialise();
    }

    public void Initialise()
    {
        NetworkTransport.Init();

        ConnectionConfig config = new ConnectionConfig();
        reliableChannel = config.AddChannel(QosType.Reliable);

        HostTopology topo = new HostTopology(config, maxUser);

        //Client only code
        hostID = NetworkTransport.AddHost(topo, 0);

#if !UNITY_WEBGL && UNITY_EDITOR
        //Standalone Client
        connectionID = NetworkTransport.Connect(hostID, serverIP, port, 0, out error);
        Debug.Log(string.Format("Connecting from standalone"));
#else
        //Web Client
        connectionID = NetworkTransport.Connect(hostID, serverIP, port, 0, out error);
        Debug.Log(string.Format("Connecting from Web"));
#endif

        Debug.Log(string.Format("Attempting to connect on {0}...", serverIP));
        isStarted = true;
    }

    public void ShutDown()
    {
        isStarted = false;
        NetworkTransport.Shutdown();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateMessagePump();
    }

    private void UpdateMessagePump()
    {
        if (!isStarted)
        {
            return;
        }
        int recHostID;  //checks whether this is from Web or standalone
        int connectionID; //checks which user is sending info
        int channelID;  //checks lane infor is being sent from

        byte[] recBuffer = new byte[byteSize];
        int dataSize;

        NetworkEventType type = NetworkTransport.Receive(out recHostID, out connectionID, out channelID, recBuffer, byteSize, out dataSize, out error);
        switch (type)
        {
            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                Debug.Log(string.Format("Connected to server"));
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("You were disconnected"));
                break;

            case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer);
                NetMessage msg = (NetMessage)formatter.Deserialize(ms);

                OnData(connectionID, channelID, recHostID, msg);

                break;

            default:
            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected network event type");
                break;
        }

    }

    private void OnData(int conID, int chanID, int rHostID, NetMessage msg)
    {
        switch (msg.OperationCode)
        {
            case NetOP.None:
                Debug.Log("Unexpected NETOP");
                break;
        }
        //Debug.Log("Recieved a message of type " + msg.OperationCode);

    }

    public void SendServer(NetMessage msg)
    {
        //This is where data is held
        byte[] buffer = new byte[byteSize];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);

        NetworkTransport.Send(hostID, connectionID, reliableChannel, buffer, byteSize, out error);

    }

    //Sends the location to the server, references the get,set from Net_Change Room
    public void SendLocation(int location)
    {
        Net_ChangeRoom ca = new Net_ChangeRoom();

        ca.Location = location;

        SendServer(ca);
    }

    public void SendPoints(int var)
    {
        Net_SendPoints lr = new Net_SendPoints();

        lr.Influence = var;
        SendServer(lr);
    }


    //public void TESTFUNCTIONCREATEACCOUNT()
    //{
    //    Net_CreateAccount ca = new Net_CreateAccount();

    //    ca.Username = ("UpDog");
    //    ca.Password = ("Password");
    //    ca.Email = ("RandomEmail@somewhere.com");

    //    SendServer(ca);
    //}
}


 