using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assigned to the room object which contains all the room models
public class Room : MonoBehaviour
{
    public int roomID;
    public string roomType;
    public Choice[] roomChoices;

    public void InitialiseRoom(int choicesPerRoom)
    {
        roomChoices = new Choice[choicesPerRoom];
        for (int choicePos = 0; choicePos < choicesPerRoom; choicePos++)
        {
            roomChoices[choicePos] = new Choice();
        }
    }
}
