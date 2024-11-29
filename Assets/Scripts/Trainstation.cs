using UnityEngine;

public class Trainstation : MonoBehaviour
{
    float destinyTimer;
    float trainTimer;
    void Start()
    {
        destinyTimer = Random.Range(10, 30);
        trainTimer = Random.Range(2, 6);
    }

    void Update()
    {
        
    }

    void releaseTrain()
    {
        //Instantiate(train, position, Quaternion.identity);
    }
}
