using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class IPManager
{
    public static string GetIP(ADDRESSFAM Addfam)
    {
        //Return null if ADDRESSFAM is Ipv6 but Os does not support it
        if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }

        string output = "";

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif 
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    //IPv4
                    if (Addfam == ADDRESSFAM.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }

                    //IPv6
                    else if (Addfam == ADDRESSFAM.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }
}

public enum ADDRESSFAM
{
    IPv4, IPv6
}

[System.Serializable]
public class Client : MonoBehaviour {
    public static Client Instance { get; set; }

    private byte reliableChannel;
    private byte error;
    private int hostID;
    private int connectionID;

    private const int maxUser = 100;
    private const int port = 26000;
    private const int webPort = 26001;
    private const int byteSize = 1024;
    //private const string serverIP = "100.104.80.220";
    public string serverIP = IPManager.GetIP(ADDRESSFAM.IPv4);
    private bool isStarted = false;

    private GameObject player;
    public GameObject connectButton;
    public GameObject loadingScreen;
    private Scene scene;
    private string sceneName;

    public enum WinLossConditions {

        InnocentsWin,
        Eliminated,
        TraitorsWin
    }

    // Use this for initialization
    void Start() {
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void Initialise() {
        NetworkTransport.Init();

        ConnectionConfig config = new ConnectionConfig();
        reliableChannel = config.AddChannel(QosType.Reliable);

        HostTopology topo = new HostTopology(config, maxUser);

        //Client only code
        hostID = NetworkTransport.AddHost(topo, 0);

#if !UNITY_WEBGL && UNITY_EDITOR
        //Standalone Client
        Debug.Log(serverIP);
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

    public void ShutDown() {
        isStarted = false;
        NetworkTransport.Shutdown();
    }

    // Update is called once per frame
    void Update() {
        player = GameObject.FindGameObjectWithTag("Player");
        UpdateMessagePump();
    }

    private void UpdateMessagePump() {
        if (!isStarted) {
            return;
        }
        int recHostID;  //checks whether this is from Web or standalone
        int connectionID; //checks which user is sending info
        int channelID;  //checks lane infor is being sent from

        byte[] recBuffer = new byte[byteSize];
        int dataSize;

        NetworkEventType type = NetworkTransport.Receive(out recHostID, out connectionID, out channelID, recBuffer, byteSize, out dataSize, out error);
        switch (type) {
            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                Debug.Log(string.Format("Connected to server"));
                //Disable the connect button so player can't have multiple instances
                    connectButton.SetActive(false);
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

    private void OnData(int conID, int chanID, int rHostID, NetMessage msg) {
        switch (msg.OperationCode) {
            case NetOP.None:
                Debug.Log("Unexpected NETOP");
                break;
            case NetOP.ChangeRoom:
                ChangeRoom(conID, chanID, rHostID, (Net_ChangeRoom)msg);
                break;
            case NetOP.SendPoints:
                SendPoints(conID, chanID, rHostID, (Net_SendPoints)msg);
                break;
            case NetOP.SendTurnEnd:
                SendTurnEnd(conID, chanID, rHostID, (Net_SendTurnEnd)msg);
                break;
            case NetOP.SendRoomCost:
                RoomCost(conID, chanID, rHostID, (Net_SendCostOfRoom)msg);
                break;
            case NetOP.SendWinLoss:
                WinLossConditonMet(conID, chanID, rHostID, (Net_SendWinLoss)msg);
                break;
            case NetOP.AssignTraitor:
                AssignTraitor(conID, chanID, rHostID, (Net_AssignTraitor)msg);
                break;
            case NetOP.AllowMovement:
                AllowMovement(conID, chanID, rHostID, (NetAllowMovement)msg);
                break;
        }
        //Debug.Log("Recieved a message of type " + msg.OperationCode);

    }

    private void RoomCost (int conID, int chanID, int rHostID, Net_SendCostOfRoom costOfRoom) {


        player.GetComponent<Player>().ActionPointCost = costOfRoom.RoomCost;
       
    }

    private void WinLossConditonMet(int conID, int chanID, int rHostID, Net_SendWinLoss WinOrLossCondition) {

        player.GetComponent<Player>().WinLossCanvas.enabled = true;


        if (WinOrLossCondition.WinOrLossCondition == (int)WinLossConditions.InnocentsWin) {

            player.GetComponent<Player>().WinLossText.text = "You Successfuly escaped. You Win";
        }

        else if (WinOrLossCondition.WinOrLossCondition == (int)WinLossConditions.TraitorsWin) {

            player.GetComponent<Player>().WinLossText.text = "You were eliminated by a corrupted robot. You Lose";
        }
         

        else if (WinOrLossCondition.WinOrLossCondition == (int)WinLossConditions.Eliminated) {
            player.GetComponent<Player>().WinLossText.text = "You managed to destory the remaining robots. You Win";

        }

    }

    private void AllowMovement(int conID, int chanID, int rHostID, NetAllowMovement allowMovement) {

        if(allowMovement.AllowToMove == true) {

            player.GetComponent<Player>().allowMovement = true;

        }
        else {

            player.GetComponent<Player>().allowMovement = false;

        }

    }


        public void SendServer(NetMessage msg) {
        //This is where data is held
        byte[] buffer = new byte[byteSize];

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);

        Debug.Log("sent");
        NetworkTransport.Send(hostID, connectionID, reliableChannel, buffer, byteSize, out error);

    }

    //Sends the location to the server, references the get,set from Net_Change Room
    public void ChangeLocation(int location) {
        Net_ChangeRoom ca = new Net_ChangeRoom();

        ca.Location = location;

        SendServer(ca);

    }

    public void SendPoints(string var) {
        Net_SendPoints lr = new Net_SendPoints();

        lr.Influence = var;
        SendServer(lr);
    }

    public void SendTurnEnd(bool var) {
        Net_SendTurnEnd te = new Net_SendTurnEnd();

        te.Ended = var;
        SendServer(te);
    }

    private void ChangeRoom(int conID, int chanID, int rHostID, Net_ChangeRoom ca) {
        //on Client side, ChangeRoom changes to the next scene (saves on having to create new net classes)
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if (sceneName == "Character Select")
        {
            loadingScreen.GetComponent<LoadingScreen>().LoadImage();
        }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void SendPoints(int conID, int chanID, int rHostID, Net_SendPoints ca) {

    }

    private void SendTurnEnd(int conID, int chanID, int rHostID, Net_SendTurnEnd ca) {
        if (ca.Ended == true) {
            player.GetComponent<Player>().Turn = true;
        }

    }
    private void AssignTraitor(int conID, int chanID, int rHostID, Net_AssignTraitor ca)
    {
        player.GetComponent<Player>().traitor = true;
    }

    public void SendScrap(int var) {

        Net_SendScrap scrap = new Net_SendScrap();

        scrap.ScrapTotal = var;
        SendServer(scrap);

    }

    public void SendComponents(int var , bool Installed = false) {

        Net_SendComponents Component = new Net_SendComponents();

        Component.ComponentNumber = var;
        Component.Installed = Installed;
        SendServer(Component);

    }

    public void SendAIPower(int var) {

        Net_SendAiPower aiPower = new Net_SendAiPower();

        aiPower.AIpowerAmountGained = var;
        SendServer(aiPower);

       

    }

    public void SendRoomNumber (int var) {


        Net_SendRoomNumber roomNumber = new Net_SendRoomNumber();

        roomNumber.Room = var;
        SendServer(roomNumber);

        Debug.Log(roomNumber.Room);


    }


}

 