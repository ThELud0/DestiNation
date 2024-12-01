using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Train : MonoBehaviour
{
    float lifetime;
    float speed;
    //public int trainID;
    List<Vector2> railway;

    int destinyType;

    private int currentTargetIndex = 0; // Index of the current waypoint
    private Vector3 currentTarget; // The current target position in 3D space
    private bool isMoving = false; // Is the train moving?

    public UnityEvent onTrainArrived = new ();

    //[SerializeField] GameObject raycastOrigin;
    public void Initialize(float lifetime, float speed, List<Vector2> railway, int destinyType)
    {
        this.lifetime = lifetime;
        this.speed = speed;
        this.railway = railway;
        this.destinyType = destinyType;
    }


    void Start(){
        if (railway.Count > 0)
        {
            currentTargetIndex++;
            isMoving = true;
            SetNextTarget();
        }

        transform.position = currentTarget;

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

    private void SetNextTarget()
    {
        if (currentTargetIndex < railway.Count)
        {
            // Convert Vector2 to Vector3 (x, 0, z) for 3D space
            currentTarget = new Vector3(railway[currentTargetIndex].x, transform.position.y, railway[currentTargetIndex].y);
        }
        else
        {
            // No more waypoints, stop moving
            isMoving = false;
            Debug.Log("Train has reached the final destination.");
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
        if (collision.collider.CompareTag("Train"))
        {
            onTrainArrived.Invoke();
            //Destroy(collision.gameObject);
            Destroy();
        }
        if (collision.collider.CompareTag("Human"))
        {
            onTrainArrived.Invoke();
           //Destroy(collision.gameObject);
            Destroy();
        }
    }


    private void Destroy(){
        GameStateResources.compteurTrain--;
        if (destinyType == 1){
            GameStateResources.compteurDictator--;
        }else if (destinyType == 3){
            GameStateResources.compteurOld--;
        }
        Destroy(gameObject);
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
