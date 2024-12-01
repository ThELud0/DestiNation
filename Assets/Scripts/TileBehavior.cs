using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class TileBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject railPrefab;
    [SerializeField]
    private GameObject railTurnPrefab;
    Vector3 additionnalHeight = new Vector3(0f, 1f, 0f);

    public void CreateRail(Quaternion rotation)
    {
        GameObject rail = Instantiate(railPrefab, transform.position + additionnalHeight, rotation);
        

    }

    public void CreateTurnRail(Quaternion rotation)
    {
        Instantiate(railTurnPrefab, transform.position + additionnalHeight, rotation);
    }


    public void CheckPath(Vector2 currentPos, Vector2 nextPos, Vector2 previousPos)
    {

        //Vector2 turnDir = nextPos - currentPos;
        Quaternion rotation = Quaternion.identity;

        if (currentPos.y > previousPos.y)
        {
            if (currentPos.x > nextPos.x)
            {
                //dh
                rotation = Quaternion.Euler(0f, 0f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.x < nextPos.x)
            {
                //db
                rotation = Quaternion.Euler(0f, -90f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.y < nextPos.y)
            {
                //d
                rotation = Quaternion.Euler(0f, 90f, 0f);
                CreateRail(rotation);
            }
        }
        else if (currentPos.x > previousPos.x)
        {
            if (currentPos.y < nextPos.y)
            {
                //bd
                rotation = Quaternion.Euler(0f, 90f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.y > nextPos.y)
            {
                //bg
                rotation = Quaternion.Euler(0f, -90f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.x < nextPos.x)
            {
                //b
                rotation = Quaternion.Euler(0f, 00f, 0f);
                CreateRail(rotation);
            }
        }
        else if (currentPos.y < previousPos.y)
        {
            if (currentPos.x > nextPos.x)
            {
                //gh
                rotation = Quaternion.Euler(0f, 90f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.x < nextPos.x)
            {
                //gb
                rotation = Quaternion.Euler(0f, -90f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.y > nextPos.y)
            {
                //g
                rotation = Quaternion.Euler(0f, 90f, 0f);
                CreateRail(rotation);
            }
        }
        else if (currentPos.x < previousPos.x)
        {
            if (currentPos.y < nextPos.y)
            {
                //hd
                rotation = Quaternion.Euler(0f, 180f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.y > nextPos.y)
            {
                //hg
                rotation = Quaternion.Euler(0f, -90f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.x > nextPos.x)
            {
                //h
                rotation = Quaternion.Euler(0f, 0f, 0f);
                CreateRail(rotation);
            }
        }



      





    }
    }
    //var monrail

    //connaitre son rail
    //on m'ajoute une direction
    //on m'enlève une direction

    //update le rail


