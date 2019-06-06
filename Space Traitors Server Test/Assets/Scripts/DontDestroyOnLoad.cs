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

    Vector3 oldPosition;
    bool posMoved;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        posMoved = false;
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if ((UI) && (sceneName == "server"))
        {
            canvas = GameObject.Find("Canvas");
            transform.parent = canvas.transform;
            if (!posMoved)
            {
                transform.position = oldPosition  + new Vector3 (Screen.width * 0.3f, Screen.height * 0.3f, 0.0f);
            }
            posMoved = true;
        }
    }
}
