using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Image thisImage;
    private GameObject canvas;

    private void Start()
    {
        thisImage.enabled = false;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadImage()
    {
        canvas = GameObject.Find("Canvas");
        transform.parent = canvas.transform;
        thisImage.enabled = true;
    }
}
