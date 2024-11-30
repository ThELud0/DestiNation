using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Train : MonoBehaviour
{
    float lifetime;
    float speed;
    float speedOriginal;
    float speedLow;
    public int trainID;
    [SerializeField] GameObject raycastOrigin;
    public void Initialize(float lifetime, float speed, int trainID)
    {
        this.lifetime = lifetime;
        this.speed = speed;
        transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);
    }
    bool test;
    RaycastHit hit;
    RaycastHit currentHit;
    int currentCaseOrderId;
    bool canTurn;

    [SerializeField] private int layerMask;

    void Start(){
        speed = 5;
        test = true;
        canTurn = true;
        speedOriginal = speed;
        speedLow = speed /3;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        transform.position += transform.forward*speed*Time.deltaTime;

        int layerMask = 1 << 6;
        layerMask = ~layerMask;
        checkCaseBelow();
        
        Debug.DrawRay(transform.position + transform.forward, transform.TransformDirection(-Vector3.up) * 1, Color.yellow);
        Debug.DrawRay(transform.position + transform.right- transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, Color.yellow);
        Debug.DrawRay(transform.position - transform.right- transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, Color.yellow);
        Physics.Raycast(transform.position + transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, out currentHit, 1, layerMask);
        Debug.DrawRay(transform.position + transform.forward, transform.TransformDirection(-Vector3.up) * 1, Color.yellow);

        if (Physics.Raycast(transform.position - transform.right - transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, out hit, 1, layerMask) && !currentHit.transform)
        {
            if (hit.transform.tag == "Rail" ){
                transform.Rotate(Vector3.up, -90);
                canTurn = false;
                //Invoke("canTurnToTrue", 2/speedLow);
                //speed = speedLow;
            }
        }else if (Physics.Raycast(transform.position + transform.right - transform.forward/2, transform.TransformDirection(-Vector3.up) * 1, out hit, 1, layerMask) && !currentHit.transform){

            if (hit.transform.tag == "Rail" ){
                transform.Rotate(Vector3.up, 90);
                canTurn = false;
                //Invoke("canTurnToTrue", 2/speedLow);
                //speed = speedLow;
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
    }

    private void canTurnToTrue(){
        speed = speedOriginal;
        canTurn = true;
    }

    private void checkCaseBelow()
    {
        

        if (!Physics.Raycast(transform.position + transform.right / 3, transform.TransformDirection(-Vector3.up) /3, out currentHit, 1, layerMask)) 
            return;

        Debug.DrawRay(transform.position + transform.right / 3, transform.TransformDirection(-Vector3.up) /3, Color.yellow);

        // print("transform: " + currentHit.transform);
        // print("gameobkect: " + currentHit.transform.gameObject);
        // print("Component: " + currentHit.transform.gameObject.GetComponent<RailBehavior>());
        // print("trainOrders: " + currentHit.transform.gameObject.GetComponent<RailBehavior>().trainOrders);

        if (!currentHit.transform.TryGetComponent(out RailBehavior rail)) return;

        List<RailBehavior.TrainOrderItem> trainOrders = rail.trainOrders;


        foreach (var orderItem in trainOrders)
        {
            if (orderItem.trainID == trainID){
                Debug.Log(orderItem.orderID);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Train"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void CheckRailway()
    {

    }
}
