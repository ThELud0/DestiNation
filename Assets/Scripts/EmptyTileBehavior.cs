using UnityEngine;
using System.Collections.Generic; // Required for List

public class EmptyTileBehavior : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Color originalColor;
    private bool isGreen = false;

    public List<GameObject> gameObjects;
    public List<int> railIdVector;

    void Start()
    {
        railIdVector = new List<int>();
        gameObjects = new List<GameObject>();

        // Get the Renderer component and store the original color
        cubeRenderer = GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnMouseOver()
    {
        if (GameStateResources.mouseButtonHeldDown) // Check if the left mouse button is pressed
        {
            Debug.Log("here");
            if (!isGreen)
            {
                cubeRenderer.material.color = Color.green; // Change to green
                isGreen = true;
            }
        }
        else if (isGreen)
        {
            cubeRenderer.material.color = originalColor; // Revert to original color
            isGreen = false;
        }
    }

    void OnMouseExit()
    {

    }
}
