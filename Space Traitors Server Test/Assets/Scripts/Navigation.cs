using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Navigation : MonoBehaviour
{
    public WayPointGraph graphNodes;

    public int currentPathIndex = 0;
    public int currentNodeIndex = 9;
    public List<int> openList = new List<int>();
    public List<int> closedList = new List<int>();
    public List<int> currentPath = new List<int>();
    public Dictionary<int, int> cameFrom = new Dictionary<int, int>();



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



    //A-Star Search
    public List<int> AStarSearch(int start, int goal) {

        //Clear everything at start
        openList.Clear();
        closedList.Clear();
        cameFrom.Clear();

        //Begin
        openList.Add(start);

        float gScore = 0;
        float fScore = gScore + Heuristic(start, goal);


        while (openList.Count > 0) {
            //Find the Node in openList that has the lowest fScore value
            int currentNode = bestOpenListFScore(start, goal);

            if (currentNode == goal) {
                return ReconstructPath(cameFrom, currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //For each of the nodes conncected to the current node
            for (int i = 0; i < graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex.Length; i++) {

                int thisNeighbourNode = graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i];

                //Ignore if neighbour node is attached
                if (!closedList.Contains(thisNeighbourNode)) {

                    //distance from current to the nextNode
                    float tentativeGScore = Heuristic(start, currentNode) + Heuristic(currentNode, thisNeighbourNode);

                    //Check to see if in openList or if new GScore is more sensible
                    if (!openList.Contains(thisNeighbourNode) || tentativeGScore < gScore) {

                        openList.Add(thisNeighbourNode);
                    }

                    if (!cameFrom.ContainsKey(thisNeighbourNode)) {
                        cameFrom.Add(thisNeighbourNode, currentNode);
                    }

                    gScore = tentativeGScore;
                    fScore = Heuristic(start, thisNeighbourNode) + Heuristic(thisNeighbourNode, goal);


                }

            }

        }

        return null;
    }

    public List<int> ReconstructPath(Dictionary<int, int> CF, int current) {

        List<int> finalPath = new List<int>();
        finalPath.Add(current);

        while (CF.ContainsKey(current)) {

            current = CF[current];
            finalPath.Add(current);
        }

        finalPath.Reverse();
        return finalPath;

    }

    public int bestOpenListFScore(int start, int goal) {

        int bestIndex = 0;

        for (int i = 0; i < openList.Count; i++) {

            if ((Heuristic(openList[i], start) + Heuristic(openList[i], goal)) < (Heuristic(openList[bestIndex], start) + Heuristic(openList[bestIndex], goal))) {

                bestIndex = i;

            }

        }

        int bestNode = openList[bestIndex];
        return bestNode;

    }
}
