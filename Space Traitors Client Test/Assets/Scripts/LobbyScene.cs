using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{

    public Text roomText, influencePoints;
    public int influence;

    public void OnClickChangeRoom(string room)
    {
        roomText .text = ("You are in the " + room);
        Client.Instance.SendLocation(room);
    }

    public void OnClickChangeVariable()
    {
        influence += 1;
        influencePoints.text = (influence.ToString());
        Client.Instance.SendPoints(influence);
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