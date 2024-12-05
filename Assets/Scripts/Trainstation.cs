using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine.Events;

public class Trainstation : MonoBehaviour
{
    float destinyTimer;
    float trainlifetime;
    float trainspeed;
    int destinyType;
    List<Vector2> currentRailway;
    public bool occupied = false;
    public Vector3 startPoint;
    public UnityEvent<List<Vector2>> onTrainHasArrived = new();

    


    [SerializeField] GameObject rockstarTrain;
    [SerializeField] GameObject oldTrain;
    [SerializeField] GameObject dictatorTrain;
    [SerializeField] GameObject lambdaTrain;

    private float floory = 2f;

    [SerializeField] GameObject departure;

    public Vector3 getDeparturePosition(){
        return departure.transform.position;
    }
    void Start()
    {
        destinyTimer = Time.time + Random.Range(3, 10);
        /*
        test = new List<Vector2>();
        for (int i = 5; i<18; i++)
        {
            test.Add(new Vector2(i, 5));
        }
        for (int i = 5; i < 15; i++)
        {
            test.Add(new Vector2(17, i));
        }*/
    }

    public Vector2[] getStartRailPosition()
    {
        Vector2[] l = new Vector2[2];
        l[0] = new Vector2 (startPoint.x, startPoint.z+1);
        l[1]= new Vector2 (startPoint.x, startPoint.z-1);
        return l;
    }

    void Update()
    {
        
        if (Time.time >= destinyTimer)
        {
            changeNature();
        }

       /* if (Mouse.current.leftButton.wasPressedThisFrame)

        {
            destinyType++;
            destinyType = destinyType % 4;
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
        
        //currentRailway = railway;
        currentRailway = GameTools.copyListVector2(railway);
        occupied = true;
        GameObject train = new();
            if (destinyType == 0)
            {
            trainlifetime = Random.Range(14, 26);
            trainspeed = 8;
             train = Instantiate(lambdaTrain, new Vector3(5f,floory,0f), Quaternion.identity);
        }
            else if (destinyType == 1)
            {
            trainlifetime = Random.Range(14, 26);
            trainspeed = 6;
            GameStateResources.compteurDictator++;
             train = Instantiate(dictatorTrain, new Vector3(5f, floory, 0f), Quaternion.LookRotation(-dictatorTrain.transform.forward));
        }
            else if (destinyType == 2)
            {
                trainlifetime = 9;
                trainspeed = 12;
                 train = Instantiate(rockstarTrain, new Vector3(5f, floory, 0f), Quaternion.identity);
            }
            else if (destinyType == 3)
            {
            trainlifetime = Random.Range(34, 40);
            trainspeed = 5;
            GameStateResources.compteurOld++;
             train = Instantiate(oldTrain, new Vector3(5f, floory, 0f), Quaternion.identity);
        }
        if (train.GetComponent<Train>() != null)
        {
            Debug.Log("here");
            GameStateResources.compteurTrain++;
            Train trainComponent = train.GetComponent<Train>();
            train.transform.position = GameTools.get3Dfrom2DVector(railway[0]);
            trainComponent.Initialize(trainlifetime, trainspeed, railway,destinyType);
            Debug.Log(railway[0].x+"/" +railway[0].y);
            trainComponent.onTrainArrived.AddListener(trainHasArrived);
        }



    }

    void trainHasArrived()
    {
        occupied = false;
        Debug.Log(currentRailway.Count+" is the railwaay");
        onTrainHasArrived.Invoke(currentRailway);
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
