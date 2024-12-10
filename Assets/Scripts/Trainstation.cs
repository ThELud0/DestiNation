using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine.Events;
using UnityEditor;
using Unity.VisualScripting;

public class Trainstation : MonoBehaviour
{
    float destinyTimer;
    float trainlifetime;
    float trainspeed, speedModificator = 0.5f;
    int destinyType;
    List<Vector2> currentRailway;
    public bool occupied = false;
    public Vector3 startPoint;

    [SerializeField] private GameObject[] list_entrance;



    //public UnityEvent<List<Vector2>> onTrainHasArrived = new();

    private BetterPathFinderTool pathFindToolInstance;

    


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

    public void setPathFindToolInstance(BetterPathFinderTool tool){
        pathFindToolInstance = tool;
    }

    public Vector2[] getStartRailPosition()
    {
        Vector2[] l = new Vector2[2];
        l[0] = new Vector2 (startPoint.x, startPoint.z+1);
        l[1]= new Vector2 (startPoint.x, startPoint.z-1);
        return l;
    }

    public Vector2 getClosestStartRailPosition(Vector2 pos_mouse){
            Vector2 pos_selected = GameTools.get2Dfrom3DVector(list_entrance[0].transform.position);
            foreach(GameObject g in list_entrance){
                Vector2 test_pos = GameTools.get2Dfrom3DVector(g.transform.position);
                if(Vector2.Distance(test_pos, pos_mouse)<Vector2.Distance(pos_selected, pos_mouse)){
                    pos_selected = test_pos;
                }
            }
            return pos_selected;
    }

    void Update()
    {
        destinyType = 0;
        if (Time.time >= destinyTimer)
        {
           // changeNature();
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
            trainComponent.Initialize(trainlifetime, trainspeed*speedModificator, railway,destinyType);
            Debug.Log(railway[0].x+"/" +railway[0].y);
            trainComponent.onTrainArrived.AddListener(trainHasArrived);
        }



    }

    void trainHasArrived(List<Vector2> list_vec, Human human_reached, Train train)
    {
        occupied = false;
        Debug.Log(list_vec.Count+" is the railwaay");
        //onTrainHasArrived.Invoke(list_vec);
        //onTrainHasArrived.RemoveAllListeners();
        pathFindToolInstance.callForDeleteCurrent(list_vec);
        train.DestroyTrain();

        if(human_reached!=null){
            Destroy(human_reached.gameObject);
            pathFindToolInstance.gameStateInstance.addToScore();
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
