using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : Navigation
{
    //Movement variables
    public float moveSpeed = 50.0f;
    public float minDistance = 0.1f;
    public int goalIndex = 0;
    public bool Turn = true;
    public bool move = true;
    public bool startMoving = false;
    public bool Begin = false;
    public bool isMoving = false;

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

        if (Turn == true && Begin == true ) {
    
            PlayerMove(goalIndex);
        }
                
    }

    public void PlayerMove(int goalIndex) {
    
        currentPath = AStarSearch(currentPath[currentPathIndex], goalIndex);
        currentPathIndex = 0;

        if (startMoving == true) {

            //Move player
            if (currentPath.Count > 0) {

                isMoving = true;

                Vector3 direction = (graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position - this.transform.position).normalized;
                Quaternion look = Quaternion.LookRotation(direction);
                transform.rotation = look;

                transform.position = Vector3.MoveTowards(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position, moveSpeed * Time.deltaTime);

                //Increase path index
                if (Vector3.Distance(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position) <= minDistance) {

                    if (currentPathIndex < currentPath.Count - 1)

                        currentPathIndex++;
                }

                currentNodeIndex = graphNodes.graphNodes[currentPath[currentPathIndex]].GetComponent<LinkedNodes>().index;   //Store current node index
            }
        }

        if(transform.position == graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position) {

            isMoving = false;

        }
    }
    
              
  }

