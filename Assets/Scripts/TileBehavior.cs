using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.FilePathAttribute;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Linq;
using NUnit.Framework;




public class TileBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject railStraightPrefab, railTurnPrefab, railIntersectionQuadrupleEnCroixRM, autreRailTurnPrefab, ThirdWaysRailPrefab, CrossRailPrefab;
    
     [SerializeField]
    private Transform railsParent;
    Vector3 additionnalHeight = new Vector3(0f, 1f, 0f);
    GameObject createdRail;

    private Levelgenerator levelgenerator;

    public bool tileForRail = false;

[SerializeField]    private List<Vector2[]> all_dir_set = new ();
    GameObject intersectionRail;
    int iii = 0;

[SerializeField]    GameObject[] markers;

void Start()
{
    disableMeshRails();
    disableMarkers();
    //addRail(new Vector2[]{new Vector2(1,0), new Vector2(1,0)});
    //CreateRail(railStraightPrefab, new Quaternion());
}
    public void CreateStraighRail(Quaternion rotation)
    {

        //disableMeshRails();
        //createdRail.SetActive(false);
         
       // createdRail = prefab;
        //createdRail = Instantiate(prefab, transform.position + additionnalHeight, rotation);
        //createdRail.transform.rotation = rotation;
        //railStraightPrefab.transform.rotation = rotation;
        disableMeshRails();
        railsParent.rotation = rotation;
        railStraightPrefab.SetActive(true);
        //Debug.Log("Displayoin "+ railStraightPrefab);

        //createdRail = Instantiate(railTurnPrefab, transform.position + additionnalHeight, rotation);
        //railStraightPrefab.SetActive(true);
        //createdRail.SetActive(true);

        
        
    }

    public void CreateTurnRail(Quaternion rotation){
        disableMeshRails();
        railsParent.rotation = rotation;
        railTurnPrefab.SetActive(true);
    }

     public void Create3WayRailPrefab(Quaternion rotation){
        disableMeshRails();
        railsParent.rotation = rotation;
        ThirdWaysRailPrefab.SetActive(true);
    }

    public void CreateCrossRailPrefab(){
        disableMeshRails();
        CrossRailPrefab.SetActive(true);
    }

   /* void Update(){
        if (Mouse.current.leftButton.wasPressedThisFrame){
          //  addRail(new Vector2(2,0));
           // Debug.Log("clicked");
        }
    }*/

    private void disableMeshRails(){

        if(!tileForRail){
            return;
        }
        foreach(GameObject p in new GameObject[]{railStraightPrefab, railTurnPrefab, railIntersectionQuadrupleEnCroixRM, autreRailTurnPrefab,
        ThirdWaysRailPrefab, CrossRailPrefab}){
            p.SetActive(false);
        }
        /*createdRail = null;
        createdRail = railStraightPrefab;
        createdRail.SetActive(true);*/
    }

   /* public void createRail(GameObject prefabRail, Quaternion){

    }*/

   /* public void CreateTurnRail(Quaternion rotation)
    {
        createdRail = Instantiate(railTurnPrefab, transform.position + additionnalHeight, rotation);
    }*/


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

    public void setTileManager(Levelgenerator levelGen){
        levelgenerator = levelGen;
    }

    public void addRail(Vector2[] dir){
            all_dir_set.Add(dir);
            UpdateRail();
    }

     public void removeRail(Vector2[] dir){
        List<Vector2[]> new_all_dir = new ();
        bool item_lremoved = false;
        for (int i = 0; i < all_dir_set.Count; i++)
        {
            if(all_dir_set[i][0]!=dir[0] || all_dir_set[i][1]!=dir[1] || item_lremoved){
                    new_all_dir.Add(all_dir_set[i]);
            }else{
                item_lremoved = true;
                iii++;
            }
        }
        if(iii > 1 ){
           // Debug.LogError("i,e");
        } 
        all_dir_set = new_all_dir;
            //all_dir_set.Remove(dir);
            if(all_dir_set.Count>0){
                //Debug.LogWarning(all_dir_set.Count+":"+all_dir_set[0][0].x+"."+all_dir_set[0][0].y+" / " + dir[0].x+"."+dir[0].y);
                Debug.LogWarning(all_dir_set.Count+":"+all_dir_set[0][0].ToString()+" / " + dir[0].ToString());
                Debug.LogWarning(all_dir_set.Count+"::"+all_dir_set[0][1].ToString()+" / " + dir[1].ToString());

            }
            //all_dir_set.Clear();
            UpdateRail();
    }

    private bool[] getCodedInformations(){
        bool[] exits_coded_bools = {false,false,false,false};
        foreach(Vector2[] dir in all_dir_set){
                    //Debug.Log("on code "+dir.x+"/"+dir.y);
                  if (dir[0].x > 0 || dir[1].x<0){
                    exits_coded_bools[0]=true;
                }
                if (dir[0].y>0 || dir[1].y<0){
                    exits_coded_bools[1]=true;
                }
                 if (dir[1].x > 0 || dir[0].x < 0 ){
                    exits_coded_bools[2]=true;
                }
                if (dir[1].y> 0 || dir[0].y<0){
                    exits_coded_bools[3]=true;
                }  
                /*if (dir.x > 0 || dir.x<-1){
                    exits_coded_bools[2]=true;
                }
                if (dir.x < 0 || dir.x>1){
                    exits_coded_bools[0]=true;
                }
                 if (dir.y > 0 || dir.y <- 1){
                    exits_coded_bools[1]=true;
                }
                if (dir.y< 0 || dir.y> 1){
                    exits_coded_bools[3]=true;
                }*/
        }
        return exits_coded_bools;
    }

    private void UpdateRail(){
            Quaternion rotation = Quaternion.identity;

            bool[] info_all_rail = getCodedInformations();
          //  Debug.Log(info_all_rail[0] + "/"+info_all_rail[1] + "/"+info_all_rail[2] + "/"+ info_all_rail[3]);
            //Debug.Log(compareBoolLists(info_all_rail,new bool[4]{true,true, false, false}));
            
            setRailInListIndex(true);
            if(compareBoolLists(info_all_rail,new bool[4]{true,false, true, false})){
               // Debug.Log("tftf");
                rotation = Quaternion.Euler(0f, 0f, 0f);
                //CreateRail(railStraightPrefab,rotation);
                CreateStraighRail(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{true,true, false, false})){
                                //Debug.Log("ttff");

                rotation = Quaternion.Euler(0f, 90f, 0f);
                //CreateRail(railTurnPrefab,rotation);
                CreateTurnRail(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{false,true, false, true})){
                                //Debug.Log("ftft");

                rotation = Quaternion.Euler(0f, 270f, 0f);
                CreateStraighRail(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{false,true, true, false})){
                               // Debug.Log("fttf");

                rotation = Quaternion.Euler(0f, 0f, 0f);
                CreateTurnRail(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{false,false, true, true})){
                               // Debug.Log("fftt");

                rotation = Quaternion.Euler(0f, 270f, 0f);
                CreateTurnRail(rotation);
            }else if (compareBoolLists(info_all_rail,new bool[4]{true,false, false, true})){
                               // Debug.Log("tfft");

                rotation = Quaternion.Euler(0f, 180f, 0f);
                CreateTurnRail(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{false,true, true, true})){
                rotation = Quaternion.Euler(0f, 270f, 0f);
                Create3WayRailPrefab(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{true,false, true, true})){
                rotation = Quaternion.Euler(0f, 180f, 0f);
                Create3WayRailPrefab(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{true,true, false, true})){
                rotation = Quaternion.Euler(0f, 90f, 0f);
                Create3WayRailPrefab(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{true,true, true, false})){
                rotation = Quaternion.Euler(0f, 0f, 0f);
                Create3WayRailPrefab(rotation);
            }else if(compareBoolLists(info_all_rail,new bool[4]{true,true, true, true})){
                    CreateCrossRailPrefab();
            }else{
                Debug.LogWarning("aaaaargh"+info_all_rail[0] + "/"+info_all_rail[1] + "/"+info_all_rail[2] + "/"+ info_all_rail[3]);
                setRailInListIndex(false);
                disableMeshRails();
            }
    }


private void setRailInListIndex(bool isRail){
    levelgenerator.setRailInListIndex(GameTools.get2Dfrom3DVector(transform.position),isRail);
}

private bool compareBoolLists(bool[] info_all_rail, bool[] bool_infos){
    for(int i=0; i<bool_infos.Length; i++){
            if(bool_infos[i]!=info_all_rail[i]){
                    return false;
            }
    }
    return true;
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
        /*if (createdRail != null)
        {
            Destroy(createdRail);
            createdRail = null;
        }
        if (intersectionRail != null)
        {
            Destroy(intersectionRail);
            intersectionRail = null;
            createdRail = Instantiate(railPrefab, transform.position + additionnalHeight, Quaternion.identity);
        }*/
    }

    public GameObject RailReturn()
    {
        return createdRail;
    }
}



