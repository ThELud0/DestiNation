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
            Debug.Log("before release: " + GameStateResources.mouseButtonHeldDown);
            GameStateResources.mouseButtonHeldDown = false;
            Debug.Log("after release: " + GameStateResources.mouseButtonHeldDown);
            GameStateResources.mouseButtonReleased = true;
            Invoke("setMouseButtonReleasedFalse", 0.05f);
        }
    }


    void setMouseButtonReleasedFalse()
    {
        GameStateResources.mouseButtonReleased = false;
    }

}
