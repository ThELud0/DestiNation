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

    private List<Vector2> all_dir_set = new ();
    GameObject intersectionRail;

[SerializeField]    GameObject[] markers;

void Start()
{
    disableMarkers();
}
    /*public void CreateRail(Quaternion rotation)
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
    }*/

   /* public void createRail(GameObject prefabRail, Quaternion){

    }*/

    public void CreateTurnRail(Quaternion rotation)
    {
        createdRail = Instantiate(railTurnPrefab, transform.position + additionnalHeight, rotation);
    }


public void CreateRail(){

}
    public void disableMarkers(){
        foreach(GameObject g in markers){
            g.SetActive(false);
        }
    }

    public void setMarker(int i){
        disableMarkers();
        markers[i].SetActive(true);
    }

    public void setYellowmarker(){
        disableMarkers();
        markers[0].SetActive(true);
    }

     public void setRedmarker(){
        disableMarkers();
        markers[1].SetActive(true);
    }

     public void setGreenmarker(){
        disableMarkers();
        markers[2].SetActive(true);
    }

    public void addRail(Vector2 dir){
            all_dir_set.Add(dir);
            UpdateRail();
    }

     public void removeRail(Vector2 dir){
            all_dir_set.Remove(dir);
            UpdateRail();
    }

    private bool[] getCodedInformations(){
        bool[] exits_coded_bools = {false,false,false,false};
        foreach(Vector2 dir in all_dir_set){
                if (dir.x > 0){
                    exits_coded_bools[2]=true;
                }
                if (dir.x < 0){
                    exits_coded_bools[0]=true;
                }
                 if (dir.y > 0){
                    exits_coded_bools[1]=true;
                }
                if (dir.y< 0){
                    exits_coded_bools[3]=true;
                }
        }
        return exits_coded_bools;
    }

    private void UpdateRail(){
                Quaternion rotation = Quaternion.identity;

            bool[] info_all_rail = getCodedInformations();

            if(info_all_rail == new bool[4]{true, false, true, false}){
                rotation = Quaternion.Euler(0f, 90f, 0f);
                //CreateRail(rotation);
            }else if(info_all_rail == new bool[4]{false,true, false, true}){
                rotation = Quaternion.Euler(0f, 0f, 0f);
                //CreateRail(rotation);
            }else if(info_all_rail == new bool[4]{false,true, false, true}){
                rotation = Quaternion.Euler(0f, 0f, 0f);
                //CreateRail(rotation);
            }
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
                //CreateRail(rotation);
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
               // CreateRail(rotation);
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
                rotation = Quaternion.Euler(0f, 180f, 0f);
                CreateTurnRail(rotation);
            }
            else if (currentPos.y > nextPos.y)
            {
                //g
                rotation = Quaternion.Euler(0f, 90f, 0f);
                //CreateRail(rotation);
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
               // CreateRail(rotation);
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



