using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Trainstation : MonoBehaviour
{
    float destinyTimer;
    float lifetime;
    int destinyType;

    void Start()
    {
        destinyTimer = Time.time + Random.Range(3, 10);
    }

    void Update()
    {
        if (Time.time >= destinyTimer)
        {
            changeNature();
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

    void OnMouseOver()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameStateResources.trainStationSelected = true;
            GameStateResources.currentTrainStationId += 1;
            GameStateResources.trainstationDestinyType = destinyType;
            GameStateResources.currentFixedX = (int)transform.position.x;
            GameStateResources.currentFixedZ = (int)transform.position.z;
            GameStateResources.previousX = (int)transform.position.x;
            GameStateResources.previousZ = (int)transform.position.z;
        }
    }
}
