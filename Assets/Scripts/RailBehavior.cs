using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class RailBehavior : MonoBehaviour
{

    // A list of pairs (trainID, orderID)
    public List<(int trainID, int orderID)> trainOrders = new List<(int, int)>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void AddTrainOrder(int trainID, int orderID)
    {
        trainOrders.Add((trainID, orderID));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        Debug.Log(trainOrders[0].trainID + " " + trainOrders[0].orderID);
    }

}
