using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageSetter : MonoBehaviour
{
    public Image[] images;

    public void GotoGame()
    {
        //Change to main game
        SceneManager.LoadScene("server"); //TODO: Should change to '1' later, build order index
    }
}
