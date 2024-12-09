using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using TMPro.Examples;

public class Train : MonoBehaviour
{
    float lifetime;
    float speed, floorY = 2f;
    //public int trainID;
    List<Vector2> railway = new();

    int destinyType, counter = 0, time_wait_till_despawn = 3;

    private int currentTargetIndex = 0; // Index of the current waypoint
    private Vector3 currentTarget; // The current target position in 3D space
    private bool isMoving = false; // Is the train moving?

    private bool hasTheTrainReturned = false, hasLaunchedTrigger = false;

    public UnityEvent<List<Vector2>, Human, Train> onTrainArrived = new ();

    //[SerializeField] GameObject raycastOrigin;
    public void Initialize(float lifetime, float speed, List<Vector2> given_railway, int destinyType)
    {
        this.lifetime = lifetime;
        this.speed = speed;
        foreach(Vector2 vec in given_railway){
            railway.Add(vec);
        }
        this.destinyType = destinyType;

        launchTrain();
    }


    void Start(){
        if (railway.Count > 0)
        {
            currentTargetIndex++;
            isMoving = true;
            SetNextTarget();
        }


        /*
        Vector3 direction = currentTarget - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Quaternion rotationOffset = Quaternion.Euler(0, 90, 0);
            targetRotation *= rotationOffset;

            transform.rotation = targetRotation;
        }*/
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void launchTrain(){
        currentTarget = railway[0];
        currentTarget.y = floorY;
        transform.position = GameTools.get3Dfrom2DVector(currentTarget);
        currentTargetIndex=1;
        isMoving = true;
        SetNextTarget();
    }

    private void SetNextTarget()
    {

        Debug.Log(currentTarget+"///"+currentTargetIndex+"////"+railway.Count);
        if (currentTargetIndex < railway.Count)
        {
            // Convert Vector2 to Vector3 (x, 0, z) for 3D space
            currentTarget = new Vector3(railway[currentTargetIndex].x,floorY, railway[currentTargetIndex].y);
        }
        else
        {

            if(hasTheTrainReturned) {
                trainHasReacheddestination(null);
            }else{
                goReverse();
            }
            //trainHasReacheddestination();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calculate the step size for this frame
        float step = speed * Time.deltaTime;

        // Move the train towards the current target
        //transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
        transform.position = Vector3.Lerp(transform.position, currentTarget, step / Vector3.Distance(transform.position, currentTarget));
        RotateTowardsTarget();

        // Check if the train has reached the current target
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            // Move to the next waypoint
            currentTargetIndex++;
            Debug.Log("Next");
            SetNextTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = currentTarget - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            Quaternion rotationOffset = Quaternion.Euler(0, -90, 0);
            targetRotation *= rotationOffset;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        return;
        if (collision.collider.CompareTag("Train"))
        {
            if(hasLaunchedTrigger){
                return;
            }
            hasLaunchedTrigger=true;
            onTrainArrived.Invoke(railway, null, this);
            //Destroy(collision.gameObject);
            //Destroy();
        }
        if (collision.collider.CompareTag("Human"))
        {
            if(hasLaunchedTrigger){
                return;
            }
                        hasLaunchedTrigger=true;

            //onTrainArrived.Invoke(railway, collision.gameObject.GetComponent<Human>(), this);
           //Destroy(collision.gameObject);
            //Destroy();
            trainHasReacheddestination(collision.gameObject.GetComponent<Human>());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.CompareTag("Human"))
        {
            Debug.Log("TTrigger"+counter);
            counter++;
            if(hasLaunchedTrigger){
                return;
            }
            hasLaunchedTrigger=true;

           trainHasReacheddestination(collision.GetComponent<Human>());
        }
         if (collision.CompareTag("Train"))
        {
            Debug.Log("Carambolage");
            isMoving = false;
            StartCoroutine(waitTillDisappear());
        }
    }

    private void trainHasReacheddestination(Human h){
        // No more waypoints, stop moving
            isMoving = false;
            Debug.Log("Train has reached the final destination.");
            onTrainArrived.Invoke(railway,h,this);
    }

    public void goReverse(){
        List<Vector2> new_railway = new ();
        for (int i = 0; i < railway.Count; i++)
        {
            new_railway.Add(railway[railway.Count-i-1]);
        }
        railway = new_railway;
        hasTheTrainReturned = true;
        currentTargetIndex = 0;
        SetNextTarget();
    }


    public void DestroyTrain(){
        GameStateResources.compteurTrain--;
        if (destinyType == 1){
            GameStateResources.compteurDictator--;
        }else if (destinyType == 3){
            GameStateResources.compteurOld--;
        }
        Destroy(gameObject);
    }

       IEnumerator waitTillDisappear(){
            yield return new WaitForSeconds(time_wait_till_despawn);

            trainHasReacheddestination(null);
    }

    /*
    // Update is called once per frame
    void Update()
    {


        
        //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        transform.position += transform.forward*speed*Time.deltaTime;

        int layerMask = 1 << 6;
        layerMask = ~layerMask;
        RaycastHit hit;
        Debug.DrawRay(transform.position + transform.forward, transform.TransformDirection(-Vector3.up) * 1, Color.yellow);
        Debug.DrawRay(transform.position + transform.right- transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, Color.yellow);
        Debug.DrawRay(transform.position - transform.right- transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, Color.yellow);

        if (Physics.Raycast(transform.position - transform.right - transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, out hit, 1, layerMask) && test){
            if (hit.transform.tag == "Rail"){
                transform.Rotate(Vector3.up, -90);
                test = false;
            }
            //
        }else if (Physics.Raycast(transform.position + transform.right - transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, out hit, 1, layerMask) && test){
            if (hit.transform.tag == "Rail"){
                transform.Rotate(Vector3.up, 90);
                test = false;
            }
        }

        // if (Physics.Raycast(raycastOrigin.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, layerMask))
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //     Debug.Log("Did Hit");
        //     if (hit.collider.CompareTag("Rail"))
        //     {
        //         if (hit.collider.gameObject.GetComponent<RailBehavior>().trainOrders[0].trainID == trainID)
        //         {

        //         }
        //     }
        // }
        // else
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1000, Color.white);
        //     Debug.Log("Did not Hit");
        // }
    }*/



}
