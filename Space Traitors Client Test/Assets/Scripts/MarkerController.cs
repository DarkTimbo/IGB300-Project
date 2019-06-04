using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour
{
    readonly int[,] roomPositions = { { -5, -25 },
                                      { 4, -25 },
                                      { 4, -16 },
                                      { 4, -7 },
                                      { 13, -25 },
                                      { 13, -16 },
                                      { 13, -7 },
                                      { 22, -7 },
                                      { 22, -16 },
                                      { 22, -25 },
                                      { 31, -25 },
                                      { 31, -16 },
                                      { 40, -25 } };

    private void Awake()
    {
        UpdateRoomPos(9);
    }

    public void UpdateRoomPos(int roomNumber)
    {
        gameObject.transform.position = new Vector3(roomPositions[roomNumber, 0], 0.1f, roomPositions[roomNumber, 1]);
    }
}
