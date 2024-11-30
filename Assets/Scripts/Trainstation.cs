using UnityEngine;
using UnityEngine.InputSystem;
public class Trainstation : MonoBehaviour
{
    float destinyTimer;
    float trainTimer;
    void Start()
    {
        destinyTimer = Time.time + Random.Range(10, 30);
        trainTimer = Random.Range(2, 6);
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
        //GetComponent<MeshRenderer>().material.mainTexture ;
    }

    void OnMouseOver()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameStateResources.trainStationSelected = true;
            GameStateResources.currentFixedX = (int)transform.position.x;
            GameStateResources.currentFixedZ = (int)transform.position.z;
            GameStateResources.previousX = (int)transform.position.x;
            GameStateResources.previousZ = (int)transform.position.z;
        }
    }
}
