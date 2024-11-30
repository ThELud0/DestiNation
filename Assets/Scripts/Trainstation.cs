using UnityEngine;

public class Trainstation : MonoBehaviour
{
    float destinyTimer;
    float trainTimer;
    void Start()
    {
        destinyTimer = Time.time + Random.Range(10, 30);
        trainTimer = Random.Range(2, 6);
    }

    void Update()
    {
        if (Time.time >= destinyTimer)
        {
            changeNature();
        }
    }

    void changeNature()
    {
        //GetComponent<MeshRenderer>().material.mainTexture ;
    }
}
