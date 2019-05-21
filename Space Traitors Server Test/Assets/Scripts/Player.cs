using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Navigation
{
    //Movement variables
    public float moveSpeed = 50.0f;
    public float minDistance = 0.1f;
    public int goalIndex = 0;
    public bool Turn = true;
    public bool move = true;
    public bool startMoving = false;

    //Network variables
    public int playerID;
    public string characterName;
    public bool connected;

    //Gameplay variables
    public float influence;
    private bool spawned = false;
    public GameObject playerStorage;
    public GameObject[] inventory;

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            playerStorage = GameObject.FindGameObjectWithTag("RoundManager");
            playerStorage.GetComponent<RoundManager>().playersInGame.Add(gameObject);
            spawned = true;
        }

        if (Turn == true && startMoving == true ) {
    
            PlayerMove(goalIndex);
        }
                
    }

    private void PlayerMove(int goalIndex) {

        if (move) {
            //Reset current path and add first node - needs to be done here because of recursive function of greedy
            currentPath.Clear();
            greedyPaintList.Clear();
            currentPathIndex = 0;
            currentPath.Add(currentNodeIndex);

            //Greedy Search
            currentPath = GreedySearch(currentPath[currentPathIndex], goalIndex, currentPath);

            //Reverse path and remove final (i.e. initial) position
            currentPath.Reverse();
            currentPath.RemoveAt(currentPath.Count - 1);
            move = false;
        }
        if (currentPath.Count > 0) {

            transform.position = Vector3.MoveTowards(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position, moveSpeed * Time.deltaTime);

            //Increase path index
            if (Vector3.Distance(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position) <= minDistance) {

                if (currentPathIndex < currentPath.Count - 1)
                    currentPathIndex++;
            }

            currentNodeIndex = graphNodes.graphNodes[currentPath[currentPathIndex]].GetComponent<LinkedNodes>().index;   //Store current node index
        }
        //If player object is at the last node of the path allow it to move again.
        if(transform.position == graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position) {
            move = true;
            startMoving = false;
        }

    }
        


        
  }

