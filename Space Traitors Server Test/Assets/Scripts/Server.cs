using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    private byte reliableChannel;
    private int hostID;
    private int webHostID;

    private const int maxUser = 100;
    private const int port = 26000;
    private const int webPort = 26001;
    private const int byteSize = 1024;

    private bool isStarted = false;
    private byte error;

    public AudioSource connectSound;
    public GameObject[] players = new GameObject[4];

    // Use this for initialization
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        Initialise();
    }

    public void Initialise()
    {
        NetworkTransport.Init();

        ConnectionConfig config = new ConnectionConfig();
        reliableChannel = config.AddChannel(QosType.Reliable);


        HostTopology topo = new HostTopology(config, maxUser);

        //Server only code
        hostID = NetworkTransport.AddHost(topo, port, null);
        webHostID = NetworkTransport.AddWebsocketHost(topo, port, null);

        Debug.Log(string.Format("Opening connection on port {0} and webport {1}", port, webPort));
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

            //When user connects to game
            case NetworkEventType.ConnectEvent:
                connectSound.Play();
                //Loop through to find a player not already connected, and assign them their ID
                foreach (GameObject player in players)
                {
                    if (!player.GetComponent<PlayerConnect>().connected)
                    {
                        player.GetComponent<PlayerConnect>().connected = true;
                        player.GetComponent<PlayerConnect>().playerID = connectionID;
                        player.GetComponent<PlayerConnect>().playerImage.enabled = true;
                        Debug.Log(player.name + " has connected through host " + recHostID);
                        break;
                    }
                }
                break;

            //When user disconnects from game
            case NetworkEventType.DisconnectEvent:
                //Loop through to find player that is disconnecting, based on their ID

                foreach (GameObject player in players)
                {
                    if (player.GetComponent<PlayerConnect>().playerID == connectionID)
                    {
                        //Reset player variables
                        player.GetComponent<PlayerConnect>().connected = false;
                        player.GetComponent<PlayerConnect>().playerID = 0;
                        player.GetComponent<PlayerConnect>().playerImage.enabled = false;
                        player.GetComponent<PlayerConnect>().influence = 0;
                        //Reset inventory
                        foreach (GameObject inv in player.GetComponent<PlayerConnect>().inventory)
                        {
                            player.GetComponent<PlayerConnect>().inventory = null;
                        }

                        Debug.Log(player.name + " has disconnected");
                        break;
                    }
                }
                Debug.Log(connectionID + " has disconnected");
                break;

            case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer);
                NetMessage msg = (NetMessage)formatter.Deserialize(ms);

                OnData(connectionID, channelID, recHostID, msg);
                break;

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
            case NetOP.ChangeRoom:
                ChangeRoom(conID, chanID, rHostID, (Net_ChangeRoom)msg);
                break;
            case NetOP.SendPoints:
                SendPoints(conID, chanID, rHostID, (Net_SendPoints)msg);
                break;
        }
        //Debug.Log("Recieved a message of type " + msg.OperationCode);

    }

    private void ChangeRoom(int conID, int chanID, int rHostID, Net_ChangeRoom ca)
    {
        foreach (GameObject player in players)
        {
            //Find the correct player
            if (player.GetComponent<PlayerConnect>().playerID == conID)
            {
                //Room Movement action here

                Debug.Log(player.name + " is in " + ca.Location);
                break;
            }
        }
    }

    private void SendPoints(int conID, int chanID, int rHostID, Net_SendPoints lr)
    {
        foreach (GameObject player in players)
        {
            //Find the correct player
            if (player.GetComponent<PlayerConnect>().playerID == conID)
            {
                //Influence (/variable) handling here
                player.GetComponent<PlayerConnect>().influence += lr.Influence;
                Debug.Log(player.name + " has " + player.GetComponent<PlayerConnect>().influence + " influence");
                break;
            }
        }
    }

    public void SendClient(int recHost, int conID, NetMessage msg)
    {
        //This is where data is held
        byte[] buffer = new byte[byteSize];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);

        if (recHost == 0)
        {
            NetworkTransport.Send(hostID, conID, reliableChannel, buffer, byteSize, out error);
        }
        else
        {
            NetworkTransport.Send(webHostID, conID, reliableChannel, buffer, byteSize, out error);
        }

    }

    public void roomLocation(Net_ChangeRoom ca)
    {




    }


}
