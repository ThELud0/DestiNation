using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("before click: " + GameStateResources.mouseButtonHeldDown);
            GameStateResources.mouseButtonHeldDown = true;
            Debug.Log("after click: "+ GameStateResources.mouseButtonHeldDown);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (GameStateResources.trainStationSelected)
            {
                GameStateResources.trainStationSelected = false;
            }


            GameStateResources.currentFixedZ = 0;
            GameStateResources.currentFixedX = 0;
            GameStateResources.zAxisFixed = false;
            GameStateResources.xAxisFixed = false;
            GameStateResources.currentRailOrderId = 0;
            GameStateResources.currentTrainStationId = 0;
            GameStateResources.previousX = 0;
            GameStateResources.previousZ = 0;
            GameStateResources.mouseButtonHeldDown = false;
            GameStateResources.mouseButtonReleased = true;
            Invoke("setMouseButtonReleasedFalse", 0.05f);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (GameStateResources.zAxisFixed != GameStateResources.xAxisFixed)
            {
                GameStateResources.zAxisFixed = !GameStateResources.zAxisFixed;
                GameStateResources.xAxisFixed = !GameStateResources.xAxisFixed;
                GameStateResources.currentFixedX = GameStateResources.xAxisFixed ? GameStateResources.previousX : GameStateResources.currentFixedX;
                GameStateResources.currentFixedZ = GameStateResources.zAxisFixed ? GameStateResources.previousZ : GameStateResources.currentFixedZ;
            }
        }
    }


    void setMouseButtonReleasedFalse()
    {
        GameStateResources.mouseButtonReleased = false;
    }

}
