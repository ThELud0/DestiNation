using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Train : MonoBehaviour
{
    float lifetime;
    float speed;
    
    public void Initialize(float lifetime, float speed)
    {
        this.lifetime = lifetime;
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
