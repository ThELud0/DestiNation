using UnityEngine;
using System.Collections.Generic;
public class RailBehavior : MonoBehaviour
{

    // A list of pairs (trainID, orderID)
    public List<(int trainID, int orderID)> trainOrders = new List<(int, int)>();
    private bool isBuilding;
    private int additionalOrderID;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBuilding = false;
    }

    public void AddTrainOrder(int trainID, int orderID)
    {
        trainOrders.Add((trainID, orderID));
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
        {
            if (GameStateResources.mouseButtonReleased && !GameStateResources.humanReached)
            {
                isBuilding = false;
            }
            else if (GameStateResources.mouseButtonReleased && GameStateResources.humanReached)
            {
                AddTrainOrder(GameStateResources.currentTrainStationId, additionalOrderID);
                isBuilding = false;
            }

        }
    }
    void OnMouseOver()
    {
        foreach (var pair in trainOrders)
            Debug.Log(pair.trainID + " " + pair.orderID);

        GameStateResources.currentX = (int)transform.position.x;
        GameStateResources.currentZ = (int)transform.position.z;



        if (GameStateResources.mouseButtonHeldDown && GameStateResources.trainStationSelected) // Check if the left mouse button is pressed
        {
            if (!isBuilding)
            {
                if (GameStateResources.zAxisFixed && ((int)transform.position.z == GameStateResources.currentFixedZ) && (Mathf.Abs((int)transform.position.x - GameStateResources.previousX) < 2))
                {
                    additionalOrderID = GameStateResources.currentRailOrderId;
                    GameStateResources.currentRailOrderId++;
                    isBuilding = true;
                    GameStateResources.previousX = (int)transform.position.x;

                }
                else if (GameStateResources.xAxisFixed && ((int)transform.position.x == GameStateResources.currentFixedX) && (Mathf.Abs((int)transform.position.z - GameStateResources.previousZ) < 2))
                {
                    additionalOrderID = GameStateResources.currentRailOrderId;
                    GameStateResources.currentRailOrderId++;
                    isBuilding = true;
                    GameStateResources.previousZ = (int)transform.position.z;

                }

            }


        }


    }
}
