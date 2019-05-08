using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Navigation : MonoBehaviour
{
    public WayPointGraph graphNodes;
    public List<int> currentPath = new List<int>();
    public List<int> greedyPaintList = new List<int>();
    public int currentPathIndex = 0;
    public int currentNodeIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Find waypoint graph
        graphNodes = GameObject.FindGameObjectWithTag("waypoint graph").GetComponent<WayPointGraph>();

        //Initial node index to move to
        currentPath.Add(currentNodeIndex);

    }


    public float Heuristic(int a, int b) {

        return Vector3.Distance(graphNodes.graphNodes[a].transform.position, graphNodes.graphNodes[b].transform.position);

    }

    public class GreedyChildren : IComparable<GreedyChildren> {

        public int childID { get; set; }
        public float childHScore { get; set; }

        public GreedyChildren(int childrenID, float childrenHScore) {
            this.childID = childrenID;
            this.childHScore = childrenHScore;
        }

        public int CompareTo(GreedyChildren other) {
            return this.childHScore.CompareTo(other.childHScore);
        }


    }



    //Greedy Search
    public List<int> GreedySearch(int currentNode, int goal, List<int> path) {

        //Code here

        if (!greedyPaintList.Contains(currentNode)) {
            greedyPaintList.Add(currentNode);
        }

        if (currentNode == goal) {
            path.Add(currentNode);
            return path;
        }

        //Make a custom list that stores the current node's children nodes and H scores. Sort them by ascending order of Heuristic
        List<GreedyChildren> thisNodesChildren = new List<GreedyChildren>();

        for (int i = 0; i < graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex.Length; i++) {

            thisNodesChildren.Add(new GreedyChildren(graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i], Heuristic(graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i], goal)));

        }

        thisNodesChildren.Sort();

        for (int i = 0; i < thisNodesChildren.Count; i++) {


            if (!greedyPaintList.Contains(thisNodesChildren[i].childID)) {

                if (thisNodesChildren[i].childID == goal) {

                    path.Add(thisNodesChildren[i].childID);
                    return path;
                }

                GreedySearch(thisNodesChildren[i].childID, goal, path);

                if (path.Count != 0) {

                    path.Add(thisNodesChildren[i].childID);
                    return path;
                }

            }

        }

        return path;
    }
}
