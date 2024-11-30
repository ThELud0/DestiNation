using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Train : MonoBehaviour
{
    float lifetime;
    float speed;
    public int trainID;
    [SerializeField] GameObject raycastOrigin;
    public void Initialize(float lifetime, float speed, int trainID)
    {
        this.lifetime = lifetime;
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        int layerMask = 1 << 6;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            if (hit.collider.CompareTag("Rail"))
            {
                if (hit.collider.gameObject.GetComponent<RailBehavior>().trainOrders[0].trainID == trainID)
                {

                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1000, Color.white);
            Debug.Log("Did not Hit");
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
