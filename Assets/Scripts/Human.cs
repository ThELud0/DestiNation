using UnityEngine;

public class Human : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnMouseOver()
    {
        if (GameStateResources.mouseButtonHeldDown)
        {
            GameStateResources.humanReached = true;
        }
    }

    private void OnMouseExit()
    {
        if (GameStateResources.humanReached)
        GameStateResources.humanReached = false;
    }
}