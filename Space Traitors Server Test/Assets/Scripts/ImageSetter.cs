using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageSetter : MonoBehaviour
{
    GameObject server;
    public Image[] images;

    private void Start()
    {
        server = GameObject.FindGameObjectWithTag("Server");
    }
    public void GotoGame()
    {
        server.GetComponent<Server>().ClientNextScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   
}
