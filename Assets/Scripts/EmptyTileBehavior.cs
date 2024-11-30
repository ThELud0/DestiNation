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

    [SerializeField] GameObject railPrefab;

    [SerializeField] GameObject rockstarTrain;
    [SerializeField] GameObject oldTrain;
    [SerializeField] GameObject dictatorTrain;
    [SerializeField] GameObject lambdaTrain;

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
                Vector3 additionnalHeight = new Vector3(0f, 1f, 0f);
                GameObject rail = Instantiate(railPrefab, transform.position + additionnalHeight, Quaternion.identity);
                RailBehavior railComponent = rail.GetComponent<RailBehavior>();
                railComponent.AddTrainOrder(GameStateResources.currentTrainStationId, currentOrderId);
                isBuilding = false;
                built = true;

                if (GameStateResources.trainstationDestinyType == 0)
                {
                    SpawnLambdaTrain();
                }
                if (GameStateResources.trainstationDestinyType == 1)
                {
                    SpawnDictatorTrain();
                }
                if (GameStateResources.trainstationDestinyType == 2)
                {
                    SpawnRockstarTrain();
                }
                if (GameStateResources.trainstationDestinyType == 3)
                {
                    SpawnOldTrain();
                }
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

    void SpawnRockstarTrain()
    {
        GameObject train = Instantiate(rockstarTrain, GameStateResources.trainstationPosition, Quaternion.identity, null);
        Train trainComponent = train.GetComponent<Train>();
        trainComponent.Initialize(9, 12, GameStateResources.currentTrainStationId);
    }

    void SpawnOldTrain()
    {
        GameObject train = Instantiate(oldTrain, GameStateResources.trainstationPosition, Quaternion.identity, null);
        Train trainComponent = train.GetComponent<Train>();
        trainComponent.Initialize(Random.Range(34, 40), 5, GameStateResources.currentTrainStationId);
    }

    void SpawnDictatorTrain()
    {
        GameObject train = Instantiate(dictatorTrain, GameStateResources.trainstationPosition, Quaternion.identity, null);
        Train trainComponent = train.GetComponent<Train>();
        trainComponent.Initialize(Random.Range(14, 26), 6, GameStateResources.currentTrainStationId);
    }

    void SpawnLambdaTrain()
    {
        GameObject train = Instantiate(lambdaTrain, GameStateResources.trainstationPosition, Quaternion.identity, null);
        Train trainComponent = train.GetComponent<Train>();
        trainComponent.Initialize(Random.Range(14, 26), 8, GameStateResources.currentTrainStationId);
    }

}
