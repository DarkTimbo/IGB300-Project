using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClientSpawner : MonoBehaviour
{
    public string customServerIP;
    public string charName;
    public GameObject client;
    public InputField ipBox;
    public Image mainPortrait;
    public Image[] portraitList;
    public Text characterLabel;
    public GameObject selectButton;
    public GameObject LobbyScene;
    // Start is called before the first frame update
    void Start()
    {
        selectButton.SetActive(false);
        LobbyScene = GameObject.Find("Controller");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIP()
    {
        //Change ipv4 based on what the user inputs as a room code. If left blank, default is the user's current ipb4 address
        if (ipBox.text != "")
        {
            customServerIP = ipBox.text;
            client.GetComponent<Client>().serverIP = customServerIP;
        }
        client.GetComponent<Client>().Initialise();
        
    }

    public void ChooseCharacter(string characterName)
    {
        selectButton.SetActive(true);

        int i = 0;
        switch (characterName)
        {
            case "Brute":
                i = 0;
                break;

            case "Butler":
                i = 1;
                break;

            case "Entertainer":
                i = 2;
                break;

            case "Techie":
                i = 3;
                break;

            case "Engineer":
                i = 4;
                break;

            case "Chef":
                i = 5;
                break;
        }

        characterLabel.text = characterName;
        mainPortrait.sprite = portraitList[i].sprite;

        //charName = characterName;
        LobbyScene.GetComponent<LobbyScene>().character = characterName;
    }

 

    public void GoToGame()
    {
        SceneManager.LoadScene("Interface");
    }
}

