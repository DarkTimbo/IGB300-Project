using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour {
    private byte reliableChannel;
    private int hostId;
    private byte error;

    private bool isStarted;


    private const int MAX_USER = 6;
    private const int PORT = 7777;
    private const int WEB_PORT = 7778;
    private const string SERVER_IP = "192.158.1.65"; //change for different computer







    // Start is called before the first frame update
    void Start() {

        DontDestroyOnLoad(gameObject);
        Init();


    }

    public void Init() {

        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel = cc.AddChannel(QosType.ReliableSequenced);

        HostTopology topo = new HostTopology(cc, MAX_USER);

        //Client only code

        hostId = NetworkTransport.AddHost(topo, 0);


#if UNITY_WENGL && !UNITY_EDITOR
        //Web Client
        NetworkTransport.Connect(hostId, SERVER_IP, WEB_PORT,0, out error);
        Debug.Log("connectiong from web");
#else
        //Standalone Client
        NetworkTransport.Connect(hostId, SERVER_IP, PORT, 0, out error);
        Debug.Log("connecting from standalone");
#endif

        Debug.Log(string.Format("Opening connection on port {0} and webport {1}", PORT, WEB_PORT));
        isStarted = true;
    }

    public void Shutdown() {

        isStarted = false;
        NetworkTransport.Shutdown();

    }


}
