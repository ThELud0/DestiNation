using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;


public class TileBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject railPrefab, railTurnPrefab, IntersectionQuadrupleEnCroixRM;
    Vector3 additionnalHeight = new Vector3(0f, 1f, 0f);
    GameObject createdRail;
    GameObject intersectionRail;


    public void CreateRail(Quaternion rotation)
    {
        if(createdRail == null)
        {
            createdRail = Instantiate(railPrefab, transform.position + additionnalHeight, rotation);
        }
        else
        {
            Destroy(createdRail);
            intersectionRail = Instantiate(IntersectionQuadrupleEnCroixRM, transform.position + additionnalHeight, rotation);
        }
    }

    public void CreateTurnRail(Quaternion rotation)
    {
        createdRail = Instantiate(railTurnPrefab, transform.position + additionnalHeight, rotation);
    }


    public void CheckPath(Vector2 currentPos, Vector2 nextPos, Vector2 previousPos)
    {
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
                rotation = Quaternion.Euler(0f, 0f, 0f);
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
    public void DeleteRail()
    {
        if (createdRail != null)
        {
            Destroy(createdRail);
            createdRail = null;
        }
        if (intersectionRail != null)
        {
            Destroy(intersectionRail);
            intersectionRail = null;
            createdRail = Instantiate(railPrefab, transform.position + additionnalHeight, Quaternion.identity);
        }
    }

    public GameObject RailReturn()
    {
        return createdRail;
    }
}



