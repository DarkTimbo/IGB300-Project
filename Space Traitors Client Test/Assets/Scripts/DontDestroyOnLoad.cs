using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public bool UI = false;
    private Scene currentScene;
    private string sceneName;
    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
       
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if ((UI) && (sceneName == "Character Select"))
        {
            canvas = GameObject.Find("Canvas");
            transform.parent = canvas.transform;
        }
    }
}
