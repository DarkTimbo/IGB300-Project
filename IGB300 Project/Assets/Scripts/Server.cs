using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    private byte reliableChannel;
    private int hostId;
    private int webHostId;

    private bool isStarted;


    private const int MAX_USER = 6;
    private const int PORT = 7777;
    private const int WEB_PORT = 7778;







    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(gameObject);
        Init();


    }

    public void Init() {

        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel = cc.AddChannel(QosType.ReliableSequenced);

        HostTopology topo = new HostTopology(cc, MAX_USER);

        //Server only code

        hostId = NetworkTransport.AddHost(topo, PORT, null);
        webHostId = NetworkTransport.AddWebsocketHost(topo, WEB_PORT, null);

        Debug.Log(string.Format("Opening connection on port {0} and webport {1}", PORT, WEB_PORT));
        isStarted = true;
    }

    public void Shutdown() {

        isStarted = false;
        NetworkTransport.Shutdown();

    }

  
}
