using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointGraph : MonoBehaviour
{
    public GameObject[] graphNodes;

    // Use this for initialization
    void Start() {

        //Assign nodes in array the index that they are in graphNodes array
        for (int i = 0; i < graphNodes.Length; i++) {
            graphNodes[i].GetComponent<LinkedNodes>().index = i;
        }
    }
}
