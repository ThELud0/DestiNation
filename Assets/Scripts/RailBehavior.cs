using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class RailBehavior : MonoBehaviour
{
    [Serializable]
    public class RailStyle
    {
        public float rotation;
        public Mesh mesh;
    }
    // A list of pairs (trainID, orderID)
    public List<(int trainID, int orderID)> trainOrders = new List<(int, int)>();
    private bool isBuilding;
    private int additionalOrderID;


    // Array to store different rail style meshes
    public RailStyle[] railStyles;

    // Reference to the MeshFilter of the Rail GameObject
    private MeshFilter meshFilter;

    // Variable to determine which rail style to use (can be set dynamically)
    public int currentRailStyleIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBuilding = false;

        // Get the MeshFilter component
        meshFilter = GetComponent<MeshFilter>();

        // Set the initial mesh
        //UpdateRailStyle();
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
                //UpdateRailStyle();
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


    public void UpdateRailStyle()
    {
        // Ensure the index is within bounds
        if (railStyles != null && railStyles.Length > 0 && currentRailStyleIndex >= 0 && currentRailStyleIndex < railStyles.Length)
        {
            // Update the mesh based on the current index
            meshFilter.mesh = railStyles[currentRailStyleIndex].mesh;
        }
        else
        {
            Debug.LogWarning("Invalid rail style index or no rail styles assigned!");
        }
    }

    public void checkSurroundings()
    {

    }


}
