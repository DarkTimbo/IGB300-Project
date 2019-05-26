using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LobbyScene : MonoBehaviour
{

    public int influence;
    public string character;
    public GameObject clientSp;
    public GameObject Player;
    public GameObject client;
    public Scene CurrentScene;

    GraphicRaycaster raycaster;

     void Start()
    {
        DontDestroyOnLoad(gameObject);
        

    }

     void Update() {

        CurrentScene = SceneManager.GetActiveScene();
        raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();

        if (CurrentScene.name == "Character Select") {
            ClickOnSelect();

        }
    }

    //Button that calls The Send location in client, tells it what to send also
    public void OnClickChangeRoom(int room)
    {
        Client.Instance.ChangeLocation(room);
    }

    public void OnClickChangeVariable()
    {
        Client.Instance.SendPoints(character);
    }

    public void OnSendTurnEnd()
    {
        //Client.Instance.SendTurnEnd(false);
        client.GetComponent<Client>().SendTurnEnd(false);
    }

    private void ClickOnSelect() {


        if (Input.GetMouseButtonDown(0)) {

            //Set up the new Pointer Event
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            pointerData.position = Input.mousePosition;
            this.raycaster.Raycast(pointerData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results) {
               
                if(result.gameObject.name == "Select") {

                    OnClickChangeVariable();
                }

            }
        }

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