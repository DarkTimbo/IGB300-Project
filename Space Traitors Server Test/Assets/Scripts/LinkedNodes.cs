using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedNodes : MonoBehaviour {
    public int index;

    public GameObject[] linkedNodeObjects;

    public int[] linkedNodesIndex;

    // Use this for initialization
    void Start() {
        //Get the correct index for each linked Node
        linkedNodesIndex = new int[linkedNodeObjects.Length];

        for (int i = 0; i < linkedNodesIndex.Length; i++) {

            linkedNodesIndex[i] = linkedNodeObjects[i].GetComponent<LinkedNodes>().index;
        }
    }

    // Update is called once per frame
    void Update() {
        //Draw lines between each connected waypoint
        foreach (GameObject linkedNode in linkedNodeObjects) {
            Debug.DrawLine(transform.position, linkedNode.transform.position, Color.green);
        }
    }
}
