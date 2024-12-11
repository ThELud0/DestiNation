using UnityEngine;
using UnityEngine.Events;

public class Human : MonoBehaviour
{
    float time_spawn;
    private float durationTillDespawn;
    private BabyManager bbInstance;

  //  public UnityEvent<Human> OnBabyDespawn = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(float _durationTillDespawn, BabyManager bInstance){
        durationTillDespawn = _durationTillDespawn;
        bbInstance = bInstance;
        time_spawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if( Time.time - time_spawn > durationTillDespawn && !gameState.gamePause){
                bbInstance.babyLost(this);
        }
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
  /*  private void OnMouseOver()
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
    }*/
}
