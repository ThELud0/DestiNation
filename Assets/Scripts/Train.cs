//using UnityEditor.ShaderGraph.Internal;
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
    List<Vector2> railway = new(), copyRail=new(), list_rail_passed = new();

    int destinyType, counter = 0, time_wait_till_despawn = 10, stepRotation = 0;

    public bool gamePaused = false;
    [SerializeField] float magnitudeTreshold=1f, rotateTreshold=2f;
    private int currentTargetIndex = 0; // Index of the current waypoint
    private Vector3 currentTarget; // The current target position in 3D space
    private bool isMoving = false, isTurning = false; // Is the train moving?

    private int babyCaptured = 0;

    private bool hasTheTrainReturned = false, hasLaunchedTrigger = false;

    public UnityEvent<List<Vector2>, int, Train> onTrainArrived = new ();

    public UnityEvent onTrainCrash = new ();

    public UnityEvent<GameObject> onBabyCaptured = new ();

    //[SerializeField] GameObject raycastOrigin;
    public void Initialize(float lifetime, float speed, List<Vector2> given_railway, int destinyType)
    {
        this.lifetime = lifetime;
        this.speed = speed;
        foreach(Vector2 vec in given_railway){
            railway.Add(vec);
            copyRail.Add(vec);
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

        if (isMoving && !gameState.gamePause)
        {
            MoveTowardsTarget();
        }
    }

    private void launchTrain(){
        gameState.compteurTrain++;
        //currentTarget = railway[0];
        currentTarget = new Vector3(railway[0].x,floorY, railway[0].y);
       // currentTarget.y = floorY;
        transform.position = currentTarget;
        Vector2 firstDir = railway[1]-railway[0];
        transform.rotation = Quaternion.LookRotation(new Vector3(firstDir.x,0, firstDir.y));
        currentTargetIndex=1;
        isTurning=false;
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
                trainHasReacheddestination(babyCaptured);
            }else{
                goReverse(railway);
            }
            //trainHasReacheddestination();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calculate the step size for this frame
        float step = speed * Time.deltaTime;
        RotateTowardsTarget();
        if(isTurning){
            return;
        }
        // Move the train towards the current target
        //transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
        transform.position = Vector3.Lerp(transform.position, currentTarget, step / Vector3.Distance(transform.position, currentTarget));

        // Check if the train has reached the current target
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {

            if(!hasTheTrainReturned){
            list_rail_passed.Add(railway[currentTargetIndex]);

            }

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
       /* Vector3 direction = currentTarget - transform.position;
        if (direction != Vector3.zero )
        {
            isTurning = true;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            Quaternion rotationOffset = Quaternion.Euler(0, -90, 0);
            targetRotation *= rotationOffset;

            Quaternion leftTorotate = transform.rotation * Quaternion.Inverse(targetRotation);
         isTurning = leftTorotate.eulerAngles.magnitude >magnitudeTreshold;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,rotateTreshold+1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,-1);

        stepRotation++;


        }*/

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
           // onTrainArrived.Invoke(railway, 0, this);
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
          //  trainHasReacheddestination(collision.gameObject.GetComponent<Human>());
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
            //Destroy(collision.gameObject);
            onBabyCaptured.Invoke(collision.transform.gameObject);
            babyCaptured++;
            goReverse(list_rail_passed);
        }
         if (collision.CompareTag("Train"))
        {
            Debug.Log("Carambolage");
            isMoving = false;
            onTrainCrash.Invoke();
            
            StartCoroutine(waitTillDisappear());
        }
    }

    private void trainHasReacheddestination(int h){
        // No more waypoints, stop moving
            isMoving = false;
            Debug.Log("Train has reached the final destination.");
            onTrainArrived.Invoke(copyRail,h,this);
    }

    public void goReverse(List<Vector2> current_rail){
        List<Vector2> new_railway = new ();
        for (int i = 0; i < current_rail.Count; i++)
        {
            new_railway.Add(current_rail[current_rail.Count-i-1]);
        }
        railway = new_railway;
        hasTheTrainReturned = true;
        currentTargetIndex = 0;
        currentTarget= new_railway[0];
        Vector3 direction = currentTarget - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
         transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,rotateTreshold+1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,-1);
        SetNextTarget();
    }


    public void DestroyTrain(){
        GameStateResources.compteurTrain--;
        gameState.compteurTrain--;
        if (destinyType == 1){
            GameStateResources.compteurDictator--;
        }else if (destinyType == 3){
            GameStateResources.compteurOld--;
        }
        Destroy(gameObject);
    }

       IEnumerator waitTillDisappear(){
            yield return new WaitForSeconds(time_wait_till_despawn);

            trainHasReacheddestination(0);
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
