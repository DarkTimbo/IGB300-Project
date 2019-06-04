using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitorDisplay : MonoBehaviour
{
    private Text traitorText;

    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        traitorText = gameObject.GetComponent<Text>();
        traitorText.enabled = false;
    }

    private void Update()
    {
        if (player.traitor)
        {
            traitorText.enabled = true;
        }
    }
}
