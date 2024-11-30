using UnityEngine;
using System.Collections.Generic; // Required for List

public class EmptyTileBehavior : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Color originalColor;
    private bool isBuilding;
    private bool built;

    public List<GameObject> gameObjects;
    public List<int> railIdVector;
    public int currentOrderId;

    void Start()
    {
        railIdVector = new List<int>();
        gameObjects = new List<GameObject>();

        // Get the Renderer component and store the original color
        cubeRenderer = GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color;

        built = false;
        isBuilding = false;

        currentOrderId = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
        {
            if(GameStateResources.mouseButtonReleased && !GameStateResources.humanReached)
            {
                cubeRenderer.material.color = originalColor;
                isBuilding = false;
            }
            else if (GameStateResources.mouseButtonReleased && GameStateResources.humanReached)
            {
                cubeRenderer.material.color = Color.blue;
                isBuilding = false;
                built = true;
            }
            else if (GameStateResources.mouseButtonHeldDown && GameStateResources.humanReached)
            {
                cubeRenderer.material.color = Color.green;
            }
        }




    }

    void OnMouseOver()
    {
        GameStateResources.currentX = (int)transform.position.x;
        GameStateResources.currentZ = (int)transform.position.z;

        if ((GameStateResources.zAxisFixed == false)&&(GameStateResources.xAxisFixed==false)&& GameStateResources.mouseButtonHeldDown && GameStateResources.trainStationSelected)
        {
            if (GameStateResources.currentX > GameStateResources.currentFixedX)
            {
                GameStateResources.zAxisFixed = true;
            }
            else
                GameStateResources.xAxisFixed = true;
        }

        if (GameStateResources.mouseButtonHeldDown && GameStateResources.trainStationSelected) // Check if the left mouse button is pressed
        {
            if (!built && !isBuilding)
            {
                if (GameStateResources.zAxisFixed && ((int)transform.position.z == GameStateResources.currentFixedZ) && (Mathf.Abs((int)transform.position.x - GameStateResources.previousX) < 2) )
                {
                    currentOrderId = GameStateResources.currentRailOrderId;
                    GameStateResources.currentRailOrderId++;
                    isBuilding = true;
                    cubeRenderer.material.color = Color.red;
                    GameStateResources.previousX = (int)transform.position.x;

                }
                else if (GameStateResources.xAxisFixed && ((int)transform.position.x == GameStateResources.currentFixedX) && (Mathf.Abs((int)transform.position.z - GameStateResources.previousZ) < 2) )
                {
                    currentOrderId = GameStateResources.currentRailOrderId;
                    GameStateResources.currentRailOrderId++;
                    isBuilding = true;
                    cubeRenderer.material.color = Color.red;
                    GameStateResources.previousZ = (int)transform.position.z;
                }

            }
        }


    }

    void OnMouseExit()
    {
        //Invoke("CancelBuildIfNecessary", 0.1f);
    }


    /*
    void CancelBuildIfNecessary()
    {
        if (!(GameStateResources.currentRailOrderId > currentOrderId + 1)&&(!GameStateResources.humanReached))
        {
            cubeRenderer.material.color = originalColor;
            isBuilding = false;
            GameStateResources.currentRailOrderId--;
        }

    }*/

}
