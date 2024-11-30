using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Trainstation : MonoBehaviour
{
    float destinyTimer;
    float lifetime;
    int destinyType;

    [SerializeField] GameObject rockstarTrain;
    [SerializeField] GameObject oldTrain;
    [SerializeField] GameObject dictatorTrain;
    [SerializeField] GameObject lambdaTrain;
    void Start()
    {
        destinyTimer = Time.time + Random.Range(3, 10);
    }

    void Update()
    {
        if (Time.time >= destinyTimer)
        {
            changeNature();

            if (destinyType == 0)
            {
                SpawnLambdaTrain();
            }
            if (destinyType == 1)
            {
                SpawnDictatorTrain();
            }
            if (destinyType == 2)
            {
                SpawnRockstarTrain();
            }
            if (destinyType == 3)
            {
                SpawnOldTrain();
            }
        }
    }

    void changeNature()
    {
        float rdm = Random.Range(0, 100);
        if (rdm < 45)
        {
            GetComponent<MeshRenderer>().material.color =  Color.blue;
            destinyType = 0;
        }
        else if (rdm < 55)
        {
            GetComponent<MeshRenderer>().material.color =  Color.red;
            destinyType = 1;
        }
        else if (rdm < 75)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
            destinyType = 2;
        }
        else if (rdm >= 75)
        {
            GetComponent<MeshRenderer>().material.color = Color.grey;
            destinyType = 3;
        }
        destinyTimer = Time.time + Random.Range(10, 30);
    }

    void SpawnRockstarTrain()
    {
        GameObject train = Instantiate(rockstarTrain, transform.position, Quaternion.identity);
        Train trainComponent = train.GetComponent<Train>();
        trainComponent.Initialize(9, 12);
    }

    void SpawnOldTrain()
    {
        GameObject train = Instantiate(oldTrain, transform.position, Quaternion.identity);
        Train trainComponent = train.GetComponent<Train>();
        trainComponent.Initialize(Random.Range(34, 40), 5);
    }

    void SpawnDictatorTrain()
    {
        GameObject train = Instantiate(dictatorTrain, transform.position, Quaternion.identity);
        Train trainComponent = train.GetComponent<Train>();
        trainComponent.Initialize(Random.Range(14, 26), 6);
    }

    void SpawnLambdaTrain()
    {
        GameObject train = Instantiate(lambdaTrain, transform.position, Quaternion.identity);
        Train trainComponent = train.GetComponent<Train>();
        trainComponent.Initialize(Random.Range(14, 26), 8);
    }

    void OnMouseOver()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameStateResources.trainStationSelected = true;
            GameStateResources.currentTrainStationId += 1;
            GameStateResources.currentFixedX = (int)transform.position.x;
            GameStateResources.currentFixedZ = (int)transform.position.z;
            GameStateResources.previousX = (int)transform.position.x;
            GameStateResources.previousZ = (int)transform.position.z;
        }
    }
}
