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
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    */
    private void OnMouseOver()
    {
        if (GameStateResources.mouseButtonHeldDown && 
            (Mathf.Abs((int)transform.position.x - GameStateResources.previousX) < 2) && 
            (Mathf.Abs((int)transform.position.z - GameStateResources.previousZ) < 2) &&
            ((GameStateResources.xAxisFixed && ((int)transform.position.x == GameStateResources.currentFixedX)) || (GameStateResources.zAxisFixed && ((int)transform.position.z == GameStateResources.currentFixedZ)))
            )
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
