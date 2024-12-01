using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections.Generic;
using TMPro.Examples;

public class Trainstation : MonoBehaviour
{
    float destinyTimer;
    float trainlifetime;
    float trainspeed;
    int destinyType;
    List<Vector2> test;


    [SerializeField] GameObject rockstarTrain;
    [SerializeField] GameObject oldTrain;
    [SerializeField] GameObject dictatorTrain;
    [SerializeField] GameObject lambdaTrain;

    private float floory = 3f;

    [SerializeField] GameObject departure;
    void Start()
    {
        destinyTimer = Time.time + Random.Range(3, 10);

        test = new List<Vector2>();
        for (int i = 5; i<15; i++)
        {
            test.Add(new Vector2(i, 5));
        }
        for (int i = 5; i < 15; i++)
        {
            test.Add(new Vector2(14, i));
        }
    }

    void Update()
    {
       /* if (Time.time >= destinyTimer)
        {
            changeNature();
        }*/

       /* if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            spawnTrain(test);
        }*/
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



   public void spawnTrain(List<Vector2> railway)
    {
        GameObject train = new();
            if (GameStateResources.trainstationDestinyType == 0)
            {
            trainlifetime = Random.Range(14, 26);
            trainspeed = 8;
             train = Instantiate(lambdaTrain, new Vector3(5f,floory,0f), Quaternion.identity);
        }
            else if (GameStateResources.trainstationDestinyType == 1)
            {
            trainlifetime = Random.Range(14, 26);
            trainspeed = 6;
             train = Instantiate(dictatorTrain, new Vector3(5f, floory, 0f), Quaternion.LookRotation(-dictatorTrain.transform.forward));
        }
            else if (GameStateResources.trainstationDestinyType == 2)
            {
                trainlifetime = 9;
                trainspeed = 12;
                 train = Instantiate(rockstarTrain, new Vector3(5f, floory, 0f), Quaternion.identity);
            }
            else if (GameStateResources.trainstationDestinyType == 3)
            {
            trainlifetime = Random.Range(34, 40);
            trainspeed = 5;
             train = Instantiate(oldTrain, new Vector3(5f, floory, 0f), Quaternion.identity);
        }
        if (train.GetComponent<Train>() != null)
        {
            Debug.Log("here");
            Train trainComponent = train.GetComponent<Train>();
            trainComponent.Initialize(trainlifetime, trainspeed, railway);
        }

    }



    /*
    void OnMouseOver()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameStateResources.trainStationSelected = true;
            GameStateResources.currentTrainStationId += 1;
            GameStateResources.trainstationPosition = departure.transform.position;
            GameStateResources.trainstationDestinyType = destinyType;
            GameStateResources.currentFixedX = (int)departure.transform.position.x;
            GameStateResources.currentFixedZ = (int)departure.transform.position.z;
            GameStateResources.previousX = (int)departure.transform.position.x;
            GameStateResources.previousZ = (int)departure.transform.position.z;
        }
    }*/

}
