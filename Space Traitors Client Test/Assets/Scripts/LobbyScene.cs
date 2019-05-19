using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{


    public int influence;
    public string character = "Brute";
    public GameObject clientSp;

    public void Start()
    {

    }

    //Button that calls The Send location in client, tells it what to send also
    public void OnClickChangeRoom(int room)
    {
        Client.Instance.SendLocation(room);
    }

    public void OnClickChangeVariable()
    {
        Client.Instance.SendPoints(character);
    }
}

//public void OnClickCreateAccount()
//{
//    string username = GameObject.Find("CreateUsername").GetComponent<TMP_InputField>().text;
//    string password = GameObject.Find("CreatePassword").GetComponent<TMP_InputField>().text;
//    string email = GameObject.Find("CreateEmail").GetComponent<TMP_InputField>().text;

//    Client.Instance.SendCreateAccount(username, password, email);
//}

//public void OnClickLoginRequest()
//{
//    string username = GameObject.Find("LoginUsernameEmail").GetComponent<TMP_InputField>().text;
//    string password = GameObject.Find("LoginPassword").GetComponent<TMP_InputField>().text;

//    Client.Instance.SendLoginRequest(username, password);
//}